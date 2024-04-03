namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class status_added_anmt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Annoucements", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Annoucements", "Status");
        }
    }
}
