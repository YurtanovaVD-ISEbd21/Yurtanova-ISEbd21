namespace AbstractDishShopServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DishMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DishId = c.Int(nullable: false),
                        MaterialsId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dishes", t => t.DishId, cascadeDelete: true)
                .ForeignKey("dbo.Materials", t => t.MaterialsId, cascadeDelete: true)
                .Index(t => t.DishId)
                .Index(t => t.MaterialsId);
            
            CreateTable(
                "dbo.Dishes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DishName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SClientId = c.Int(nullable: false),
                        DishId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dishes", t => t.DishId, cascadeDelete: true)
                .ForeignKey("dbo.SClients", t => t.SClientId, cascadeDelete: true)
                .Index(t => t.SClientId)
                .Index(t => t.DishId);
            
            CreateTable(
                "dbo.SClients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SClientFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaterialsName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockId = c.Int(nullable: false),
                        MaterialsId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materials", t => t.MaterialsId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.StockId)
                .Index(t => t.MaterialsId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockMaterials", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockMaterials", "MaterialsId", "dbo.Materials");
            DropForeignKey("dbo.DishMaterials", "MaterialsId", "dbo.Materials");
            DropForeignKey("dbo.SOrders", "SClientId", "dbo.SClients");
            DropForeignKey("dbo.SOrders", "DishId", "dbo.Dishes");
            DropForeignKey("dbo.DishMaterials", "DishId", "dbo.Dishes");
            DropIndex("dbo.StockMaterials", new[] { "MaterialsId" });
            DropIndex("dbo.StockMaterials", new[] { "StockId" });
            DropIndex("dbo.SOrders", new[] { "DishId" });
            DropIndex("dbo.SOrders", new[] { "SClientId" });
            DropIndex("dbo.DishMaterials", new[] { "MaterialsId" });
            DropIndex("dbo.DishMaterials", new[] { "DishId" });
            DropTable("dbo.Stocks");
            DropTable("dbo.StockMaterials");
            DropTable("dbo.Materials");
            DropTable("dbo.SClients");
            DropTable("dbo.SOrders");
            DropTable("dbo.Dishes");
            DropTable("dbo.DishMaterials");
        }
    }
}
