namespace SmartInItProjekat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Regex : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FurnitureSalons", "PIB", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.FurnitureSalons", "AccountNumber", c => c.String(nullable: false, maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FurnitureSalons", "AccountNumber", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.FurnitureSalons", "PIB", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
