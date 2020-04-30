namespace PhoneBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yearm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FMonths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MonthName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FYears",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        YearName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FYears");
            DropTable("dbo.FMonths");
        }
    }
}
