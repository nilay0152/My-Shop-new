namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_FormRoleMapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormRoleMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        FullRights = c.Boolean(nullable: false),
                        AllowMenu = c.Boolean(nullable: false),
                        AllowView = c.Boolean(nullable: false),
                        AllowInsert = c.Boolean(nullable: false),
                        AllowUpdate = c.Boolean(nullable: false),
                        AllowDelete = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreateOn = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(nullable: false),
                        UpdatedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        MenuId = c.Int(nullable: false),
                        FormName = c.String(),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FormRoleMappings");
        }
    }
}
