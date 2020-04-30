using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBill.Models
{
    public class EmpInbox
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public string Message { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int MessageStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? SupStatus { get; set; }
        public double? EmpPhoneNumber { get; set; }

    }
}