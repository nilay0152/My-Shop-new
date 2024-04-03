namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesinroletables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Signups", "RoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.WebpagesRoles", "RoleName", c => c.String());
            DropColumn("dbo.Signups", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Signups", "Role", c => c.Int(nullable: false));
            AlterColumn("dbo.WebpagesRoles", "RoleName", c => c.String(nullable: false));
            DropColumn("dbo.Signups", "RoleId");
        }
    }
}
