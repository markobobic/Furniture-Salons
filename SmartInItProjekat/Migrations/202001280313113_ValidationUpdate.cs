namespace SmartInItProjekat.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Furnitures", "Code", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Furnitures", "Name", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Furnitures", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Furnitures", "Code", c => c.String(nullable: false));
        }
    }
}
