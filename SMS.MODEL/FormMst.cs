using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SMS.Model
{
   public  class FormMst
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Display(Name = "Navigate URL")]
        public string NavigateURL { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string FormAcessCode { get; set; }

        [Display(Name = "Display Order")]
        public int? DisplayOrder { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        [Display(Name = "Parent form")]
        public int? ParentForm { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        public bool IsDisplayMenu { get; set; }
       
        


    }
}
