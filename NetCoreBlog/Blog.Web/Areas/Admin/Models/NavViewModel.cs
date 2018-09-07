using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Areas.Admin.Models
{
    public class NavViewModel
    {
        public string title { get; set; }
        public string icon { get; set; }
        public string href { get; set; }
        public bool spread { get; set; }
        public List<NavViewModel> children { get; set; }
        public NavViewModel()
        {
            spread = false;
            icon = "";
        }
    }
}
