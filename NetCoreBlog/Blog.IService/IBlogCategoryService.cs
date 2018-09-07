
using Blog.Common;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.IService
{
    public interface IBlogCategoryService:IService<BlogCategory>
    {
        /// <summary>
        /// 设置首页显示
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool SetNav(int id, bool value);
        /// <summary>
        /// 设置启用状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool SetEnable(int id, bool value);
        /// <summary>
        /// 级联删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Response CascadingDeletion(int id);
    }
}
