namespace PhoneBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inbox : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmpInboxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        FromId = c.Int(nullable: false),
                        ToId = c.Int(nullable: false),
                        Message = c.String(),
                        MessageStatus = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmpInboxes");
        }
    }
}
