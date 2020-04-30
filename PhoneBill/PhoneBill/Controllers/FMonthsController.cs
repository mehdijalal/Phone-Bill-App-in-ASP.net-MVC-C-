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
    public class FMonthsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: FMonths
        public ActionResult Index()
        {
            return View(db.FMonths.ToList());
        }

        // GET: FMonths/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FMonths fMonths = db.FMonths.Find(id);
            if (fMonths == null)
            {
                return HttpNotFound();
            }
            return View(fMonths);
        }

        // GET: FMonths/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FMonths/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MonthName")] FMonths fMonths)
        {
            if (ModelState.IsValid)
            {
                db.FMonths.Add(fMonths);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fMonths);
        }

        // GET: FMonths/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FMonths fMonths = db.FMonths.Find(id);
            if (fMonths == null)
            {
                return HttpNotFound();
            }
            return View(fMonths);
        }

        // POST: FMonths/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MonthName")] FMonths fMonths)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fMonths).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fMonths);
        }

        // GET: FMonths/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FMonths fMonths = db.FMonths.Find(id);
            if (fMonths == null)
            {
                return HttpNotFound();
            }
            return View(fMonths);
        }

        // POST: FMonths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FMonths fMonths = db.FMonths.Find(id);
            db.FMonths.Remove(fMonths);
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
