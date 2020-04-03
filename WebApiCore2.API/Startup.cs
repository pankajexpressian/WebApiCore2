using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using WebApiCore2.API.Services;

namespace WebApiCore2.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddMvcOptions(o =>
            {
                o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });

            //Use this to get property name as they are defined in model class. By default their name starts with lower case
            // in the result which is returned in response to api call
            //services.AddMvc().AddJsonOptions(o=> {
            //    if (o.SerializerSettings.ContractResolver!=null)
            //    {
            //        var customSerializer = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //        customSerializer.NamingStrategy = null;
            //    }
            //});

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
