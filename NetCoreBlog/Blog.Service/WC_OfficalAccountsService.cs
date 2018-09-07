using Blog.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Service
{
    public partial class WC_OfficalAccountsService
    {
        public  Response SetDefault(int id)
        {
            Response res;
            string sql = "update \"WC_OfficalAccounts\" set \"IsDefault\" =false;update \"WC_OfficalAccounts\" set \"IsDefault\"=true where \"Id\"=@id";
            var result = Rep.ExecuteSqlCommand(sql, new NpgsqlParameter("@id", id));
            if (result > 0)
            {
                res = new Response() { Code= ResponseCode.Success, Message="设置成功！" };
            }
            else
            {
                res = new Response() { Code = ResponseCode.Fail, Message = "设置失败！" };
            }
            return res;
        }
    }
}
