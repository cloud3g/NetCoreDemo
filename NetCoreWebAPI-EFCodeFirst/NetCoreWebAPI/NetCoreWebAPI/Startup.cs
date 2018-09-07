using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCoreWebAPI.Data;
using NetCoreWebAPI.Data.Infrastructure;
using NetCoreWebAPI.Data.Interface;
using NetCoreWebAPI.Data.Repositories;
using NetCoreWebAPI.Service.Interface;
using NetCoreWebAPI.Service.Service;

namespace NetCoreWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDbFactory, DbFactory>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IStudentCourseRepository, StudentCourseRepository>();
            services.AddTransient<IStudentCourseService, StudentCourseService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddDbContext<NetCoreWebAPIDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
