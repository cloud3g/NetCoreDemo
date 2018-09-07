
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NewApi.Data {
    public interface IRepository<TEntity> where TEntity : class 
    {

        TEntity Insert(TEntity obj);

        TEntity Update(TEntity obj);

        void Remove(TEntity obj);

        IEnumerable<TEntity> List();

        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);

        TEntity FindById(Guid id);


    }
}