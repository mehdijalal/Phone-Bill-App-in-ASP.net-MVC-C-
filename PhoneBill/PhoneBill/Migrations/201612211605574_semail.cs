namespace PhoneBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class semail : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "SupervisorEmail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "SupervisorEmail", c => c.String());
        }
    }
}
