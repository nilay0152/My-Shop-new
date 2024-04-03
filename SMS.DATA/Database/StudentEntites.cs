using SMS.Model;
using System;
using System.Data.Entity;
using System.Linq;

namespace SMS.Data.Database
{
    public class StudentEntites : DbContext
    {
        // Your context has been configured to use a 'StudentEntites' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SMS.Data.Database.StudentEntites' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'StudentEntites' 
        // connection string in the application configuration file.
        public StudentEntites()
            : base("name=StudentEntites")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Annoucement> annoucements { get; set; }    
        public DbSet<Student> students { get; set; }
        public DbSet<FormMst> formModel { get; set; }      
        public DbSet<FormRoleMapping> FormRoleMappings { get; set; }
        public DbSet<webpages_Roles> webpages_Roles { get; set; }
        public DbSet<webpages_UsersInRoles> webpages_UsersInRoles { get; set; }
        public DbSet<User> usersProfile { get; set; }
        public DbSet<webpages_Membership> webpages_Memberships { get; set; }
        public DbSet<webpages_OAuthMembership> webpages_OAuthMemberships { get; set; }
        public DbSet<EmailFP> emailtemplate { get; set; }
        public DbSet<UserProfile> profile { get; set; }


        public override int SaveChanges()
        {
         
            foreach (var entry in ChangeTracker.Entries())
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;

                    entity.GetType().GetProperty("Status").SetValue(entity, false) ;
                }
            }
            return base.SaveChanges();
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}