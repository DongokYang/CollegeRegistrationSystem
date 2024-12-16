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
    public class NextRegistrationsController : Controller
    {
        private BITCollege_DYContext db = new BITCollege_DYContext();

        // GET: NextRegistrations
        public ActionResult Index()
        {
            var nextRegistration = NextRegistration.GetInstance();
            return View(nextRegistration);
        }

        // GET: NextRegistrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextRegistration nextRegistration = (NextRegistration)db.NextUniqueNumbers.Find(id);
            if (nextRegistration == null)
            {
                return HttpNotFound();
            }
            return View(nextRegistration);
        }

        // GET: NextRegistrations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NextRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NextUniqueNumberId,NextAvailableNumber")] NextRegistration nextRegistration)
        {
            if (ModelState.IsValid)
            {
                db.NextUniqueNumbers.Add(nextRegistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nextRegistration);
        }

        // GET: NextRegistrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextRegistration nextRegistration = (NextRegistration)db.NextUniqueNumbers.Find(id);
            if (nextRegistration == null)
            {
                return HttpNotFound();
            }
            return View(nextRegistration);
        }

        // POST: NextRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NextUniqueNumberId,NextAvailableNumber")] NextRegistration nextRegistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nextRegistration);
        }

        // GET: NextRegistrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextRegistration nextRegistration = (NextRegistration)db.NextUniqueNumbers.Find(id);
            if (nextRegistration == null)
            {
                return HttpNotFound();
            }
            return View(nextRegistration);
        }

        // POST: NextRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NextRegistration nextRegistration = (NextRegistration)db.NextUniqueNumbers.Find(id);
            db.NextUniqueNumbers.Remove(nextRegistration);
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
