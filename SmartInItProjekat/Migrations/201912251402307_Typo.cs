namespace SmartInItProjekat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Typo : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Furnitures", name: "FurnitureShopId", newName: "FurnitureSalonId");
            RenameIndex(table: "dbo.Furnitures", name: "IX_FurnitureShopId", newName: "IX_FurnitureSalonId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Furnitures", name: "IX_FurnitureSalonId", newName: "IX_FurnitureShopId");
            RenameColumn(table: "dbo.Furnitures", name: "FurnitureSalonId", newName: "FurnitureShopId");
        }
    }
}
