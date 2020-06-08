namespace HTTT_QLyBanDongHo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrder : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrderDetails");
            AddPrimaryKey("dbo.OrderDetails", new[] { "OrderID", "ProductID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.OrderDetails");
            AddPrimaryKey("dbo.OrderDetails", new[] { "ProductID", "OrderID" });
        }
    }
}
