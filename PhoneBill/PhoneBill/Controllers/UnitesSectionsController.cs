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
    public class UnitesSectionsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: UnitesSections
        public ActionResult Index()
        {
            return View(db.MyUnitSectionContext.ToList());
        }

        // GET: UnitesSections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitesSections unitesSections = db.MyUnitSectionContext.Find(id);
            if (unitesSections == null)
            {
                return HttpNotFound();
            }
            return View(unitesSections);
        }

        // GET: UnitesSections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnitesSections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UnitSectionName,SectionHeadEmail,SectionPhone,Extension,FinanceCode,Remarks")] UnitesSections unitesSections)
        {
            if (ModelState.IsValid)
            {
                db.MyUnitSectionContext.Add(unitesSections);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(unitesSections);
        }

        // GET: UnitesSections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitesSections unitesSections = db.MyUnitSectionContext.Find(id);
            if (unitesSections == null)
            {
                return HttpNotFound();
            }
            return View(unitesSections);
        }

        // POST: UnitesSections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UnitSectionName,SectionHeadEmail,SectionPhone,Extension,FinanceCode,Remarks")] UnitesSections unitesSections)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unitesSections).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(unitesSections);
        }

        // GET: UnitesSections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitesSections unitesSections = db.MyUnitSectionContext.Find(id);
            if (unitesSections == null)
            {
                return HttpNotFound();
            }
            return View(unitesSections);
        }

        // POST: UnitesSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UnitesSections unitesSections = db.MyUnitSectionContext.Find(id);
            db.MyUnitSectionContext.Remove(unitesSections);
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
