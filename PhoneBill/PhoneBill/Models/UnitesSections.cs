using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBill.Models
{
    public class UnitesSections
    {
        public int Id { get; set; }
        public string UnitSectionName { get; set; }
        public string SectionHeadEmail { get; set; }
        public double SectionPhone { get; set; }
        public string Extension { get; set; }
        public string FinanceCode { get; set; }
        public string Remarks { get; set; }
    }
}