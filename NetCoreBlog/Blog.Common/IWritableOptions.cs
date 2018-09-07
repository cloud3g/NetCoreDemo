using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
namespace Blog.Common
{
    /// <summary>
    /// 修改json文件节点的接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWritableOptions<T> : IOptionsSnapshot<T> where T : class, new()
    {
        
        void Update(Action<T> applyChanges);
        
    }


}
