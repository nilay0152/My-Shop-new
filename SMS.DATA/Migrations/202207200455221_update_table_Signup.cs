namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_table_Signup : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Signups", "ConfirmPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Signups", "ConfirmPassword", c => c.String());
        }
    }
}
