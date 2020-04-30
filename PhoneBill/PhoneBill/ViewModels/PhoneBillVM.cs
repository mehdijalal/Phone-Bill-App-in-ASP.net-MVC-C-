using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBill.ViewModels
{
    public class PhoneBillVM
    {
        public int Id { get; set; }
        public double? ACCT_ID { get; set; }
        public double? CALLING_NUMBER { get; set; }
        public double? CALLED_NUMBER { get; set; }
        public double? CALL_DURATION { get; set; }
        [DataType(DataType.Date)]
        public DateTime CALL_DATE { get; set; }
        [DataType(DataType.Time)]
        public DateTime CALL_TIME { get; set; }
        public double? CALL_COST { get; set; }
        public double? TAX { get; set; }
        public string CustomTime { get; set; }
        //---Employee Relation----//
        //public int PhoneNumber { get; set; }
        public double PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public int? StatusId { get; set; }
        //----PersonalNumbers-----//
        public int EmployeeId { get; set; }
        public double PersonalNumber { get; set; }
    }
}