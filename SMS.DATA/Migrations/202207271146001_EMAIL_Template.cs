namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EMAIL_Template : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailFPs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TemplateCode = c.String(),
                        Name = c.String(),
                        Subject = c.String(),
                        MailBody = c.String(),
                        CC = c.String(),
                        BCC = c.String(),
                        IsActive = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        CreatedBy = c.Int(),
                        CreatedOn = c.DateTime(),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {         
            DropTable("dbo.EmailFPs");
        }
    }
}
