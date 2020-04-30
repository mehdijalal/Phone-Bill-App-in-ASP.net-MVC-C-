namespace PhoneBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NumberTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberTypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonalNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        PersonalNumber = c.Double(nullable: false),
                        NumberTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PersonalNumbers");
            DropTable("dbo.NumberTypes");
        }
    }
}
