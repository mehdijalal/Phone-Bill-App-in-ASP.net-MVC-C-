using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBill.Models
{
    public class PersonalNumbers
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public double PersonalNumber { get; set; }
        public int NumberTypeId { get; set; }
    }
}