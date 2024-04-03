namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Announcementsubject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Annoucements", "Subject", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Annoucements", "Subject");
        }
    }
}
