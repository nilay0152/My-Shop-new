using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SMS.Model
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=StudentEntites")
        {
        }

        //public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
