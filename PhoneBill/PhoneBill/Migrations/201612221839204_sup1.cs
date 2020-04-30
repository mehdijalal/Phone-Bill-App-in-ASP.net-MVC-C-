namespace PhoneBill.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sup1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllPhoneBills", "SupStatus", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AllPhoneBills", "SupStatus");
        }
    }
}
