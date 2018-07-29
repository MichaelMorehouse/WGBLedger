namespace WGBLedger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewBalanceToTransModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "NewBalance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "NewBalance");
        }
    }
}
