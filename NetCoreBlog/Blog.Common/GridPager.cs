using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Common
{
    /// <summary>
    /// 分页帮助类类
    /// </summary>
    /// <typeparam name="T">需要返回数据的类型</typeparam>
    public class GridPager<T>
    {
        /// <summary>
        /// 每页行数
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 当前页是第几页
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public string sort { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public int totalRows { get; set; }
        /// <summary>
        /// 数据集合
        /// </summary>
        public List<T> Entities { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int totalPages 
        {
            get
            {
                return (int)Math.Ceiling((float)totalRows / (float)rows);
            }
        }
    }
}
