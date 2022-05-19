using EntityFrameworkDemo.EmpServiceContracts;
using EntityFrameworkDemo.EmpUtils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EntityFrameworkDemo
{
    public class Startup
    {
        private IConfiguration _config { get; }
        public Startup(IConfiguration configuration)
        {
           _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContextPool<EmployeesDBContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("SQLServerConnectionString")));
            services.AddDbContextPool<TempItemsDBContext>(options =>
                options.UseSqlServer(_config.GetConnectionString("SQLServerConnectionString")));
            services.AddTransient<IEmployeesRepository, EmployeesRepository>();
            services.AddScoped<ServiceContracts.Logger.ILogger, Utils.Logger.LoggerUtils>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { 
                    Title = "My EntityFrameWorkDemo",
                    Description = "Project to test entity framework",
                    Version = "1.0" });
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                options.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseSessionLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options => {
                    options.SwaggerEndpoint("/swagger/v1.0/swagger.json", "My EntityframeworkDemo Swagger");
                    options.RoutePrefix = string.Empty;
                });
            }
            
        }
    }
}
