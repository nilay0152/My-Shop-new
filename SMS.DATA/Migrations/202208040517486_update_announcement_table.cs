namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_announcement_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Annoucements", "RoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "Gender", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "ContactNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "ContactNumber", c => c.String());
            AlterColumn("dbo.Students", "Email", c => c.String());
            AlterColumn("dbo.Students", "Gender", c => c.String());
            DropColumn("dbo.Annoucements", "RoleId");
        }
    }
}
