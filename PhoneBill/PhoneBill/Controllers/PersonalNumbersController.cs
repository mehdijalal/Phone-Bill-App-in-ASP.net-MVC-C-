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
    public class PersonalNumbersController : Controller
    {
        private MyDbContext db = new MyDbContext();

        public ActionResult Index()
        {
            var UserName = User.Identity.Name;
            var TrimedUser = UserName.TrimStart("UNOCHA\\".ToCharArray());
            //-----------Get My Number and My Employee Id ---------------//
            double My_Number = 0;
            int My_Emp_Id = 0;
            var myNumber = db.MyEmployeeContext.FirstOrDefault(u => u.UserName.Equals(TrimedUser, StringComparison.CurrentCultureIgnoreCase));
            if (myNumber != null)
            {
                My_Number = myNumber.PhoneNumber;
                My_Emp_Id = myNumber.Id;
            }
            var MyPersonalNumbers = from c in db.MyPersonalNumContext
                                    where c.EmployeeId == My_Emp_Id
                                    select c;
            return View(MyPersonalNumbers);
        }
        // GET: PersonalNumbers/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PersonalNumbers personalNumbers = db.MyPersonalNumContext.Find(id);
        //    if (personalNumbers == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(personalNumbers);
        //}

        //// GET: PersonalNumbers/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: PersonalNumbers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,EmployeeId,PersonalNumber,NumberTypeId")] PersonalNumbers personalNumbers)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.MyPersonalNumContext.Add(personalNumbers);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(personalNumbers);
        //}

        //// GET: PersonalNumbers/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PersonalNumbers personalNumbers = db.MyPersonalNumContext.Find(id);
        //    if (personalNumbers == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(personalNumbers);
        //}

        //// POST: PersonalNumbers/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,EmployeeId,PersonalNumber,NumberTypeId")] PersonalNumbers personalNumbers)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(personalNumbers).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(personalNumbers);
        //}

        // GET: PersonalNumbers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalNumbers personalNumbers = db.MyPersonalNumContext.Find(id);
            if (personalNumbers == null)
            {
                return HttpNotFound();
            }
            return View(personalNumbers);
        }

        // POST: PersonalNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonalNumbers personalNumbers = db.MyPersonalNumContext.Find(id);
            db.MyPersonalNumContext.Remove(personalNumbers);
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
