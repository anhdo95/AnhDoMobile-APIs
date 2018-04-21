namespace Mobile.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDiscountAccompanying : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "DiscountAccompanying", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "DiscountAccompanying");
        }
    }
}
