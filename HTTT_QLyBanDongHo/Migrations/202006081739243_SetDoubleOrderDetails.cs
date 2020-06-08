namespace HTTT_QLyBanDongHo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetDoubleOrderDetails : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderDetails", "UnitPrice", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetails", "UnitPrice", c => c.Single());
        }
    }
}
