using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Filter
{
    public class SetActionAttribute:Attribute
    {
        public string ActionName { get; set; }
    }
}
