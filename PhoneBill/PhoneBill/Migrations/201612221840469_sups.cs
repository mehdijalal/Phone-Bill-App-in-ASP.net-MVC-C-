namespace PhoneBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sups : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmpInboxes", "SupStatus", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmpInboxes", "SupStatus");
        }
    }
}
