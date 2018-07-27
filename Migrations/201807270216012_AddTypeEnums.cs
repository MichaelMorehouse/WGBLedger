namespace WGBLedger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTypeEnums : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "BankAccount_Id", c => c.Guid());
            AlterColumn("dbo.BankAccounts", "AccountType", c => c.Int(nullable: false));
            AlterColumn("dbo.Transactions", "TransactionType", c => c.Int(nullable: false));
            AlterColumn("dbo.Transactions", "TransactionMethod", c => c.Int(nullable: false));
            CreateIndex("dbo.Transactions", "BankAccount_Id");
            AddForeignKey("dbo.Transactions", "BankAccount_Id", "dbo.BankAccounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "BankAccount_Id", "dbo.BankAccounts");
            DropIndex("dbo.Transactions", new[] { "BankAccount_Id" });
            AlterColumn("dbo.Transactions", "TransactionMethod", c => c.String());
            AlterColumn("dbo.Transactions", "TransactionType", c => c.String());
            AlterColumn("dbo.BankAccounts", "AccountType", c => c.String());
            DropColumn("dbo.Transactions", "BankAccount_Id");
        }
    }
}
