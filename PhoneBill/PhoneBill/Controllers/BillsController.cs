using PhoneBill.MJLib;
using PhoneBill.Models;
using PhoneBill.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace PhoneBill.Controllers
{
    public class BillsController : Controller
    {
        private MyDbContext db = new MyDbContext();
        // GET: Bills
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

            //----------Get All Employees----//
            var AllEmployee = db.MyEmployeeContext.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MyEmployee = new List<SelectListItem>();
            MyEmployee.Add(new SelectListItem { Value = "", Text = "-All-" });
            foreach (var EmpItem in AllEmployee)
            {
                MyEmployee.Add(new SelectListItem { Value = EmpItem.Id.ToString(), Text = EmpItem.FullName });
            }
            ViewBag.EmployeeOptions = MyEmployee;

            //----------Get All Status----//
            var AllStatus = db.Status.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MyStatus = new List<SelectListItem>();
            MyStatus.Add(new SelectListItem { Value = "", Text = "-All-" });
            foreach (var SItem in AllStatus)
            {
                MyStatus.Add(new SelectListItem { Value = SItem.Id.ToString(), Text = SItem.StatusName });
            }
            ViewBag.StatusOptions = MyStatus;

            var S_Total = from s in db.MyAllPhoneBillContext
                          join st in db.MyEmployeeContext on s.CALLING_NUMBER equals st.PhoneNumber
                   

                          select new PhoneBillVM
                          {
                              Id = s.Id,
                              FullName = st.FullName,
                              UserName = st.UserName,
                              EmailAddress = st.EmailAddress,
                              CALLING_NUMBER = s.CALLING_NUMBER,
                              CALLED_NUMBER = s.CALLED_NUMBER,
                              CALL_DURATION = s.CALL_DURATION,
                              CALL_DATE = s.CALL_DATE,
                              CALL_TIME = s.CALL_TIME,
                              CALL_COST = s.CALL_COST
                           
                          };
            int NumRows = S_Total.Count();

            var MyVM = (from s in db.MyAllPhoneBillContext
                       join st in db.MyEmployeeContext on s.CALLING_NUMBER equals st.PhoneNumber


                       select new PhoneBillVM
                       {
                           Id = s.Id,
                           FullName = st.FullName,
                           UserName = st.UserName,
                           EmailAddress = st.EmailAddress,
                           CALLING_NUMBER = s.CALLING_NUMBER,
                           CALLED_NUMBER = s.CALLED_NUMBER,
                           CALL_DURATION = s.CALL_DURATION,
                           CALL_DATE = s.CALL_DATE,
                           CALL_TIME = s.CALL_TIME,
                           CALL_COST = s.CALL_COST

                       

        }).AsEnumerable().OrderByDescending(o => o.Id).Skip(starting).Take(pageNum);

            string links = Pagination.paginate(NumRows, starting, pageNum, "", "page", strpost);
            ViewBag.Start = starting;
            ViewBag.total_rec = NumRows;
            ViewBag.link = links;
            if (Request.IsAjaxRequest())
            {
                return PartialView("Ajax_Index", MyVM);
            }
            return View(MyVM);
        }

        public ActionResult MyCalls()
        {
            var UserName = User.Identity.Name;
            var TrimedUser = UserName.TrimStart("UNOCHA\\".ToCharArray());
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

            //----------Get All Employees----//
            var AllEmployee = db.MyEmployeeContext.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MyEmployee = new List<SelectListItem>();
            MyEmployee.Add(new SelectListItem { Value = "", Text = "-All-" });
            foreach (var EmpItem in AllEmployee)
            {
                MyEmployee.Add(new SelectListItem { Value = EmpItem.Id.ToString(), Text = EmpItem.FullName });
            }
            ViewBag.EmployeeOptions = MyEmployee;

            //----------Get All Status----//
            var AllStatus = db.Status.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MyStatus = new List<SelectListItem>();
            MyStatus.Add(new SelectListItem { Value = "", Text = "-All-" });
            foreach (var SItem in AllStatus)
            {
                MyStatus.Add(new SelectListItem { Value = SItem.Id.ToString(), Text = SItem.StatusName });
            }
            ViewBag.StatusOptions = MyStatus;

            //-----------Get My Number ---------------//
            double My_Number = 0;
            var myNumber = db.MyEmployeeContext.FirstOrDefault(u => u.UserName.Equals(TrimedUser, StringComparison.CurrentCultureIgnoreCase));
            if(myNumber!=null)
            {
                My_Number = myNumber.PhoneNumber;
            }
            var S_Total = from s in db.MyAllPhoneBillContext
                          where s.CALLING_NUMBER == My_Number
                          where s.CALLED_NUMBER!=null
                          select new PhoneBillVM
                          {
                              Id = s.Id,
                              CALLING_NUMBER = s.CALLING_NUMBER,
                              CALLED_NUMBER = s.CALLED_NUMBER,
                              CALL_DURATION = s.CALL_DURATION,
                              CALL_DATE = s.CALL_DATE,
                              CALL_TIME = s.CALL_TIME,
                              CALL_COST = s.CALL_COST

                          };
            int NumRows = S_Total.Count();

            var MyVM = (from s in db.MyAllPhoneBillContext
                        where s.CALLING_NUMBER == My_Number
                        where s.CALLED_NUMBER != null
                        select new PhoneBillVM
                        {
                            Id = s.Id,
                            CALLING_NUMBER = s.CALLING_NUMBER,
                            CALLED_NUMBER = s.CALLED_NUMBER,
                            CALL_DURATION = s.CALL_DURATION,
                            CALL_DATE = s.CALL_DATE,
                            CALL_TIME = s.CALL_TIME,
                            CALL_COST = s.CALL_COST
                        }).AsEnumerable().OrderByDescending(o => o.Id).Skip(starting).Take(pageNum);



            string links = Pagination.paginate(NumRows, starting, pageNum, "", "page", strpost);
            ViewBag.Start = starting;
            ViewBag.total_rec = NumRows;
            ViewBag.link = links;
            if (Request.IsAjaxRequest())
            {
                return PartialView("Ajax_Index", MyVM);
            }
            return View(MyVM);
        }

        public ActionResult GroupMyCalls()
        {

            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            if (currentMonth == 1)
            {
                currentMonth = 12;
                currentYear = currentYear - 1;

            }
            else
            {
                currentMonth = currentMonth - 1;
            }
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
            string PageNumber = WebConfigurationManager.AppSettings["pageNumber"];
            int pageNum = Convert.ToInt16(PageNumber);
            //=============Details==========================//
            int starting = 0;
            if (Request.Form["starting"] != null)
            {
                starting = Convert.ToInt32(Request.Form["starting"]);
            }

            //----------Get All Status----//
            var AllStatus = db.Status.ToList();
            //IEnumerable<SelectListItem> items = db.Provinces.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name });
            List<SelectListItem> MyStatus = new List<SelectListItem>();
            MyStatus.Add(new SelectListItem { Value = "", Text = "-All-" });
            foreach (var SItem in AllStatus)
            {
                MyStatus.Add(new SelectListItem { Value = SItem.Id.ToString(), Text = SItem.StatusName });
            }
            ViewBag.StatusOptions = MyStatus;

            //-----------Get My Number ---------------//
            double My_Number = 0;
            int My_Emp_Id = 0;
            var myNumber = db.MyEmployeeContext.FirstOrDefault(u => u.UserName.Equals(TrimedUser, StringComparison.CurrentCultureIgnoreCase));
            if (myNumber != null)
            {
                My_Number = myNumber.PhoneNumber;
                My_Emp_Id = myNumber.Id;
            }
          

      
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
            var DistinctNos = db.MyAllPhoneBillContext
                .Where(n => n.CALLING_NUMBER == My_Number)
                .Where(m => m.CALLED_NUMBER != null)
                .Where(y => y.CALL_DATE.Year == currentYear)
                .Where(mn => mn.CALL_DATE.Month == currentMonth)
                .Select(e => new { e.CALLING_NUMBER, e.CALLED_NUMBER }).Distinct();
            List<PhoneBillVM> FModel = new List<PhoneBillVM>();
            if (DistinctNos!=null)
            {
                foreach (var items in DistinctNos)
                {
                    FModel.Add(new PhoneBillVM { CALLED_NUMBER = items.CALLED_NUMBER, CALLING_NUMBER = items.CALLING_NUMBER });
                }
            }

            var MyExistingPersonalNo = from p in db.MyPersonalNumContext
                                       where p.EmployeeId == My_Emp_Id
                                       select p.PersonalNumber;
            List<double> myPersonalNums = new List<double>();
            foreach(var psitems in MyExistingPersonalNo)
            {
                myPersonalNums.Add(psitems);
            }

            ViewBag.PersonalNums = myPersonalNums;
            return View(FModel);
        }

        public ActionResult SavePersonalCalles(List<double> PostedPersonalNumbers)
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
          
            if(PostedPersonalNumbers!=null)
            {
                //----**********-----Get My Existing Personal Numbers-----********-----//
                var QueryMyExistingPersonalNumbers = db.MyPersonalNumContext.Where(i => i.EmployeeId == My_Emp_Id).Select(f => f.PersonalNumber).ToArray();
                //---########----We get only the new numbers excloding existing numbers ---######---//
                List<double> OnlyNewPersonalNums = PostedPersonalNumbers.Except(QueryMyExistingPersonalNumbers).ToList();

                //=======Now we add new personal numbers===========//
                List<PersonalNumbers> MyPersonalNumbers = new List<PersonalNumbers>();

                foreach (var item in OnlyNewPersonalNums)
                {
                    MyPersonalNumbers.Add(new PersonalNumbers { PersonalNumber = item, EmployeeId = My_Emp_Id,NumberTypeId=2 });
                }

                db.MyPersonalNumContext.AddRange(MyPersonalNumbers);
                db.SaveChanges();
            }
            else
            {
                return View();
            }
            return View();
        }

        public ActionResult MyPersonalCallCost()
        {
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            if (currentMonth == 1)
            {
                currentMonth = 12;
                currentYear = currentYear - 1;

            }
            else
            {
                currentMonth = currentMonth - 1;
            }
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
            var myNumber = db.MyEmployeeContext.FirstOrDefault(u => u.UserName.Equals(TrimedUser, StringComparison.CurrentCultureIgnoreCase));
            if (myNumber != null)
            {
                My_Number = myNumber.PhoneNumber;
                My_Emp_Id = myNumber.Id;
            }

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
                                      where c.CALL_DATE.Year==currentYear
                                      where c.CALL_DATE.Month==currentMonth
                                      select new PhoneBillVM
                                      {
                                          CALLING_NUMBER = c.CALLING_NUMBER,
                                          CALLED_NUMBER = c.CALLED_NUMBER,
                                          CALL_DURATION = c.CALL_DURATION,
                                          CustomTime = c.CALL_TIME.Hour + ":" + c.CALL_TIME.Minute + ":" + c.CALL_TIME.Second,
                                          CALL_COST = c.CALL_COST,
                                          CALL_DATE = c.CALL_DATE
                                      };
            ////////----Get all my calls this month-----//
            var TotalAllCalls = GetAllCalls.Sum(a => a.CALL_COST);
            var TotalPersonalCost = GetAllPersonalCalls.Sum(c => c.CALL_COST);

            var TotalOfficialCalls = TotalAllCalls - TotalPersonalCost;
            ViewBag.TotalCost = TotalAllCalls;
            ViewBag.TotalOfficial = TotalOfficialCalls;
            ViewBag.Total_PersonalCallCost = TotalPersonalCost;
            return View(GetAllPersonalCalls);
        }

       
        
        public ActionResult RemoveFromPersonal(double? PersonalNumber)
        {
            var UserName = User.Identity.Name;
            var TrimedUser = UserName.TrimStart("UNOCHA\\".ToCharArray());

            //-----------Get My Number ---------------//
            double My_Number = 0;
            int My_Emp_Id = 0;
            var myNumber = db.MyEmployeeContext.FirstOrDefault(u => u.UserName.Equals(TrimedUser, StringComparison.CurrentCultureIgnoreCase));
            if (myNumber != null)
            {
                My_Number = myNumber.PhoneNumber;
                My_Emp_Id = myNumber.Id;
            }

            if (PersonalNumber == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //PersonalNumbers personalNumbers = db.MyPersonalNumContext.Find();
            var MyPersonalNum = (from p in db.MyPersonalNumContext
                                where p.EmployeeId == My_Emp_Id
                                where p.PersonalNumber == PersonalNumber
                                select new PersonalNumberVM
                                {
                                    Id = p.Id,
                                    EmployeeId = p.EmployeeId,
                                    PersonalNumber = p.PersonalNumber,
                                    NumberTypeId = p.NumberTypeId
                                }).FirstOrDefault();
            if (MyPersonalNum == null)
            {
                return HttpNotFound();
            }

            return View(MyPersonalNum);
         
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveFromPersonalConfirmed()
        {
            int id = Convert.ToInt32(Request.Form["Id"]);
          
            var UserName = User.Identity.Name;
            var TrimedUser = UserName.TrimStart("UNOCHA\\".ToCharArray());

            //-----------Get My Number ---------------//
            double My_Number = 0;
            int My_Emp_Id = 0;
            var myNumber = db.MyEmployeeContext.FirstOrDefault(u => u.UserName.Equals(TrimedUser, StringComparison.CurrentCultureIgnoreCase));
            if (myNumber != null)
            {
                My_Number = myNumber.PhoneNumber;
                My_Emp_Id = myNumber.Id;
            }

            PersonalNumbers pn = db.MyPersonalNumContext.Where(c => c.EmployeeId == My_Emp_Id).Where(i => i.Id == id).FirstOrDefault();
            db.MyPersonalNumContext.Remove(pn);
            db.SaveChanges();
            return RedirectToAction("GroupMyCalls");
        }
        public ActionResult choesen()
        {
            return View();
        }
        public ActionResult CalculateAllCalls()
        {
            //var base_url = HttpContext.Request.Url.Authority;


            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;

            if (currentMonth == 1)
            {
                currentMonth = 12;
                currentYear = currentYear - 1;

            }
            else
            {
                currentMonth = currentMonth - 1;
            }
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

            ViewBag.Submit_to_supervisor = "/Inbox/Submit_ToSupervisor?year=" + currentYear + "&month=" + currentMonth+"&EMP_ID="+My_Emp_Id+"&EMP_Sup_Id="+ My_Supervisor_Id;

            var MyExistingPersonalNo = from p in db.MyPersonalNumContext
                                       where p.EmployeeId == My_Emp_Id
                                       select p.PersonalNumber;
            List<double> myPersonalNums = new List<double>();
            foreach (var psitems in MyExistingPersonalNo)
            {
                myPersonalNums.Add(psitems);
            }

            ViewBag.PersonalNums = myPersonalNums;

            return View(GetAllCalls.OrderBy(d => d.CALL_DATE));
        }
        
        public ActionResult Main ()
        {
            return View();
        }
    }
}