namespace SMS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public  class webpages_UsersInRoles
    {
        [Key]
        public int Id { get; set; }
        
        public int UserId { get; set; }       
        
        public int RoleId { get; set; }
       
    }
}
