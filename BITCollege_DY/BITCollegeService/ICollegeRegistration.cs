using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BITCollegeService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICollegeRegistration" in both code and config file together.
    [ServiceContract]
    public interface ICollegeRegistration
    {
        /// <summary>
        /// Get registration record from the database,then remove the record from the database.
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        [OperationContract]
        bool DropCourse(int registrationId);

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
        [OperationContract]
        int RegisterCourse(int studentId, int courseId, string notes);


        /// <summary>
        /// Retrieve and Update the grade from Registration record from the database which corresponds to the method argument.
        /// </summary>
        /// <param name="registrationId">The ID of the registration</param>
        /// <param name="grade">The grade of the student</param>
        /// <param name="notes">note related to the registration</param>
        [OperationContract]
        double? UpdateGrade(int registrationId, double grade, string notes);

    }
}
