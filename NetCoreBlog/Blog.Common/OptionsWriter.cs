using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blog.Common
{
    /// <summary>
    /// 修改json文件节点的实现类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WritableOptions<T> : IWritableOptions<T> where T : class, new()
    {
    
        private readonly IHostingEnvironment _environment;
        private readonly IOptionsMonitor<T> _options;
        private readonly string _section;
        private readonly string _file;
        
        public WritableOptions(
            IHostingEnvironment environment,
            IOptionsMonitor<T> options,
            string section,
            string file)
        {
            _environment = environment;
            _options = options;
            _section = section;
            _file = file;
        }

        public T Value => this._options.CurrentValue;

        //public T CurrentValue => this._options.CurrentValue;

        public T Get(string name) => _options.Get(name);

        //public IDisposable OnChange(Action<T, string> listener)
        //{
        //    throw new NotImplementedException();
        //}

        public void Update(Action<T> applyChanges)
        {
            var fileProvider = _environment.ContentRootFileProvider;
            var fileInfo = fileProvider.GetFileInfo(_file);
            var physicalPath = fileInfo.PhysicalPath;

            var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(physicalPath));
            var sectionObject = jObject.TryGetValue(_section, out JToken section) ?
                JsonConvert.DeserializeObject<T>(section.ToString()) : (Value ?? new T());

            applyChanges(sectionObject);

            jObject[_section] = JObject.Parse(JsonConvert.SerializeObject(sectionObject));
            File.WriteAllText(physicalPath, JsonConvert.SerializeObject(jObject, Formatting.Indented));
        }
    }

    /// <summary>
    /// 把json文件节点绑定到类的扩展方法，具有修改功能
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureWritable<T>(
            this IServiceCollection services,
            IConfigurationSection section,
            string file = "appsettings.json") where T : class, new()
        {
            services.Configure<T>(section);
            services.AddTransient<IWritableOptions<T>>(provider =>
            {
                var environment = provider.GetService<IHostingEnvironment>();
                var options = provider.GetService<IOptionsMonitor<T>>();
                
                return new WritableOptions<T>(environment, options, section.Key, file);
            });
        }
    }

}
