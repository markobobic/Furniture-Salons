namespace SmartInItProjekat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Furnitures", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Furnitures", "PhotoType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Furnitures", "PhotoType");
            DropColumn("dbo.Furnitures", "Price");
        }
    }
}
