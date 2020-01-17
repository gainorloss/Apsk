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
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

namespace Gateway.API
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
            services.AddOcelot()
                .AddConsul();// ocelot.

            services.AddControllers(cfg => cfg.Filters.Add<GlobalLogExceptionFilter>());

            services.AddApskComponents(Configuration)
                .AddApskBus()
                .AddApskRestControllers()
                .AddApskJwtBearer(Configuration)
                .AddApskOpenApiDocument(Configuration)
                .AddApskServiceDiscoveryClient(Configuration)
                .BuildDynamicProxyProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseApskOpenApiDocument(Configuration);
            app.UseApskServiceRegistration(Configuration, applicationLifetime);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOcelot().Wait();// ocelot.
        }
    }
}
