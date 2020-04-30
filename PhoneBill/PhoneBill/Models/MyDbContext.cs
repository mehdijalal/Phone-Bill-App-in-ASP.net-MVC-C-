using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhoneBill.Models
{
    public class MyDbContext:DbContext
    {
        public MyDbContext() : base("MyDbConnection")
        {

        }
        public DbSet<Employee> MyEmployeeContext { get; set; }
        public DbSet<AllPhoneBill> MyAllPhoneBillContext { get; set; }
        public DbSet<MyRoles> MyRolesContext { get; set; }

        public System.Data.Entity.DbSet<PhoneBill.Models.Status> Status { get; set; }
        public DbSet<NumberType> MyNumberTypeContext { get; set; }
        public DbSet<PersonalNumbers> MyPersonalNumContext { get; set; }

        public System.Data.Entity.DbSet<PhoneBill.Models.FYears> FYears { get; set; }

        public System.Data.Entity.DbSet<PhoneBill.Models.FMonths> FMonths { get; set; }
        public DbSet<UnitesSections> MyUnitSectionContext { get; set; }
        public DbSet<Supervisor> MySupervisorContext { get; set; }
        public DbSet<EmpInbox> MyEmpInbox { get; set; }
    }
}