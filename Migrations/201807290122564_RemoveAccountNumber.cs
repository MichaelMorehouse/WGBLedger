namespace WGBLedger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAccountNumber : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BankAccounts", "AccountNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankAccounts", "AccountNumber", c => c.Int(nullable: false));
        }
    }
}
