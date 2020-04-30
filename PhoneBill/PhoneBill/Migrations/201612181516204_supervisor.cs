namespace PhoneBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class supervisor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "UnitesSectionsId", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "SupervisorId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "SupervisorId");
            DropColumn("dbo.Employees", "UnitesSectionsId");
        }
    }
}
