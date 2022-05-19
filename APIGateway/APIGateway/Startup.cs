using CacheManager.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace APIGateway
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // private IConfigurationRoot Configuration { get; }
        //public Startup(IWebHostEnvironment env)
        //{
        //    var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
        //    builder.SetBasePath(env.ContentRootPath + Path.DirectorySeparatorChar + "OcelotConfigs");
        //    //if (File.Exists(env.ContentRootPath + Path.DirectorySeparatorChar + "OcelotConfigs" + Path.DirectorySeparatorChar + 
        //    //    $"ocelot.{env.EnvironmentName}.json"))
        //    //    builder.AddJsonFile($"ocelot.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
        //    //else
        //        builder.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
        //    builder.AddEnvironmentVariables();
        //
        //    Configuration = builder.Build();
        //}




        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Action<ConfigurationBuilderCachePart> settings = (x) =>
            //{
            //    object p = x.WithMicrosoftLogging(log =>
            //    {
            //        log.AddConsole(LogLevel.Debug);
            //    }).WithDictionaryHandle();
            //};
            services.AddControllers();
            services.AddOcelot(Configuration);
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "My API Gateway Service",
                    Description = "Project to test My API Gateway Service",
                    Version = "1.0"
                });
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                options.IncludeXmlComments(filePath);
            });
            services.AddSwaggerForOcelot(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1.0/swagger.json", "My API Gateway Service Swagger");
                    options.RoutePrefix = string.Empty;
                });
                app.UseSwaggerForOcelotUI(options =>
                {
                    options.PathToSwaggerGenerator = "/swagger/docs";
                });
            }

            app.UseRouting();

            await app.UseOcelot();

            //extras all below
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("APIGateway :-> Hello World! \n env = " + env.EnvironmentName);
                });
            });
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("APIGateway :-> Hello World! \n env = " + env.EnvironmentName);
            //});
        }
    }
}
