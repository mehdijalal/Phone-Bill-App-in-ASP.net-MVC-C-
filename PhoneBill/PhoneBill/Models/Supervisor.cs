using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBill.Models
{
    public class Supervisor
    {
        public int Id { get; set; }
        public string SupervisorName { get; set; }
        public string SupervisorEmail { get; set; }
        public double SupervisorPhone { get; set; }
        public int? UnitesSectionsId { get; set; }
    }
}