using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebTest
{
    public class Startup
    {
        private readonly IWebHostEnvironment environment;

        public Startup(
            IWebHostEnvironment environment, IConfiguration configuration)
        {
            this.environment = environment;
            config = configuration;
        }

        public IConfiguration config { get; }


        /// <summary>
        /// <para>This method gets called by the runtime. Use this method to add services to the container.</para>
        /// <para>用于添加服务到容器</para>
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(
            IServiceCollection services)
        {
            // 配置控制器
            // services.AddControllers(); ;
            services.AddMvc()
                    .AddNewtonsoftJson();
            // 配置日志组件 Logging
            services.AddLogging(builder =>
            {
                builder
                .AddConfiguration(config.GetSection("Logging"))
                .AddConsole()
                .AddDebug()
                .AddEventSourceLogger();
            });
            // SignalR (使用详细错误信息)
            services.AddSignalR((opt) =>
            {
                if (environment.IsDevelopment())
                {
                    opt.EnableDetailedErrors = true;
                }
            });
            // 配置CORS
            services.AddCors(option =>
            {
                option.AddPolicy("cors", policy =>
                {
                    string CorsUrl = config.GetValue("CorsOrigins", "");
                    string[] CorsArray = CorsUrl.Split(',');
                    policy.WithOrigins(CorsArray)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithExposedHeaders("Authorization");
                });
            });

        }

        /// <summary>
        /// <para>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</para>
        /// <para>用于配置Http请求管线</para>
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            ILogger<Startup> logger  // 日志服务注入
            )
        {
            logger.LogDebug("正在设置中间件");

            if (env.IsDevelopment())
            {
                logger.LogInformation("- 使用面向开发者的异常页面");
                app.UseDeveloperExceptionPage();
            }

            logger.LogDebug("- 使用Https重定向");
            app.UseHttpsRedirection();

            logger.LogDebug("- 使用静态文件");
            app.UseStaticFiles();
            app.UseFileServer();

            logger.LogDebug("- 使用路由");
            app.UseRouting();

            logger.LogDebug("- 使用Cors");
            app.UseCors("cors");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
            logger.LogDebug("配置中间件完成");
        }
    }
}
