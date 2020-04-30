namespace PhoneBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class supervisors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Supervisors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupervisorName = c.String(),
                        SupervisorEmail = c.String(),
                        SupervisorPhone = c.Double(nullable: false),
                        UnitesSectionsId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UnitesSections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UnitSectionName = c.String(),
                        SectionHeadEmail = c.String(),
                        SectionPhone = c.Double(nullable: false),
                        Extension = c.String(),
                        FinanceCode = c.String(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UnitesSections");
            DropTable("dbo.Supervisors");
        }
    }
}
