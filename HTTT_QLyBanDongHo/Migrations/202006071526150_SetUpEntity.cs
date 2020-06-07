namespace HTTT_QLyBanDongHo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetUpEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 255, unicode: false),
                        UserName = c.String(maxLength: 255, unicode: false),
                        Email = c.String(maxLength: 255, unicode: false),
                        Password = c.String(maxLength: 255, unicode: false),
                        CreateAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 255, unicode: false),
                        Name = c.String(maxLength: 255),
                        Email = c.String(maxLength: 255, unicode: false),
                        Address = c.String(maxLength: 255),
                        Phonenumber = c.String(maxLength: 255, unicode: false),
                        Gender = c.String(maxLength: 255),
                        Birthday = c.DateTime(storeType: "date"),
                        YearOld = c.Int(),
                        CreateAt = c.DateTime(),
                        CustomerTypeID = c.Int(nullable: false),
                        AccountID = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CustomerType", t => t.CustomerTypeID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .Index(t => t.CustomerTypeID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.CustomerType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 255, unicode: false),
                        TotalQuantity = c.Int(name: "Total Quantity"),
                        TotalPrice = c.Single(name: "Total Price"),
                        Discount = c.Int(),
                        CreateAt = c.DateTime(name: "Create At"),
                        CustomerID = c.String(nullable: false, maxLength: 255, unicode: false),
                        OrderStatusID = c.Int(nullable: false),
                        PaymentTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.OrderStatus", t => t.OrderStatusID)
                .ForeignKey("dbo.PaymentType", t => t.PaymentTypeID)
                .ForeignKey("dbo.Customer", t => t.CustomerID)
                .Index(t => t.CustomerID)
                .Index(t => t.OrderStatusID)
                .Index(t => t.PaymentTypeID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        OrderID = c.String(nullable: false, maxLength: 255, unicode: false),
                        Quantity = c.Int(),
                        UnitPrice = c.Single(),
                    })
                .PrimaryKey(t => new { t.ProductID, t.OrderID })
                .ForeignKey("dbo.Product", t => t.ProductID)
                .ForeignKey("dbo.Order", t => t.OrderID)
                .Index(t => t.ProductID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Price = c.Single(),
                        AfterPrice = c.Single(),
                        Thumbnails = c.String(maxLength: 255, unicode: false),
                        Discount = c.Int(),
                        isBestSeller = c.Int(),
                        isNew = c.Int(),
                        isSpecial = c.Int(),
                        Description = c.String(maxLength: 255),
                        Status = c.String(maxLength: 255),
                        CreateAt = c.DateTime(),
                        ManufactureID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.CategoryID)
                .ForeignKey("dbo.Manufacture", t => t.ManufactureID)
                .Index(t => t.ManufactureID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Manufacture",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Logo = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PaymentType",
                c => new
                    {
                        PaymentTypeID = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.PaymentTypeID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 255, unicode: false),
                        Name = c.String(maxLength: 255),
                        Gender = c.String(maxLength: 255, unicode: false),
                        Birthday = c.String(maxLength: 255, unicode: false),
                        YearOld = c.Int(),
                        Address = c.String(maxLength: 255),
                        Status = c.String(maxLength: 255),
                        RoleID = c.String(nullable: false, maxLength: 255, unicode: false),
                        AccountID = c.String(nullable: false, maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.RoleID)
                .ForeignKey("dbo.Account", t => t.AccountID)
                .Index(t => t.RoleID)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 255, unicode: false),
                        RoleName = c.String(name: "Role Name", maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "AccountID", "dbo.Account");
            DropForeignKey("dbo.User", "RoleID", "dbo.Role");
            DropForeignKey("dbo.Customer", "AccountID", "dbo.Account");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Order", "PaymentTypeID", "dbo.PaymentType");
            DropForeignKey("dbo.Order", "OrderStatusID", "dbo.OrderStatus");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Order");
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Product", "ManufactureID", "dbo.Manufacture");
            DropForeignKey("dbo.Product", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.Customer", "CustomerTypeID", "dbo.CustomerType");
            DropIndex("dbo.User", new[] { "AccountID" });
            DropIndex("dbo.User", new[] { "RoleID" });
            DropIndex("dbo.Product", new[] { "CategoryID" });
            DropIndex("dbo.Product", new[] { "ManufactureID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropIndex("dbo.OrderDetails", new[] { "ProductID" });
            DropIndex("dbo.Order", new[] { "PaymentTypeID" });
            DropIndex("dbo.Order", new[] { "OrderStatusID" });
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropIndex("dbo.Customer", new[] { "AccountID" });
            DropIndex("dbo.Customer", new[] { "CustomerTypeID" });
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.PaymentType");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.Manufacture");
            DropTable("dbo.Category");
            DropTable("dbo.Product");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Order");
            DropTable("dbo.CustomerType");
            DropTable("dbo.Customer");
            DropTable("dbo.Account");
        }
    }
}
