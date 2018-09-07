
using Blog.Common;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.IRepository
{
    public interface IBlogCategoryRepository:IRepository<BlogCategory>
    {
        Response CascadingDeletion(int id);
    }
}
