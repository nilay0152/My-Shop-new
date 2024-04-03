namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_WebpagesRole_UserRoleMapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserRoleMappings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.WebpagesRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RoleCode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            //AddColumn("dbo.Signups", "Role", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Signups", "Role");
            DropTable("dbo.WebpagesRoles");
            DropTable("dbo.UserRoleMappings");
        }
    }
}
