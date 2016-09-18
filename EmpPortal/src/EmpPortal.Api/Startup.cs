using Autofac;
using Autofac.Extensions.DependencyInjection;
using EmpPortal.Api.Infrastructure;
using EmpPortal.Common.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace EmpPortal.Api
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

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            Configuration = builder.Build();
            _env = env;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            // log setup
            ApplicationServices.LoggerFactory = new LoggerFactory();
            ILoggerFactory loggerFactory = ApplicationServices.LoggerFactory;

            if (_env.IsDevelopment())
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

            // Config
            ApplicationServices.Configuration = Configuration;
            services.AddSingleton<IConfiguration>(Configuration);

            // setup MVC
            var mvcBuilder = services.AddMvc();
            mvcBuilder.AddMvcOptions(o => o.Filters.Add(new GlobalExceptionHandler(loggerFactory)));

            containerBuilder.Populate(services);
            containerBuilder.Register<ILogIdentifier>(c => new LogIdentifier()).InstancePerLifetimeScope();
            _container = containerBuilder.Build();
            ApplicationServices.Container = _container;

            return new AutofacServiceProvider(_container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseBrowserLink();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                // app.UseExceptionHandler("/Home/Error");
            }

            app.UseLogIdentifier();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc();
        }
    }
}
