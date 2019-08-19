using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Guestlogix.SkyRoutes.Application.Settings;
using Guestlogix.SkyRoutes.Persistence.Data;
using Guestlogix.SkyRoutes.Persistence.Interfaces;
using Guestlogix.SkyRoutes.Persistence.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Guestlogix.SkyRoutes.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddMvcCore()
                    .AddControllersAsServices()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddJsonOptions(
                        options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            ); ;

            services.AddApiVersioning(
                options => { options.ReportApiVersions = true; }
            );

            services.AddVersionedApiExplorer(
                options => { options.SubstituteApiVersionInUrl = true; }
            );
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddScoped<CsvContext>();

            services.AddScoped<ICsvRepository, CsvRepository>();

            // Register the Swagger services

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "SkyRoutes API";
                    document.Info.Description = "A simple ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Thiago Simionato dos Santos",
                        Email = "tgo.simionato@gmail.com"
                    };
                };
            });

            return BuildDependencyInjectionProvider(services);
        }

        private IServiceProvider BuildDependencyInjectionProvider(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);

            IContainer appContainer = builder.Build();
            return new AutofacServiceProvider(appContainer);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
