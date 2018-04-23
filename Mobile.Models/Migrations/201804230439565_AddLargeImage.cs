namespace Mobile.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLargeImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "LargeImage", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "LargeImage");
        }
    }
}
