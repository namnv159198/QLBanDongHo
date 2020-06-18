namespace HTTT_QLyBanDongHo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFixEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Email", "Content", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Email", "Content", c => c.String(maxLength: 255));
        }
    }
}
