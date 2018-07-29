namespace WGBLedger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrevBalanceToTransModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "PreviousBalance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "PreviousBalance");
        }
    }
}
