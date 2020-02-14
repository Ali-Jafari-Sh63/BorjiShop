namespace BorjiShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EnglishName = c.String(maxLength: 50),
                        PersianName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EnglishName = c.String(),
                        PersianName = c.String(),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Email = c.String(),
                        Text = c.String(),
                        Read = c.Boolean(nullable: false),
                        Answered = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Pieces",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BrandId = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                        PieceTypeId = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        FileName = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        IsSlider = c.Boolean(nullable: false),
                        IsSold = c.Boolean(nullable: false),
                        SaleDate = c.DateTime(),
                        BuyerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Piecetypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EnglishName = c.String(),
                        PersianName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ShowShoppingCarts",
                c => new
                    {
                        PieceId = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                        DeviceName = c.String(),
                        BrandName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.PieceId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EnRoleName = c.String(nullable: false),
                        PeRoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserRole = c.Int(nullable: false),
                        Email = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        RememberMe = c.String(),
                        ActivationCode = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Devices", "BrandId", "dbo.Brands");
            DropIndex("dbo.Devices", new[] { "BrandId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.ShowShoppingCarts");
            DropTable("dbo.Piecetypes");
            DropTable("dbo.Pieces");
            DropTable("dbo.Messages");
            DropTable("dbo.Devices");
            DropTable("dbo.Brands");
        }
    }
}
