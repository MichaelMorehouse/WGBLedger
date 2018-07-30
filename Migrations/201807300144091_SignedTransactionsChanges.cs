namespace WGBLedger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SignedTransactionsChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BankAccounts", "Name", c => c.String(maxLength: 150));
            AlterColumn("dbo.Transactions", "Description", c => c.String(maxLength: 500));
            DropColumn("dbo.Transactions", "TransactionMethod");
            DropColumn("dbo.Transactions", "NewBalance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "NewBalance", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "TransactionMethod", c => c.Int(nullable: false));
            AlterColumn("dbo.Transactions", "Description", c => c.String());
            AlterColumn("dbo.BankAccounts", "Name", c => c.String());
        }
    }
}
