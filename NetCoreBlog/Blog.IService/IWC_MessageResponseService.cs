using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.IService
{
    public partial interface IWC_MessageResponseService
    {
        bool PostData(WC_MessageResponse model);
        bool SetDefault(int officealId, int category, int request);
    }
}
