namespace CraveCart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Foods", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Foods", "ImageUrl", c => c.String(nullable: false));
            AlterColumn("dbo.Foods", "Ingredients", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurants", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurants", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurants", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurants", "ImageUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "ImageUrl", c => c.String());
            AlterColumn("dbo.Restaurants", "Phone", c => c.String());
            AlterColumn("dbo.Restaurants", "Address", c => c.String());
            AlterColumn("dbo.Restaurants", "Name", c => c.String());
            AlterColumn("dbo.Foods", "Ingredients", c => c.String());
            AlterColumn("dbo.Foods", "ImageUrl", c => c.String());
            AlterColumn("dbo.Foods", "Name", c => c.String());
        }
    }
}
