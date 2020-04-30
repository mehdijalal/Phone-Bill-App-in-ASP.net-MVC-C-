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
    public class MyRolesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: MyRoles
        public ActionResult Index()
        {
            return View(db.MyRolesContext.ToList());
        }

        // GET: MyRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyRoles myRoles = db.MyRolesContext.Find(id);
            if (myRoles == null)
            {
                return HttpNotFound();
            }
            return View(myRoles);
        }

        // GET: MyRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoleName")] MyRoles myRoles)
        {
            if (ModelState.IsValid)
            {
                db.MyRolesContext.Add(myRoles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myRoles);
        }

        // GET: MyRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyRoles myRoles = db.MyRolesContext.Find(id);
            if (myRoles == null)
            {
                return HttpNotFound();
            }
            return View(myRoles);
        }

        // POST: MyRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoleName")] MyRoles myRoles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myRoles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myRoles);
        }

        // GET: MyRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyRoles myRoles = db.MyRolesContext.Find(id);
            if (myRoles == null)
            {
                return HttpNotFound();
            }
            return View(myRoles);
        }

        // POST: MyRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyRoles myRoles = db.MyRolesContext.Find(id);
            db.MyRolesContext.Remove(myRoles);
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
