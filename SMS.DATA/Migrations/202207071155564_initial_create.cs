namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Annoucements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnnoucementDetail = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.Signups",
                c => new
                    {
                        Userid = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.Userid);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Guid(nullable: false),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        Gender = c.String(),
                        Standard = c.Int(nullable: false),
                        Email = c.String(),
                        ContactNumber = c.String(),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        MobileNumber = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Signups");
            DropTable("dbo.FormModels");
            DropTable("dbo.Annoucements");
        }
    }
}
