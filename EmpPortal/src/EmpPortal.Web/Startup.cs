using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using MVCApp.Infrastructure;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MVCApp.Controllers;
using EmpPortal.Common.Infrastructure;

namespace MVCApp
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IContainer _container;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
            _env = env;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            // log setup
            ApplicationServices.LoggerFactory = new LoggerFactory();
            ILoggerFactory loggerFactory = ApplicationServices.LoggerFactory;

            if(_env.IsDevelopment())
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
            }
            else
            {
                loggerFactory.AddNLog();
                _env.ConfigureNLog("nlog.config");
            }

            services.AddSingleton<ILoggerFactory>(loggerFactory);
            // services.AddScoped<ILogIdentifier>(c => new LogIdentifier());

            // config setup
            ApplicationServices.Configuration = Configuration;
            services.AddSingleton<IConfiguration>(Configuration);

            // MVC setup
            var mvcBuilder = services.AddMvc();
            mvcBuilder.AddMvcOptions(o => o.Filters.Add(new GlobalExceptionHandler(loggerFactory)));

            containerBuilder.Populate(services);
            containerBuilder.Register<ILogIdentifier>(c => new LogIdentifier()).InstancePerLifetimeScope();
            _container = containerBuilder.Build();
            ApplicationServices.Container = _container;

            return new AutofacServiceProvider(_container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
