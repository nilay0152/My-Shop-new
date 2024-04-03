using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    public class FormRoleMapping
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Role Id")]
        [Required]
        public int RoleId { get; set; }
        [Display(Name = "Full Right")]
        public bool FullRights { get; set; }
        [Display(Name = "Allow Menu")]
        public bool AllowMenu { get; set; }
        [Display(Name = "Allow View")]
        public bool AllowView { get; set; }
        [Display(Name = "Allow Insert")]
        public bool AllowInsert { get; set; }
        [Display(Name = "Allow Update")]
        public bool AllowUpdate { get; set; }
        [Display(Name = "Allow Delete")]
        public bool AllowDelete { get; set; }
        [Display(Name = "Created By")]
        public int CreatedBy { get; set; }
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Update By")]
        public int UpdatedBy { get; set; }
        [Display(Name = "Update On")]
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
        public int MenuId { get; set; }
        [NotMapped]
        [Display(Name = "Form Name ")]
        public string FormName { get; set; }
        [NotMapped]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}
