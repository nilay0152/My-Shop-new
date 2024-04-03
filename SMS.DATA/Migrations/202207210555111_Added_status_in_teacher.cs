namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_status_in_teacher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "Status");
        }
    }
}
