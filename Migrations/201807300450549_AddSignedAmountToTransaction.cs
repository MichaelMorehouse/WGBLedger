namespace WGBLedger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSignedAmountToTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "SignedAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "SignedAmount");
        }
    }
}
