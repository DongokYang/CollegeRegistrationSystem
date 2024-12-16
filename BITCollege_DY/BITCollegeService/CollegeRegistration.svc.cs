using BITCollege_DY.Data;
using BITCollege_DY.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Utility;

namespace BITCollegeService
{
    public class CollegeRegistration : ICollegeRegistration
    {
        private BITCollege_DYContext db = new BITCollege_DYContext();

        /// <summary>
        /// Get registration record from the database,then remove the record from the database.
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns>Return whether method was successful</returns>
        public bool DropCourse(int registrationId)
        {
            try
            {
                var registration = db.Registrations.SingleOrDefault(r => r.RegistrationId == registrationId);

                if (registration == null)
                {
                    return false;
                }

                db.Registrations.Remove(registration);

                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Retrive, validate and add the Registration object to the database and persist to the database
        /// </summary>
        /// <param name="studentId">The ID of the student</param>
        /// <param name="courseId">The ID of the course</param>
        /// <param name="notes">note related to the registration</param>
        /// <returns> 
        /// Returns 0 if the registration is successful. 
        /// Returns -100 if the student already has an incomplete registration for the same course. 
        /// Returns -200 if the student exceeds the maximum allowed attempts for a Mastery course. 
        /// Returns -300 if an exception occurs during the database update.
        /// </returns>
        public int RegisterCourse(int studentId, int courseId, string notes)
        {
            IQueryable<Registration> registrations = db.Registrations
                .Where(r => r.StudentId == studentId && r.CourseId == courseId);

            int attempCount = 0;
            foreach (Registration registration in registrations)
            {
                if (registration.Grade == null) return -100;

                if (registration.Course.CourseType == "Mastery") attempCount++;
            }

            if (attempCount > 0)
            {
                MasteryCourse course = (MasteryCourse)db.Courses
                .Where(x => x.CourseId == courseId)
                .SingleOrDefault();

                if (attempCount > course.MaximumAttempts) return -200;
            }


            try
            {
                Registration newRegistration = new Registration();

                newRegistration.StudentId = studentId;
                newRegistration.CourseId = courseId;
                newRegistration.Notes = notes;
                newRegistration.RegistrationDate = DateTime.Today;
                newRegistration.SetNextRegistrationNumber();
                db.Registrations.Add(newRegistration);
                db.SaveChanges();

                Student student = db.Students
                                    .Where(x => x.StudentId == studentId)
                                    .SingleOrDefault();

                newRegistration = db.Registrations
                    .Include(r => r.Course)
                    .SingleOrDefault(r => r.RegistrationId == newRegistration.RegistrationId);

                student.OutstandingFees += student.GradePointState.TuitionRateAdjustment(student) * newRegistration.Course.TuitionAmount;
                db.SaveChanges();

            }
            catch (Exception)
            {
                return -300;
            }

            return 0;
        }



        /// <summary>
        /// Retrieve and Update the grade from Registration record from the database which corresponds to the method argument.
        /// </summary>
        /// <param name="registrationId">The ID of the registration</param>
        /// <param name="grade">The grade of the student</param>
        /// <param name="notes">note related to the registration</param>
        /// <returns>result of CalculateGradePointAveragev method</returns>
        public double? UpdateGrade(int registrationId, double grade, string notes)
        {
            try
            {
                Registration registration = db.Registrations
                                              .Where(x => x.RegistrationId == registrationId)
                                              .SingleOrDefault();

                if (registration == null)
                {
                    return null;
                }

                registration.Grade = grade;
                registration.Notes = notes;
                db.SaveChanges();

                double? updatedGPA = CalculateGradePointAverage(registration.StudentId);
                db.SaveChanges();

                return updatedGPA;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Calculates the Grade Point Average (GPA) for a student 
        /// </summary>
        /// <param name="studentId">The Id of  student</param>
        /// <returns>
        /// the calculated GPA, or null if the student has no graded courses
        /// </returns>

        public double? CalculateGradePointAverage(int studentId)
        {
            IQueryable<Registration> registrations = db.Registrations
                                                       .Where(x => x.StudentId == studentId && x.Grade != null);

            double totalGradePoint = 0;
            double totalCreditHours = 0;

            foreach (Registration registration in registrations.ToList())
            {
                double grade = (double)registration.Grade;
                CourseType courseType = BusinessRules.CourseTypeLookup(registration.Course.CourseType);

                if (courseType == CourseType.AUDIT)
                {
                    continue;
                }

                double gradePoint = BusinessRules.GradeLookup(grade, courseType);
                double gradePointValue = gradePoint * registration.Course.CreditHours;

                totalGradePoint += gradePointValue;
                totalCreditHours += registration.Course.CreditHours;
            }

            double? GPA = (totalCreditHours > 0) ? (double?)(totalGradePoint / totalCreditHours) : null;

            Student student = db.Students
                                .Where(x => x.StudentId == studentId)
                                .SingleOrDefault();

            if (student != null)
            {
                student.GradePointAverage = GPA;
                student.ChangeState();
                db.SaveChanges();
            }

            return GPA;
        }
    }
}