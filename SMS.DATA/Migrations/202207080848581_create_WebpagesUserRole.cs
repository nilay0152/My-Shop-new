namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_WebpagesUserRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WebpagesUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WebpagesUserRoles");
        }
    }
}
