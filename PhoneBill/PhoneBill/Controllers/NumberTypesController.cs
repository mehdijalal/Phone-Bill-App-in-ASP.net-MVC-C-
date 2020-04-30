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
    public class NumberTypesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: NumberTypes
        public ActionResult Index()
        {
            return View(db.MyNumberTypeContext.ToList());
        }

        // GET: NumberTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NumberType numberType = db.MyNumberTypeContext.Find(id);
            if (numberType == null)
            {
                return HttpNotFound();
            }
            return View(numberType);
        }

        // GET: NumberTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NumberTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NumberTypeName")] NumberType numberType)
        {
            if (ModelState.IsValid)
            {
                db.MyNumberTypeContext.Add(numberType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(numberType);
        }

        // GET: NumberTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NumberType numberType = db.MyNumberTypeContext.Find(id);
            if (numberType == null)
            {
                return HttpNotFound();
            }
            return View(numberType);
        }

        // POST: NumberTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NumberTypeName")] NumberType numberType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(numberType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(numberType);
        }

        // GET: NumberTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NumberType numberType = db.MyNumberTypeContext.Find(id);
            if (numberType == null)
            {
                return HttpNotFound();
            }
            return View(numberType);
        }

        // POST: NumberTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NumberType numberType = db.MyNumberTypeContext.Find(id);
            db.MyNumberTypeContext.Remove(numberType);
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
