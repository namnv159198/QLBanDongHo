namespace HTTT_QLyBanDongHo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscountDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Discount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "Discount", c => c.Int());
        }
    }
}
