using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBill.Models
{
    public class AllPhoneBill
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
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? SupStatus { get; set; }
    }
}