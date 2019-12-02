namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOfCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Date", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Date");
        }
    }
}
