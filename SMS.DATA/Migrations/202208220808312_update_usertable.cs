namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_usertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "mobileNumber", c => c.String());
            AddColumn("dbo.User", "gender", c => c.String());
            AddColumn("dbo.User", "DOB", c => c.DateTime(nullable: false));
            AddColumn("dbo.User", "profileImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "profileImage");
            DropColumn("dbo.User", "DOB");
            DropColumn("dbo.User", "gender");
            DropColumn("dbo.User", "mobileNumber");
        }
    }
}
