namespace PhoneBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        FullName = c.String(),
                        UserName = c.String(),
                        EmailAddress = c.String(),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AllPhoneBills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ACCT_ID = c.Double(),
                        CALLING_NUMBER = c.Double(),
                        CALLED_NUMBER = c.Double(),
                        CALL_DURATION = c.Double(),
                        CALL_DATE = c.Double(),
                        CALL_TIME = c.DateTime(nullable: false),
                        CALL_COST = c.Double(),
                        TAX = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MyRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MyRoles");
            DropTable("dbo.AllPhoneBills");
            DropTable("dbo.Employees");
        }
    }
}
