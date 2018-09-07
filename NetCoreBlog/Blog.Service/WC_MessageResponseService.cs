using Blog.IRepository;
using Blog.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Service
{
    public partial class WC_MessageResponseService
    {
        public bool PostData(WC_MessageResponse model)
        {
            return (Rep as IWC_MessageResponseRepository).PostData(model);
        }
        public bool SetDefault(int officealId, int category, int request)
        {
            Rep.ExecuteSqlCommand("update \"WC_MessageResponse\" set \"IsDefault\"=false where \"OfficalAccountId\"=@id and \"MessageRule\"=@MessageRule", new NpgsqlParameter("@id",officealId), new NpgsqlParameter("@MessageRule", request));
            var result = Rep.ExecuteSqlCommand("update \"WC_MessageResponse\" set \"IsDefault\"=true where \"OfficalAccountId\"=@id and \"MessageRule\"=@MessageRule and \"Category\"=@category", new NpgsqlParameter("@id", officealId), new NpgsqlParameter("@MessageRule", request), new NpgsqlParameter("@category", category));
            return result > 0;
        }
    }
}
