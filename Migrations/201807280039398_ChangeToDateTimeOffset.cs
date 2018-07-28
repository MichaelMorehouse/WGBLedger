namespace WGBLedger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToDateTimeOffset : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BankAccounts", "DateCreated", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Transactions", "Date", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BankAccounts", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
