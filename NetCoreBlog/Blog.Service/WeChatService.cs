using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Blog.IRepository;
using Blog.IService;

namespace Blog.Service
{
    public partial class WC_OfficalAccountsService : Service<WC_OfficalAccounts>, IWC_OfficalAccountsService
    {
        public WC_OfficalAccountsService(IWC_OfficalAccountsRepository rep) : base(rep)
        {
        }
    }
    public partial class WC_MessageResponseService : Service<WC_MessageResponse>, IWC_MessageResponseService
    {
        public WC_MessageResponseService(IWC_MessageResponseRepository rep) : base(rep)
        {
        }
    }
}
