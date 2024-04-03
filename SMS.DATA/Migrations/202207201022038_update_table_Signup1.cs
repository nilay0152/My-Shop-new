namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_table_Signup1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teachers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "MobileNumber", c => c.String(nullable: false));
            DropColumn("dbo.Signups", "RoleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Signups", "RoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.Teachers", "MobileNumber", c => c.String());
            AlterColumn("dbo.Teachers", "Email", c => c.String());
            AlterColumn("dbo.Teachers", "LastName", c => c.String());
            AlterColumn("dbo.Teachers", "FirstName", c => c.String());
        }
    }
}
