namespace AbstractDishShopServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigr2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Implementers", "ImplementerName", c => c.String(nullable: false));
            DropColumn("dbo.Implementers", "ImplementerFIO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Implementers", "ImplementerFIO", c => c.String(nullable: false));
            DropColumn("dbo.Implementers", "ImplementerName");
        }
    }
}
