namespace WGBLedger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameAndRefactor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "DateCreated", c => c.DateTime(nullable: false));
            DropColumn("dbo.BankAccounts", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankAccounts", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.BankAccounts", "DateCreated");
        }
    }
}
