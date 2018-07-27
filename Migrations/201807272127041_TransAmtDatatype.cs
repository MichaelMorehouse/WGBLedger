namespace WGBLedger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransAmtDatatype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transactions", "Amount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "Amount", c => c.String());
        }
    }
}
