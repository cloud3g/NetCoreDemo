using Blog.Common;
using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Blog.Repository
{
    public class Repository<T> where T : Entity
    {
        public DbContext Db { get; set; }
        public Repository(DbContext db)
        {
            Db = db;
        }
        public virtual IQueryable<T> GetList()
        {
            return Db.Set<T>();
        }

        #region 查询
        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return Db.Set<T>().FirstOrDefault(predicate);
        }
        public virtual T FindJoin(Expression<Func<T, bool>> predicate, string[] tableNames)
        {
            if (tableNames == null && tableNames.Any() == false)
            {
                throw new Exception("缺少连表名称");
            }
            IQueryable<T> query = Db.Set<T>();

            foreach (var table in tableNames)
            {
                query = query.Include(table);
            }
            return query.FirstOrDefault(predicate);
        }
        /// <summary>
        /// 单表查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> predicate)
        {
            return Db.Set<T>().Where(predicate);
        }
        /// <summary>
        /// 多表关联查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="tableNames"></param>
        /// <returns></returns>
        public virtual IQueryable<T> GetListJoin(Expression<Func<T, bool>> predicate, string[] tableNames)
        {
            if (tableNames == null && tableNames.Any() == false)
            {
                throw new Exception("缺少连表名称");
            }

            IQueryable<T> query = Db.Set<T>();

            foreach (var table in tableNames)
            {
                query = query.Include(table);
            }

            return query.Where(predicate);
        }
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <typeparam name="TKey">排序字段类型</typeparam>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="total">数据总数</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>分页列表</returns>
        public virtual IQueryable<T> GetPageList<TKey>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, bool isAsc = true)
        {
            var list = Db.Set<T>().Where(where);
            total = list.Count();
            if (isAsc)
                return list.OrderBy(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            else
                return list.OrderByDescending(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="sort">排序字段</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="total">数据总数</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns>分页列表</returns>
        public virtual IQueryable<T> GetPageList(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> where, string sort, bool isAsc = true)
        {
            var list = Db.Set<T>().Where(where);

            var _orderParames = Expression.Parameter(typeof(T), "s");
            //根据属性名获取属性
            var _property = typeof(T).GetProperty(sort);
            //创建一个访问属性的表达式
            var _propertyAccess = Expression.MakeMemberAccess(_orderParames, _property);
            var _orderByExp = Expression.Lambda(_propertyAccess, _orderParames);
            string _orderName = isAsc ? "OrderBy" : "OrderByDescending";
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), _orderName, new Type[] { typeof(T), _property.PropertyType }, list.Expression, Expression.Quote(_orderByExp));
            list = list.Provider.CreateQuery<T>(resultExp);

            total = list.Count();
            return list.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        }
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pager">分页类</param>
        /// <param name="where">查询条件</param>
        /// <returns>分页类</returns>
        public virtual GridPager<T> GetPageList(GridPager<T> pager, Expression<Func<T, bool>> where)
        {
            var list = Db.Set<T>().Where(where);

            if (pager.sort != null)
            {
                var _orderParames = Expression.Parameter(typeof(T), "s");
                //根据属性名获取属性
                var _property = typeof(T).GetProperty(pager.sort);
                //创建一个访问属性的表达式
                var _propertyAccess = Expression.MakeMemberAccess(_orderParames, _property);
                var _orderByExp = Expression.Lambda(_propertyAccess, _orderParames);
                string _orderName = pager.order.ToLower() != "desc" ? "OrderBy" : "OrderByDescending";
                MethodCallExpression resultExp = Expression.Call(typeof(Queryable), _orderName, new Type[] { typeof(T), _property.PropertyType }, list.Expression, Expression.Quote(_orderByExp));
                list = list.Provider.CreateQuery<T>(resultExp);
            }
            pager.totalRows = list.Count();
            pager.Entities = list.Skip((pager.page - 1) * pager.rows).Take(pager.rows).ToList();
            return pager;
        }

        /// <summary>
        ///  根据条件排序获取分页实体
        /// </summary>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="total">总数据量</param>
        /// <param name="where">查询条件</param>
        /// <param name="orderByExpression">排序实体(包含排序字段和排序方式)</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetPageList(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> where, params OrderModelField[] orderByExpression)
        {
            //条件过滤
            var query = this.Db.Set<T>().Where(where);

            //创建表达式变量参数
            var parameter = Expression.Parameter(typeof(T), "o");

            if (orderByExpression != null && orderByExpression.Length > 0)
            {
                for (int i = 0; i < orderByExpression.Length; i++)
                {
                    //根据属性名获取属性
                    var property = typeof(T).GetProperty(orderByExpression[i].PropertyName);
                    //创建一个访问属性的表达式
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);

                    string OrderName = "";
                    if (i > 0)
                    {
                        OrderName = orderByExpression[i].IsDESC ? "ThenByDescending" : "ThenBy";
                    }
                    else
                        OrderName = orderByExpression[i].IsDESC ? "OrderByDescending" : "OrderBy";


                    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), OrderName, new Type[] { typeof(T), property.PropertyType }, query.Expression, Expression.Quote(orderByExp));

                    query = query.Provider.CreateQuery<T>(resultExp);
                }
            }

            total = query.Count();
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
        #endregion

        #region 编辑


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isSave">是否立即保存</param>
        public virtual Response Edit(T model, bool isSave = true)
        {
            var opsResult = new Response();
            Db.Set<T>().Update(model);
           
            if (isSave)
            {
                opsResult.Code = Db.SaveChanges() > 0 ? ResponseCode.Success : ResponseCode.Fail;
                opsResult.Message = opsResult.Code == ResponseCode.Success ? Suggestion.EditSucceed : Suggestion.EditFail;
            }
            else
            {
                opsResult.Code = ResponseCode.Success;
                opsResult.Message = "已添加到更新队列";
            }
            return opsResult;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        public virtual Response Delete(T entity, bool isSave = true)
        {
            var opsResult = new Response();
            Db.Set<T>().Remove(entity);
            if (isSave)
            {
                opsResult.Code = Db.SaveChanges() > 0 ? ResponseCode.Success : ResponseCode.Fail;
                opsResult.Message = opsResult.Code == ResponseCode.Success ? Suggestion.DeleteSucceed : Suggestion.DeleteFail;
            }
            else
            {
                opsResult.Code = ResponseCode.Success;
                opsResult.Message = "已添加到删除队列";
            }
            return opsResult;
        }
        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns></returns>
        public virtual Response Delete(object key, bool isSave = true)
        {
            var entity = Db.Set<T>().Find(key);
            var opsResult = new Response();
            if (entity == null)
            {
                opsResult.Code = ResponseCode.Fail;
                opsResult.Message = "没有找到删除的记录！";
                return opsResult;
            }
            opsResult= Delete(entity, isSave);
            return opsResult;
        }
        #endregion

        #region 新增
        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        public virtual Response Add(T entity, bool isSave = true)
        {
            var opsResult = new Response();
            Db.Set<T>().Add(entity);
            if (isSave)
            {
                opsResult.Code = Db.SaveChanges() > 0 ? ResponseCode.Success : ResponseCode.Fail;
                opsResult.Message = opsResult.Code == ResponseCode.Success ? Suggestion.InsertSucceed : Suggestion.InsertFail;
            }
            else
            {
                opsResult.Code = ResponseCode.Success;
            }
            return opsResult;
        }
        #endregion

        #region 保存
        public virtual int SaveChanges()
        {
            return  Db.SaveChanges();
        }
        #endregion

        #region 执行sql
        /// <summary>
        /// 执行sql增删改语句
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="pars">参数</param>
        /// <returns></returns>
        public virtual  int ExecuteSqlCommand(string sql, params IDbDataParameter[] pars)
        {
            try
            {
                return Db.Database.ExecuteSqlCommand(sql, pars);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 执行sql查询
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="pars">参数</param>
        /// <returns></returns>
        public virtual IQueryable<T> SqlQuery(string sql, params IDbDataParameter[] pars)
        {
            return Db.Set<T>().FromSql(sql, pars);
        }
        #endregion
    }
}
