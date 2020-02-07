namespace SmartInItProjekat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegexUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FurnitureSalons", "AccountNumber", c => c.String(nullable: false, maxLength: 8));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FurnitureSalons", "AccountNumber", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
