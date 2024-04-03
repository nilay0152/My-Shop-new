namespace SMS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public  class webpages_Roles
    {
             [Key]
        
        public int RoleId { get; set; }

        [Required]
        [StringLength(256)]
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public string RoleCode { get; set; }

        
    }
}
