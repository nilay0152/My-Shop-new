namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_userProfile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        userId = c.Int(nullable: false),
                        userName = c.String(),
                        Email = c.String(),
                        mobileNumber = c.String(),
                        gender = c.String(),
                        DOB = c.DateTime(nullable: false),
                        profileImage = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProfiles");
        }
    }
}
