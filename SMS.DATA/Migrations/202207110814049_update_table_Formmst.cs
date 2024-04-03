namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_table_Formmst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FormMsts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        NavigateURL = c.String(),
                        FormAcessCode = c.String(nullable: false),
                        DisplayOrder = c.Int(),
                        CreatedBy = c.Int(),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(),
                        ParentForm = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDisplayMenu = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.FormRoleMappings", "CreatedOn", c => c.DateTime(nullable: false));
            DropColumn("dbo.FormRoleMappings", "CreateOn");
            DropColumn("dbo.FormRoleMappings", "FormName");
            DropColumn("dbo.FormRoleMappings", "RoleName");
            //DropTable("dbo.FormModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FormModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NavigateURL = c.String(),
                        FormAcessCode = c.String(),
                        DisplayOrder = c.Int(),
                        CreatedBy = c.Int(),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(),
                        ParentForm = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDisplayMenu = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.FormRoleMappings", "RoleName", c => c.String());
            AddColumn("dbo.FormRoleMappings", "FormName", c => c.String());
            AddColumn("dbo.FormRoleMappings", "CreateOn", c => c.DateTime(nullable: false));
            DropColumn("dbo.FormRoleMappings", "CreatedOn");
            DropTable("dbo.FormMsts");
        }
    }
}
