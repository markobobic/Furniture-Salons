namespace SmartInItProjekat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationUpdate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Furnitures", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.FurnitureSalons", "TelephoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.FurnitureSalons", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FurnitureSalons", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.FurnitureSalons", "TelephoneNumber", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Furnitures", "Name", c => c.String(maxLength: 50));
        }
    }
}
