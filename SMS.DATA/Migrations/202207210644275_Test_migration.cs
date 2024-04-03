namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test_migration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teachers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "MobileNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teachers", "MobileNumber", c => c.String());
            AlterColumn("dbo.Teachers", "Email", c => c.String());
            AlterColumn("dbo.Teachers", "LastName", c => c.String());
            AlterColumn("dbo.Teachers", "FirstName", c => c.String());
        }
    }
}
