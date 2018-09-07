using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Data.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private NetCoreWebAPIDbContext dbContext;
        private readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected NetCoreWebAPIDbContext DbContext
        {
            get { return dbContext ?? (dbContext = DbFactory.Init()); }
        }

        protected Repository(IDbFactory _dbFactory)
        {
            DbFactory = _dbFactory;
            dbSet = DbContext.Set<T>();
        }
        public virtual List<T> GetALL(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public virtual T GetSingleByID(int id)
        {
            return dbSet.Find(id);
        }

        public virtual bool DeleteByID(int id)
        {
            var entity = dbSet.Find(id);
            if(entity == null)
            {
                return false;
            }
            dbSet.Remove(entity);

            int checkDelete = dbContext.SaveChanges();
            if (checkDelete > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
