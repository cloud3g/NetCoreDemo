using Blog.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Repository
{
    public partial class WC_MessageResponseRepository
    {

        public bool PostData(WC_MessageResponse model)
        {
          
            //全部设置为不默认
            ExecuteSqlCommand("update \"WC_MessageResponse\" set \"IsDefault\"=false where \"OfficalAccountId\"=@id and \"MessageRule\"=@MessageRule", new NpgsqlParameter("@id", model.OfficalAccountId), new NpgsqlParameter("@MessageRule", (int)model.MessageRule ));
            //默认回复和订阅回复,且不是图文另外处理，因为他们有3种模式，但是只有一个是默认的
            if (model.Category != WeChatReplyCategory.Image && (model.MessageRule == WeChatRequestRuleEnum.Default || model.MessageRule ==WeChatRequestRuleEnum.Subscriber))
            {
                //查看数据库是否存在数据
                
                var entity = Db.Set<WC_MessageResponse>().Where(p => p.OfficalAccountId == model.OfficalAccountId && p.MessageRule == model.MessageRule && p.Category ==model.Category).FirstOrDefault();
                if (entity != null)
                {
                    //删除原来的
                    Delete(entity);
                }
            }
            //全部设置为默认
            ExecuteSqlCommand("update \"WC_MessageResponse\" set \"IsDefault\"=true where \"OfficalAccountId\"=@id and \"MessageRule\"=@MessageRule and \"Category\"=@category", new NpgsqlParameter("@id", model.OfficalAccountId), new NpgsqlParameter("@MessageRule", (int)model.MessageRule), new NpgsqlParameter("@category", (int)model.Category));
            //修改
            if (Find(m=>m.Id== model.Id)!=null)
            {             
                return Edit(model).Code== Common.ResponseCode.Success;
            }
            else
            {
                return Add(model).Code == Common.ResponseCode.Success;
            }
        }
    }
}
