namespace WGBLedger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransDesc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "Description");
        }
    }
}
