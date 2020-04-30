namespace PhoneBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datechange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AllPhoneBills", "CALL_DATE", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AllPhoneBills", "Year", c => c.Int());
            AlterColumn("dbo.AllPhoneBills", "Month", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AllPhoneBills", "Month", c => c.Int(nullable: false));
            AlterColumn("dbo.AllPhoneBills", "Year", c => c.Int(nullable: false));
            AlterColumn("dbo.AllPhoneBills", "CALL_DATE", c => c.Double());
        }
    }
}
