namespace PhoneBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "SupervisorEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "SupervisorEmail");
        }
    }
}
