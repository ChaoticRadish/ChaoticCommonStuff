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
        /// <para>������ӷ�������</para>
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(
            IServiceCollection services)
        {
            // ���ÿ�����
            // services.AddControllers(); ;
            services.AddMvc()
                    .AddNewtonsoftJson();
            // ������־��� Logging
            services.AddLogging(builder =>
            {
                builder
                .AddConfiguration(config.GetSection("Logging"))
                .AddConsole()
                .AddDebug()
                .AddEventSourceLogger();
            });
            // SignalR (ʹ����ϸ������Ϣ)
            services.AddSignalR((opt) =>
            {
                if (environment.IsDevelopment())
                {
                    opt.EnableDetailedErrors = true;
                }
            });
            // ����CORS
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
        /// <para>��������Http�������</para>
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            ILogger<Startup> logger  // ��־����ע��
            )
        {
            logger.LogDebug("���������м��");

            if (env.IsDevelopment())
            {
                logger.LogInformation("- ʹ�����򿪷��ߵ��쳣ҳ��");
                app.UseDeveloperExceptionPage();
            }

            logger.LogDebug("- ʹ��Https�ض���");
            app.UseHttpsRedirection();

            logger.LogDebug("- ʹ�þ�̬�ļ�");
            app.UseStaticFiles();
            app.UseFileServer();

            logger.LogDebug("- ʹ��·��");
            app.UseRouting();

            logger.LogDebug("- ʹ��Cors");
            app.UseCors("cors");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
            logger.LogDebug("�����м�����");
        }
    }
}
