using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SMS.Model
{
    public partial class WebSecurity : DbContext
    {
        public WebSecurity()
            : base("name=StudentEntites")
        {
        }

        
    }
}
