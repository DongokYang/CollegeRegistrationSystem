using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BITCollege_DY.Data;
using BITCollege_DY.Models;

namespace BITCollege_DY.Controllers
{
    public class NextAuditCoursesController : Controller
    {
        private BITCollege_DYContext db = new BITCollege_DYContext();

        // GET: NextAuditCourses
        public ActionResult Index()
        {
            var nextAuditCourse = NextAuditCourse.GetInstance();
            return View(nextAuditCourse);
        }

        // GET: NextAuditCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextAuditCourse nextAuditCourse = (NextAuditCourse)db.NextUniqueNumbers.Find(id);
            if (nextAuditCourse == null)
            {
                return HttpNotFound();
            }
            return View(nextAuditCourse);
        }

        // GET: NextAuditCourses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NextAuditCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NextUniqueNumberId,NextAvailableNumber")] NextAuditCourse nextAuditCourse)
        {
            if (ModelState.IsValid)
            {
                db.NextUniqueNumbers.Add(nextAuditCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nextAuditCourse);
        }

        // GET: NextAuditCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextAuditCourse nextAuditCourse = (NextAuditCourse)db.NextUniqueNumbers.Find(id);
            if (nextAuditCourse == null)
            {
                return HttpNotFound();
            }
            return View(nextAuditCourse);
        }

        // POST: NextAuditCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NextUniqueNumberId,NextAvailableNumber")] NextAuditCourse nextAuditCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextAuditCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nextAuditCourse);
        }

        // GET: NextAuditCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextAuditCourse nextAuditCourse = (NextAuditCourse)db.NextUniqueNumbers.Find(id);
            if (nextAuditCourse == null)
            {
                return HttpNotFound();
            }
            return View(nextAuditCourse);
        }

        // POST: NextAuditCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NextAuditCourse nextAuditCourse = (NextAuditCourse)db.NextUniqueNumbers.Find(id);
            db.NextUniqueNumbers.Remove(nextAuditCourse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
