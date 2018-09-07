
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace NewApi.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        
        protected NewApiContext db;
        protected DbSet<TEntity> dbSet;

        public Repository(NewApiContext _newApiContext)
        {
            db = _newApiContext;
            dbSet = db.Set<TEntity>();
        }

        public void Commit() 
        {
            db.SaveChanges();
        }

        public TEntity FindById(Guid id)
        {
            return dbSet.Find(id);
        }

        public TEntity Insert(TEntity obj)
        {
            return dbSet.Add(obj).Entity;
        }

        public IEnumerable<TEntity> List()
        {
            return dbSet.ToList();
        }

        public void Remove(TEntity obj)
        {
            dbSet.Remove(obj);
        }

        public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public TEntity Update(TEntity obj)
        {
            var entry = db.Entry(obj);
            dbSet.Attach(obj);
            entry.State = EntityState.Modified;
            return obj;
        }
    }
}
