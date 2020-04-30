using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBill.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public double PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        //public string SupervisorEmail { get; set; }
        public int StatusId { get; set; }
        public int UnitesSectionsId { get; set; }
        public int SupervisorId { get; set; }
        //----***Supervisor****----//
        public string SupervisorName { get; set; }
        public string SupervisorEmail { get; set; }
        public double SupervisorPhone { get; set; }

    }
}