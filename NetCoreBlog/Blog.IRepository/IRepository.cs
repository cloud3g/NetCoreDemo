using Blog.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Blog.IRepository
{
    public interface IRepository<T>
    {
        #region 查询
        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Find(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="tableNames">需要联合查询的表名</param>
        /// <returns></returns>
        T FindJoin(Expression<Func<T, bool>> predicate, string[] tableNames);
        /// <summary>
        /// 单表查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> GetList(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 多表关联查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="tableNames"></param>
        /// <returns></returns>
        IQueryable<T> GetListJoin(Expression<Func<T, bool>> predicate, string[] tableNames);
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
        IQueryable<T> GetPageList<TKey>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, bool isAsc = true);
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
        IQueryable<T> GetPageList(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> where, string sort, bool isAsc = true);
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pager">分页类</param>
        /// <param name="where">查询条件</param>
        /// <returns>分页类</returns>
        GridPager<T> GetPageList(GridPager<T> pager, Expression<Func<T, bool>> where);
        /// <summary>
        ///  根据条件排序获取分页实体
        /// </summary>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="total">总数据量</param>
        /// <param name="where">查询条件</param>
        /// <param name="orderByExpression">排序实体(包含排序字段和排序方式)</param>
        /// <returns></returns>
        IQueryable<T> GetPageList(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> where, params OrderModelField[] orderByExpression);
        #endregion
        #region 编辑

        /// <summary>
        /// 直接查询之后再修改
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isSave">是否立即保存</param>
        Response Edit(T model, bool isSave = true);
        #endregion
        #region 删除
        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        Response Delete(T entity, bool isSave = true);
        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="isSave">是否立即保存</param>
        /// <returns></returns>
        Response Delete(object key, bool isSave = true);
        #endregion
        #region 新增
        /// <summary>
        /// 创建一个实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isSave">是否立即保存</param>
        Response Add(T entity, bool isSave = true);
        #endregion
        #region 保存
        int SaveChanges();
        #endregion
        #region 执行sql
        /// <summary>
        /// 执行sql增删改语句
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="pars">参数</param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, params IDbDataParameter[] pars);
        /// <summary>
        /// 执行sql查询
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="pars">参数</param>
        /// <returns></returns>
        IQueryable<T> SqlQuery(string sql, params IDbDataParameter[] pars);
        #endregion
    }
}
