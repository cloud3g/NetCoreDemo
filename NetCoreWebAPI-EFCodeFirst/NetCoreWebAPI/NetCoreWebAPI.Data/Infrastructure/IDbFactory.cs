using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        NetCoreWebAPIDbContext Init();
    }
}
