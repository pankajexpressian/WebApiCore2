using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using WebApiCore2.API.Contexts;
using WebApiCore2.API.Services;
using AutoMapper;

namespace WebApiCore2.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o =>
                {
                    o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                })
                .AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            //Use this to get property name as they are defined in model class. By default their name starts with lower case
            // in the result which is returned in response to api call
            //services.AddMvc().AddJsonOptions(o=> {
            //    if (o.SerializerSettings.ContractResolver!=null)
            //    {
            //        var customSerializer = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //        customSerializer.NamingStrategy = null;
            //    }
            //});

            var cityInfoDbConnection = _configuration["ConnectionStrings:CityInfoDbConnection"];
            services.AddDbContext<CityInfoDbContext>(o => o.UseSqlServer(cityInfoDbConnection));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient<ICityRepository, CityRepository>();
#if DEBUG
            services.AddTransient<IMailService, DevMailService>();
#else
services.AddTransient<IMailService, ProdMailService>();
#endif

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
                app.UseExceptionHandler();
            }
            app.UseStatusCodePages();
            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
