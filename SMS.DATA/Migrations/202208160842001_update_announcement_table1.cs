namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_announcement_table1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Annoucements", "Subject", c => c.String(nullable: false));
            AlterColumn("dbo.Annoucements", "AnnoucementDetail", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Annoucements", "AnnoucementDetail", c => c.String());
            AlterColumn("dbo.Annoucements", "Subject", c => c.String());
        }
    }
}
