namespace PhoneBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _enum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmpInboxes", "EmpPhoneNumber", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmpInboxes", "EmpPhoneNumber");
        }
    }
}
