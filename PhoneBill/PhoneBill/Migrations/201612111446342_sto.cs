namespace PhoneBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllPhoneBills", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.AllPhoneBills", "Month", c => c.Int(nullable: false));
            AlterColumn("dbo.FYears", "YearName", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FYears", "YearName", c => c.String());
            DropColumn("dbo.AllPhoneBills", "Month");
            DropColumn("dbo.AllPhoneBills", "Year");
        }
    }
}
