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
    public class AllPhoneBillsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: AllPhoneBills
        public ActionResult Index()
        {
            return View(db.MyAllPhoneBillContext.ToList());
        }

        // GET: AllPhoneBills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllPhoneBill allPhoneBill = db.MyAllPhoneBillContext.Find(id);
            if (allPhoneBill == null)
            {
                return HttpNotFound();
            }
            return View(allPhoneBill);
        }

        // GET: AllPhoneBills/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AllPhoneBills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ACCT_ID,CALLING_NUMBER,CALLED_NUMBER,CALL_DURATION,CALL_DATE,CALL_TIME,CALL_COST,TAX")] AllPhoneBill allPhoneBill)
        {
            if (ModelState.IsValid)
            {
                db.MyAllPhoneBillContext.Add(allPhoneBill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(allPhoneBill);
        }

        // GET: AllPhoneBills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllPhoneBill allPhoneBill = db.MyAllPhoneBillContext.Find(id);
            if (allPhoneBill == null)
            {
                return HttpNotFound();
            }
            return View(allPhoneBill);
        }

        // POST: AllPhoneBills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ACCT_ID,CALLING_NUMBER,CALLED_NUMBER,CALL_DURATION,CALL_DATE,CALL_TIME,CALL_COST,TAX")] AllPhoneBill allPhoneBill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allPhoneBill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(allPhoneBill);
        }

        // GET: AllPhoneBills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllPhoneBill allPhoneBill = db.MyAllPhoneBillContext.Find(id);
            if (allPhoneBill == null)
            {
                return HttpNotFound();
            }
            return View(allPhoneBill);
        }

        // POST: AllPhoneBills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllPhoneBill allPhoneBill = db.MyAllPhoneBillContext.Find(id);
            db.MyAllPhoneBillContext.Remove(allPhoneBill);
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
