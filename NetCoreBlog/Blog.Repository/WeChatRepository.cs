using Blog.IRepository;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository
{
    public partial class WC_OfficalAccountsRepository : Repository<WC_OfficalAccounts>, IWC_OfficalAccountsRepository
    {
        public WC_OfficalAccountsRepository(BlogDbContext db) : base(db)
        {
        }
    }
    public partial class WC_MessageResponseRepository : Repository<WC_MessageResponse>, IWC_MessageResponseRepository
    {
        public WC_MessageResponseRepository(BlogDbContext db) : base(db)
        {
        }
    }
}
