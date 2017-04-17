using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace dotnet_core_api_example
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
				  builder => builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials());
			});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

			app.UseCors("CorsPolicy");

			//Run our own logic instead of MVC
			//app.Run(async context =>
			//{
				
			//	var path = context.Request.Path.ToString();

			//	var methodType = context.Request.Method;
			//	Console.WriteLine("In Middleware, Path is: " + path + ", Method is:" + methodType);
			//	context.Items["ReturnObj"] = new { Path = path, Method = methodType };

			//	var res = JsonConvert.SerializeObject(context.Items["ReturnObj"]);
			//	context.Response.StatusCode = StatusCodes.Status200OK;
			//	context.Response.ContentType = "application/json";
			//	context.Response.ContentLength = res.Length;
			//	await context.Response.WriteAsync(res);
			//});

           app.UseMvc();

        }
    }
}
