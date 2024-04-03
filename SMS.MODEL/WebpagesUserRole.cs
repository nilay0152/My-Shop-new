using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    public class WebpagesUserRole
    {
        [Key]
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
