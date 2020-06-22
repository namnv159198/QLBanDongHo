namespace HTTT_QLyBanDongHo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixBug : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "Description", c => c.String(maxLength: 255));
        }
    }
}
