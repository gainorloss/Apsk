using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apsk.AspNetCore.Extensions;
using Apsk.AspNetCore.Filters;
using Apsk.AspNetCore.Middlewares;
using Apsk.Cloud.Extensions;
using Apsk.Extensions;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CatalogItems.API
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
            services.AddControllers(opt =>
            {
                opt.Filters.Add<GlobalLogExceptionFilter>();
            });

            services.AddApskComponents(Configuration)//di
                    .AddApskBus()//event bus
                    .AddApskRestControllers()//dynamic api
                    .AddApskJwtBearer(Configuration)
                    .AddApskOpenApiDocument(Configuration)
                    .AddApskServiceDiscovery(Configuration)//doc.
                    ;
            services.AddCors(opt => opt.AddPolicy("any", policy => policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins(new[] { "http://localhost:8081" })));
            services.BuildDynamicProxyProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseApskServiceDiscovery(Configuration, applicationLifetime);//consul service registration.
            app.UseCors("any");
            app.UseApskOpenApiDocument(Configuration);// doc.
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
