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
using System.Web.Configuration;
using PhoneBill.MJLib;

namespace PhoneBill.Controllers
{
    public class InboxController : Controller
    {
        private MyDbContext db = new MyDbContext();
        /**
         * Supervisor Inbox View
         * For User there is another view
         */
        
        public ActionResult Index()
        {
            string PageNumber = WebConfigurationManager.AppSettings["pageNumber"];
            int pageNum = Convert.ToInt16(PageNumber);
            //=============Details==========================//
            int starting = 0;
            if (Request.Form["starting"] != null)
            {
                starting = Convert.ToInt32(Request.Form["starting"]);
            }
            //--------------details-------------------------//
            string strpost = "&ajax=1";
            //-----------User details---------------------//
            var UserName = User.Identity.Name;
            var TrimedUser = UserName.TrimStart("UNOCHA\\".ToCharArray());
            //-----------Get My Number and My Employee Id ---------------//
            double My_Number = 0;
            int My_Emp_Id = 0;
            int My_Supervisor_Id = 0;
            var myNumber = db.MyEmployeeContext.FirstOrDefault(u => u.UserName.Equals(TrimedUser, StringComparison.CurrentCultureIgnoreCase));
            if (myNumber != null)
            {
                My_Number = myNumber.PhoneNumber;
                My_Emp_Id = myNumber.Id;
                My_Supervisor_Id = myNumber.SupervisorId;
            }

            var S_Total = from i in db.MyEmpInbox
                             join em in db.MyEmployeeContext on i.ToId equals em.Id
                             where i.ToId == My_Emp_Id
                             select new EmpInboxVM
                             {
                                 Id = i.Id,
                                 EmailAddress = em.EmailAddress,
                                 Message = i.Message,
                                 FromId = i.FromId,
                                 EmpPhoneNumber = i.EmpPhoneNumber,
                                 ToId = i.ToId,
                                 year = i.year,
                                 month = i.month,
                                 CreatedDate = i.CreatedDate,
                                 ModifiedDate = i.ModifiedDate,
                                 SupStatus = i.SupStatus


                             };
            int NumRows = S_Total.Count();
            //============Query all===================================//
            var MyMessages = (from i in db.MyEmpInbox
                             join em in db.MyEmployeeContext on i.ToId equals em.Id
                             where i.ToId == My_Emp_Id
                             select new EmpInboxVM
                             {
                                 Id = i.Id,
                                 EmailAddress = em.EmailAddress,
                                 Message = i.Message,
                                 FromId = i.FromId,
                                 EmpPhoneNumber = i.EmpPhoneNumber,
                                 ToId =i.ToId,
                                 year = i.year,
                                 month =i.month,
                                 CreatedDate = i.CreatedDate,
                                 ModifiedDate = i.ModifiedDate,
                                 SupStatus = i.SupStatus
                                 

                             }).AsEnumerable().OrderByDescending(o => o.ModifiedDate).Skip(starting).Take(pageNum);

            string links = Pagination.paginate(NumRows, starting, pageNum, "", "page", strpost);
            ViewBag.Start = starting;
            ViewBag.total_rec = NumRows;
            ViewBag.link = links;
            if (Request.IsAjaxRequest())
            {
                return PartialView("Ajax_Index", MyMessages);
            }
            return View(MyMessages);
        }

