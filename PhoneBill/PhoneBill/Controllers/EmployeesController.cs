using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneBill.Models;
using PhoneBill.ViewModels;

namespace PhoneBill.Controllers
{
    public class EmployeesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            var emp = from em in db.MyEmployeeContext
                      join s in db.MySupervisorContext on em.SupervisorId equals s.Id
                      select new EmployeeVM
                      {
                          Id = em.Id,
                          FullName = em.FullName,
                          UserName = em.UserName,
                          EmailAddress = em.EmailAddress,
                          PhoneNumber = em.PhoneNumber,
                          SupervisorName = s.SupervisorName,
                          SupervisorEmail = s.SupervisorEmail,
                          SupervisorPhone = s.SupervisorPhone
                      };
            return View(emp);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.MyEmployeeContext.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            //----------Get All Years----//
            var AllSupervisors = db.MySupervisorContext.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MySupervisor = new List<SelectListItem>();
            //MyYears.Add(new SelectListItem { Value = "", Text = "-All-" });
            foreach (var SItem in AllSupervisors)
            {
                MySupervisor.Add(new SelectListItem { Value = SItem.Id.ToString(), Text = SItem.SupervisorName.ToString() });
            }
            ViewBag.SupervisorOptions = MySupervisor;
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoleId,PhoneNumber,FullName,UserName,EmailAddress,StatusId,UnitesSectionsId,SupervisorId")] Employee employee, int SupervisorOptions)
        {
            if (ModelState.IsValid)
            {
                employee.SupervisorId = SupervisorOptions;
                employee.RoleId = 2;
                db.MyEmployeeContext.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.MyEmployeeContext.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            //----------Get All Years----//
            var AllSupervisors = db.MySupervisorContext.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MySupervisor = new List<SelectListItem>();
            //MyYears.Add(new SelectListItem { Value = "", Text = "-All-" });
            foreach (var SItem in AllSupervisors)
            {
                var item = new SelectListItem();
                if (SItem.Id == employee.SupervisorId)
                    item.Selected = true;
                item.Value = SItem.Id.ToString();
                item.Text = SItem.SupervisorName.ToString();
                MySupervisor.Add(item);
                //MySupervisor.Add(new SelectListItem { Value = SItem.Id.ToString(), Text = SItem.SupervisorName.ToString() });
            }
            ViewBag.SupervisorOptions = MySupervisor;
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoleId,PhoneNumber,FullName,UserName,EmailAddress,StatusId,UnitesSectionsId,SupervisorId")] Employee employee,int SupervisorOptions)
        {
            if (ModelState.IsValid)
            {
                employee.SupervisorId = SupervisorOptions;
                employee.RoleId = 2;
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.MyEmployeeContext.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.MyEmployeeContext.Find(id);
            db.MyEmployeeContext.Remove(employee);
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
