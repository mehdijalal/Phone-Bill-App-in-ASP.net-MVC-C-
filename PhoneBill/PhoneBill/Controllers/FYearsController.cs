using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneBill.Models;

namespace PhoneBill.Controllers
{
    public class FYearsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: FYears
        public ActionResult Index()
        {
            return View(db.FYears.ToList());
        }

        // GET: FYears/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FYears fYears = db.FYears.Find(id);
            if (fYears == null)
            {
                return HttpNotFound();
            }
            return View(fYears);
        }

        // GET: FYears/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FYears/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,YearName")] FYears fYears)
        {
            if (ModelState.IsValid)
            {
                db.FYears.Add(fYears);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fYears);
        }

        // GET: FYears/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FYears fYears = db.FYears.Find(id);
            if (fYears == null)
            {
                return HttpNotFound();
            }
            return View(fYears);
        }

        // POST: FYears/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,YearName")] FYears fYears)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fYears).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fYears);
        }

        // GET: FYears/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FYears fYears = db.FYears.Find(id);
            if (fYears == null)
            {
                return HttpNotFound();
            }
            return View(fYears);
        }

        // POST: FYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FYears fYears = db.FYears.Find(id);
            db.FYears.Remove(fYears);
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
