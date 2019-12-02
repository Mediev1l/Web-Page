namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpellingCorrection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "MembershipName", c => c.String());
            DropColumn("dbo.MembershipTypes", "MembeshipName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MembershipTypes", "MembeshipName", c => c.String());
            DropColumn("dbo.MembershipTypes", "MembershipName");
        }
    }
}
