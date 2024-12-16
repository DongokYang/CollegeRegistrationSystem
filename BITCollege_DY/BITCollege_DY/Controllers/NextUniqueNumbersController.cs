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
    public class NextUniqueNumbersController : Controller
    {
        private BITCollege_DYContext db = new BITCollege_DYContext();

        // GET: NextUniqueNumbers
        public ActionResult Index()
        {
            return View(db.NextUniqueNumbers.ToList());
        }

        // GET: NextUniqueNumbers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextUniqueNumber nextUniqueNumber = db.NextUniqueNumbers.Find(id);
            if (nextUniqueNumber == null)
            {
                return HttpNotFound();
            }
            return View(nextUniqueNumber);
        }

        // GET: NextUniqueNumbers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NextUniqueNumbers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NextUniqueNumberId,NextAvailableNumber")] NextUniqueNumber nextUniqueNumber)
        {
            if (ModelState.IsValid)
            {
                db.NextUniqueNumbers.Add(nextUniqueNumber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nextUniqueNumber);
        }

        // GET: NextUniqueNumbers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextUniqueNumber nextUniqueNumber = db.NextUniqueNumbers.Find(id);
            if (nextUniqueNumber == null)
            {
                return HttpNotFound();
            }
            return View(nextUniqueNumber);
        }

        // POST: NextUniqueNumbers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NextUniqueNumberId,NextAvailableNumber")] NextUniqueNumber nextUniqueNumber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextUniqueNumber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nextUniqueNumber);
        }

        // GET: NextUniqueNumbers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextUniqueNumber nextUniqueNumber = db.NextUniqueNumbers.Find(id);
            if (nextUniqueNumber == null)
            {
                return HttpNotFound();
            }
            return View(nextUniqueNumber);
        }

        // POST: NextUniqueNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NextUniqueNumber nextUniqueNumber = db.NextUniqueNumbers.Find(id);
            db.NextUniqueNumbers.Remove(nextUniqueNumber);
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
