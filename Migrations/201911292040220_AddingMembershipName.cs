namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMembershipName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "MembeshipName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MembershipTypes", "MembeshipName");
        }
    }
}