        /**
         * Employee Inbox view
         */
        public ActionResult empIndex()
        {
            string PageNumber = WebConfigurationManager.AppSettings["pageNumber"];
            int pageNum = Convert.ToInt16(PageNumber);
            //=============Details==========================//
            int starting = 0;
            if (Request.Form["starting"] != null)
            {
                starting = Convert.ToInt32(Request.Form["starting"]);
            }
            //--------------details-------------------------//
            string strpost = "&ajax=1";
            //-----------User details---------------------//
            var UserName = User.Identity.Name;
            var TrimedUser = UserName.TrimStart("UNOCHA\\".ToCharArray());
            //-----------Get My Number and My Employee Id ---------------//
            double My_Number = 0;
            int My_Emp_Id = 0;
            int My_Supervisor_Id = 0;
            var myNumber = db.MyEmployeeContext.FirstOrDefault(u => u.UserName.Equals(TrimedUser, StringComparison.CurrentCultureIgnoreCase));
            if (myNumber != null)
            {
                My_Number = myNumber.PhoneNumber;
                My_Emp_Id = myNumber.Id;
                My_Supervisor_Id = myNumber.SupervisorId;
            }

            var S_Total = from i in db.MyEmpInbox
                          join em in db.MyEmployeeContext on i.FromId equals em.Id
                          where i.FromId == My_Emp_Id
                          select new EmpInboxVM
                          {
                              Id = i.Id,
                              EmailAddress = em.EmailAddress,
                              Message = i.Message,
                              FromId = i.FromId,
                              EmpPhoneNumber = i.EmpPhoneNumber,
                              ToId = i.ToId,
                              year = i.year,
                              month = i.month,
                              CreatedDate = i.CreatedDate,
                              ModifiedDate = i.ModifiedDate,
                              SupStatus = i.SupStatus


                          };
            int NumRows = S_Total.Count();
            //============Query all===================================//
            var MyMessages = (from i in db.MyEmpInbox
                              join em in db.MyEmployeeContext on i.FromId equals em.Id
                              where i.FromId == My_Emp_Id
                              select new EmpInboxVM
                              {
                                  Id = i.Id,
                                  EmailAddress = em.EmailAddress,
                                  Message = i.Message,
                                  FromId = i.FromId,
                                  EmpPhoneNumber = i.EmpPhoneNumber,
                                  ToId = i.ToId,
                                  year = i.year,
                                  month = i.month,
                                  CreatedDate = i.CreatedDate,
                                  ModifiedDate = i.ModifiedDate,
                                  SupStatus = i.SupStatus


                              }).AsEnumerable().OrderByDescending(o => o.ModifiedDate).Skip(starting).Take(pageNum);

            string links = Pagination.paginate(NumRows, starting, pageNum, "", "page", strpost);
            ViewBag.Start = starting;
            ViewBag.total_rec = NumRows;
            ViewBag.link = links;
            if (Request.IsAjaxRequest())
            {
                return PartialView("empIndex_ajax", MyMessages);
            }
            return View(MyMessages);
        }

