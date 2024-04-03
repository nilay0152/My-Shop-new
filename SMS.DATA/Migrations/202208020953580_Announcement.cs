namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Announcement : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Annoucements", "CreatedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Annoucements", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}
