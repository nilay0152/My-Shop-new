using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Model
{
    public class MenuVW
    {
        public MenuVW()
        {
            SubMenu = new List<MenuVW>();
        }
        public int Id { get; set; }
        public string FormAcessCode { get; set; }
        public string Name { get; set; }
        public int? ParentForm { get; set; }
        public string NavigateURL { get; set; }
        public string Icon { get; set; }
        public int? DisplayOrder { get; set; }
        public List<MenuVW> SubMenu { get; set; }
    }
}
