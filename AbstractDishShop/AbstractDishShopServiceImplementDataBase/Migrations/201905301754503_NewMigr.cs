namespace AbstractDishShopServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Implementers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImplementerName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SOrders", "ImplementerId", c => c.Int());
            CreateIndex("dbo.SOrders", "ImplementerId");
            AddForeignKey("dbo.SOrders", "ImplementerId", "dbo.Implementers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SOrders", "ImplementerId", "dbo.Implementers");
            DropIndex("dbo.SOrders", new[] { "ImplementerId" });
            DropColumn("dbo.SOrders", "ImplementerId");
            DropTable("dbo.Implementers");
        }
    }
}