        public ActionResult ViewEmployeeCalls(int? Inbox_id,int?emp_id,int year, int month)
        {
            //var base_url = HttpContext.Request.Url.Authority;


            int currentYear = year;
            int currentMonth = month;

        
            //----------Get All Years----//
            var AllYears = db.FYears.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MyYears = new List<SelectListItem>();
            //MyYears.Add(new SelectListItem { Value = "", Text = "-All-" });
            foreach (var Yitem in AllYears)
            {
                var itemY = new SelectListItem();
                if (Yitem.YearName == currentYear)
                    itemY.Selected = true;
                itemY.Value = Yitem.YearName.ToString();
                itemY.Text = Yitem.YearName.ToString();

                //MyMonths.Add(new SelectListItem { Value = Mitem.Id.ToString(), Text = Mitem.MonthName });
                MyYears.Add(itemY);
                //MyYears.Add(new SelectListItem { Value = Yitem.YearName.ToString(), Text = Yitem.YearName.ToString() });
            }
            ViewBag.YearOptions = MyYears;

            //----------Get All Months----//
            var AllMonths = db.FMonths.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MyMonths = new List<SelectListItem>();
            //MyMonths.Add(new SelectListItem { Value = "", Text = "-All-" });
            foreach (var Mitem in AllMonths)
            {
                var itemM = new SelectListItem();
                if (Mitem.Id == currentMonth)
                    itemM.Selected = true;
                itemM.Value = Mitem.Id.ToString();
                itemM.Text = Mitem.MonthName.ToString();

                //MyMonths.Add(new SelectListItem { Value = Mitem.Id.ToString(), Text = Mitem.MonthName });
                MyMonths.Add(itemM);
            }
            ViewBag.MonthOptions = MyMonths;

            var UserName = User.Identity.Name;
            var TrimedUser = UserName.TrimStart("UNOCHA\\".ToCharArray());
            //-----------Get My Number and My Employee Id ---------------//
            double My_Number = 0;
            int My_Emp_Id = 0;
            int My_Supervisor_Id = 0;
            var myNumber = db.MyEmployeeContext.FirstOrDefault(u => u.UserName.Equals(TrimedUser, StringComparison.CurrentCultureIgnoreCase));
            if (myNumber != null)
            {
                My_Number = myNumber.PhoneNumber;
                My_Emp_Id = myNumber.Id;
                My_Supervisor_Id = myNumber.SupervisorId;
            }


            //----------Get All CalledNo----//
            var AllMyCalledNo = (from c in db.MyAllPhoneBillContext
                                 where c.CALLING_NUMBER == My_Number
                                 where c.CALLED_NUMBER != null

                                 select c).ToList();

            List<SelectListItem> MyCalledNos = new List<SelectListItem>();
            MyCalledNos.Add(new SelectListItem { Value = "", Text = "-All-" });
            foreach (var citem in AllMyCalledNo)
            {
                MyCalledNos.Add(new SelectListItem { Value = citem.CALLED_NUMBER.ToString(), Text = citem.CALLED_NUMBER.ToString() });
            }
            ViewBag.called_no = MyCalledNos;
            //var GetMyPersonalNumbers = db.MyPersonalNumContext.Where(i => i.EmployeeId == My_Emp_Id).Select(n => n.PersonalNumber).ToList();



            if (!String.IsNullOrEmpty(Request.Form["YearOptions"]))
            {
                if (!String.IsNullOrEmpty(Request.Form["MonthOptions"]))
                {
                    currentYear = Convert.ToInt32(Request.Form["YearOptions"]);
                    currentMonth = Convert.ToInt32(Request.Form["MonthOptions"]);
                    //***********Geting Years Selected *********************//
                    List<SelectListItem> MyYearsP = new List<SelectListItem>();
                    foreach (var yitems in AllYears)
                    {
                        var item = new SelectListItem();
                        if (yitems.YearName == currentYear)
                            item.Selected = true;
                        item.Value = yitems.YearName.ToString();
                        item.Text = yitems.YearName.ToString();
                        MyYearsP.Add(item);
                    }

                    ViewBag.YearOptions = MyYearsP;
                    //********Geting Months selected***************//
                    List<SelectListItem> MyMonthsP = new List<SelectListItem>();
                    foreach (var MonthItems in AllMonths)
                    {
                        var itemM = new SelectListItem();
                        if (MonthItems.Id == currentMonth)
                            itemM.Selected = true;
                        itemM.Value = MonthItems.Id.ToString();
                        itemM.Text = MonthItems.MonthName.ToString();
                        MyMonthsP.Add(itemM);
                    }

                    ViewBag.MonthOptions = MyMonthsP;
                }
            }


            var GetAllCalls = from c in db.MyAllPhoneBillContext

                              where c.CALLED_NUMBER != null
                              where c.CALLING_NUMBER == My_Number
                              where c.CALL_DATE.Year == currentYear
                              where c.CALL_DATE.Month == currentMonth
                              select new PhoneBillVM
                              {
                                  CALLING_NUMBER = c.CALLING_NUMBER,
                                  CALLED_NUMBER = c.CALLED_NUMBER,
                                  CALL_DURATION = c.CALL_DURATION,
                                  CustomTime = c.CALL_TIME.Hour + ":" + c.CALL_TIME.Minute + ":" + c.CALL_TIME.Second,
                                  CALL_COST = c.CALL_COST,
                                  CALL_DATE = c.CALL_DATE
                              };


            var GetAllPersonalCalls = from c in db.MyAllPhoneBillContext
                                      join p in db.MyPersonalNumContext on c.CALLED_NUMBER equals p.PersonalNumber
                                      where c.CALLING_NUMBER == My_Number
                                      where c.CALL_DATE.Year == currentYear
                                      where c.CALL_DATE.Month == currentMonth
                                      where c.CALLED_NUMBER != null
                                      select new PhoneBillVM
                                      {
                                          CALLING_NUMBER = c.CALLING_NUMBER,
                                          CALLED_NUMBER = c.CALLED_NUMBER,
                                          CALL_DURATION = c.CALL_DURATION,
                                          CustomTime = c.CALL_TIME.Hour + ":" + c.CALL_TIME.Minute + ":" + c.CALL_TIME.Second,
                                          CALL_COST = c.CALL_COST,
                                          CALL_DATE = c.CALL_DATE
                                      };
            //=========Sum personal calls=========//
            var TotalPersonalCost = GetAllPersonalCalls.Sum(p => p.CALL_COST);
            //---------check if called no posted--------//
            if (!string.IsNullOrEmpty(Request.Form["called_no"]))
            {
                double called_Number = Convert.ToDouble(Request.Form["called_no"]);
                GetAllCalls = GetAllCalls.Where(c => c.CALLED_NUMBER == called_Number);

                //***********Geting Years Selected *********************//
                List<SelectListItem> MyCalledNoP = new List<SelectListItem>();
                foreach (var pitems in AllMyCalledNo)
                {
                    var cpitem = new SelectListItem();
                    if (pitems.CALLED_NUMBER == called_Number)
                        cpitem.Selected = true;
                    cpitem.Value = pitems.CALLED_NUMBER.ToString();
                    cpitem.Text = pitems.CALLED_NUMBER.ToString();
                    MyCalledNoP.Add(cpitem);
                }

                ViewBag.called_no = MyCalledNoP;

                TotalPersonalCost = 0;
                ViewBag.CalledNoExist = 1;

            }
            //////////----Get all my calls this month-----//
            var TotalAllCalls = GetAllCalls.Sum(a => a.CALL_COST);

            var TotalOfficialCalls = TotalAllCalls - TotalPersonalCost;
            ViewBag.TotalCost = TotalAllCalls;
            ViewBag.TotalOfficial = TotalOfficialCalls;
            ViewBag.Total_PersonalCallCost = TotalPersonalCost;
            //========Inbox Id==============//
            ViewBag.Inbox_ID = Inbox_id;
            //ViewBag.Submit_to_supervisor = "/Inbox/Submit_ToSupervisor?year=" + currentYear + "&month=" + currentMonth + "&EMP_ID=" + My_Emp_Id + "&EMP_Sup_Id=" + My_Supervisor_Id;

            var GetEmpPersonalNums = from p in db.MyPersonalNumContext
                                       where p.EmployeeId == emp_id
                                     select p.PersonalNumber;
            List<double> empPersonalNums = new List<double>();
            foreach (var psitems in GetEmpPersonalNums)
            {
                empPersonalNums.Add(psitems);
            }

            ViewBag.empPersonalNums = empPersonalNums;

            return View(GetAllCalls.OrderBy(d => d.CALL_DATE));
        }
        public ActionResult Submit_ToSupervisor(int year, int month, int EMP_ID, int EMP_Sup_Id)
        {
            var UserName = User.Identity.Name;
            var TrimedUser = UserName.TrimStart("UNOCHA\\".ToCharArray());

            var myNumber = db.MyEmployeeContext.FirstOrDefault(u => u.UserName.Equals(TrimedUser, StringComparison.CurrentCultureIgnoreCase));
            if (myNumber != null)
            {
                if (myNumber.Id != EMP_ID)
                {
                    return HttpNotFound();
                }
                ViewBag.MyPhonNo = myNumber.PhoneNumber;

            }
            else
            {
                return HttpNotFound();
            }
            ViewBag.year = year;
            ViewBag.month = month;
            ViewBag.EMP_ID = EMP_ID;
            ViewBag.EMP_Sub_Id = EMP_Sup_Id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitToSupervisor([Bind(Include = "Id,Message")] EmpInbox eInbox)
        {
            if (ModelState.IsValid)
            {
                var Message = Request.Form["message"];
                int year = Convert.ToInt32(Request.Form["year"]);
                int mont = Convert.ToInt32(Request.Form["month"]);
                int EMP_ID = Convert.ToInt32(Request.Form["emp_id"]);
                int EMP_Sup_Id = Convert.ToInt32(Request.Form["emp_sup_id"]);
                double EmpPhoneNo = Convert.ToDouble(Request.Form["empphoneno"]);
                eInbox.CreatedDate = DateTime.Now;
                eInbox.ModifiedDate = DateTime.Now;
                eInbox.EmployeeId = EMP_ID;
                eInbox.year = year;
                eInbox.month = mont;
                eInbox.FromId = EMP_ID;
                eInbox.EmpPhoneNumber = EmpPhoneNo;
                eInbox.ToId = EMP_Sup_Id;
                eInbox.Message = Message;
                eInbox.MessageStatus = 1;
                eInbox.SupStatus = 1;
                db.MyEmpInbox.Add(eInbox);

                db.SaveChanges();
                //return RedirectToAction("Index");
                return View("~/Views/Inbox/BillsSubmitSuccess.cshtml");
            }

            return View(eInbox);
        }
        
        public ActionResult BillsSubmitSuccess()
        {
            return View();
        }

        public ActionResult ApproveBill(int? inbox_id)
        {
            if (inbox_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpInbox eInbox = db.MyEmpInbox.Find(inbox_id);
            if (eInbox == null)
            {
                return HttpNotFound();
            }

            var UserName = User.Identity.Name;
            var TrimedUser = UserName.TrimStart("UNOCHA\\".ToCharArray());
            var myNumber = db.MyEmployeeContext.FirstOrDefault(u => u.UserName.Equals(TrimedUser, StringComparison.CurrentCultureIgnoreCase));
            if (myNumber != null)
            {
                if(myNumber.Id!=eInbox.ToId)
                {
                    return HttpNotFound();
                }
                eInbox.SupStatus = 2;
                eInbox.ModifiedDate = DateTime.Now;
                db.MyEmpInbox.Attach(eInbox);
                db.Entry(eInbox).Property(x => x.SupStatus).IsModified = true;
                db.Entry(eInbox).Property(x => x.ModifiedDate).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return HttpNotFound();
            }
 

         
        }

        public ActionResult RejectBillMessage(int? inbox_id)
        {
            if (inbox_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpInbox eInbox = db.MyEmpInbox.Find(inbox_id);
            if (eInbox == null)
            {
                return HttpNotFound();
            }

            return View(eInbox);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RejectBill([Bind(Include = "Id,Message")] EmpInbox eInbox)
        {
            if (ModelState.IsValid)
            {

                eInbox.SupStatus = 3;
                eInbox.ModifiedDate = DateTime.Now;
                db.MyEmpInbox.Attach(eInbox);
                db.Entry(eInbox).Property(x => x.SupStatus).IsModified = true;
                db.Entry(eInbox).Property(x => x.Message).IsModified = true;
                db.Entry(eInbox).Property(x => x.ModifiedDate).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eInbox);
        }

        /**
         * Resubmit message
         */
        public ActionResult ResubmitMessage(int? inbox_id)
        {
            if (inbox_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpInbox eInbox = db.MyEmpInbox.Find(inbox_id);
            if (eInbox == null)
            {
                return HttpNotFound();
            }

            return View(eInbox);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResubmitBill([Bind(Include = "Id,Message")] EmpInbox eInbox)
        {
            if (ModelState.IsValid)
            {

                eInbox.SupStatus = 4;
                eInbox.ModifiedDate = DateTime.Now;
                db.MyEmpInbox.Attach(eInbox);
                db.Entry(eInbox).Property(x => x.SupStatus).IsModified = true;
                db.Entry(eInbox).Property(x => x.Message).IsModified = true;
                db.Entry(eInbox).Property(x => x.ModifiedDate).IsModified = true;
                db.SaveChanges();
                return PartialView("~/Views/Inbox/ResubmitBillSuccess.cshtml");
            }

            return View(eInbox);
        }
        // GET: Inbox/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpInbox empInbox = db.MyEmpInbox.Find(id);
            if (empInbox == null)
            {
                return HttpNotFound();
            }
            return View(empInbox);
        }

        // GET: Inbox/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inbox/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,FromId,ToId,Message,MessageStatus,CreatedDate,ModifiedDate")] EmpInbox empInbox)
        {
            if (ModelState.IsValid)
            {
                db.MyEmpInbox.Add(empInbox);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empInbox);
        }

        // GET: Inbox/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpInbox empInbox = db.MyEmpInbox.Find(id);
            if (empInbox == null)
            {
                return HttpNotFound();
            }
            return View(empInbox);
        }

        // POST: Inbox/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,FromId,ToId,Message,MessageStatus,CreatedDate,ModifiedDate")] EmpInbox empInbox)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empInbox).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empInbox);
        }

        // GET: Inbox/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpInbox empInbox = db.MyEmpInbox.Find(id);
            if (empInbox == null)
            {
                return HttpNotFound();
            }
            return View(empInbox);
        }

        // POST: Inbox/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmpInbox empInbox = db.MyEmpInbox.Find(id);
            db.MyEmpInbox.Remove(empInbox);
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
