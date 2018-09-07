using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Data.Infrastructure
{
    public interface IRepository <T> where T : class
    {
        /// <summary>
        /// Get All Entity With Condition Lambdas Expression
        /// </summary>
        /// <param name="where">Exam: n => n.ID == id</param>
        /// <returns>Return List Entity</returns>
        List<T> GetALL(Expression<Func<T, bool>> where);
        /// <summary>
        /// Get First Entity With Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return First Entity</returns>
        T GetSingleByID(int id);
        /// <summary>
        /// Delete With ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteByID(int id);
    }
}
