namespace PhoneBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bi : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "PhoneNumber", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "PhoneNumber", c => c.Int(nullable: false));
        }
    }
}
