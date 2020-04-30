namespace PhoneBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inbox2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmpInboxes", "year", c => c.Int(nullable: false));
            AddColumn("dbo.EmpInboxes", "month", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmpInboxes", "month");
            DropColumn("dbo.EmpInboxes", "year");
        }
    }
}
