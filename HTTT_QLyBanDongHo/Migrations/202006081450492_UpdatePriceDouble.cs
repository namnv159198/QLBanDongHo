namespace HTTT_QLyBanDongHo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePriceDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "Total Price", c => c.Double());
            AlterColumn("dbo.Product", "Price", c => c.Double());
            AlterColumn("dbo.Product", "AfterPrice", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "AfterPrice", c => c.Single());
            AlterColumn("dbo.Product", "Price", c => c.Single());
            AlterColumn("dbo.Order", "Total Price", c => c.Single());
        }
    }
}
