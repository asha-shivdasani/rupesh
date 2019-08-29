using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogAPI.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CatalogAPI
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
            services.AddSingleton<CatalogContext>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Catalog API",
                    Description = "Contains the API operations for adding and querying events",
                    Version = "1.0",
                    Contact = new Contact { Name = "Asha Shivdasani", Email = "ashas2@hexaware.com" }
                });
            });

            services.AddCors(c =>
            {
                //c.AddPolicy("HexClients", config =>
                //{
                //    config.WithOrigins("https://hexaware.com")
                //    .WithHeaders("Content-Type", "Accept", "Authorization")
                //    .WithMethods("GET", "POST", "PUT", "DELETE");

                //});
                //c.AddPolicy("AllowAll", config =>
                //{
                //    config.AllowAnyOrigin()
                //    .AllowAnyHeader()
                //    .AllowAnyMethod();
                //});
                c.AddDefaultPolicy(config =>
                {
                    config.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseCors();

            if (env.IsDevelopment())
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API");
                    c.RoutePrefix = "";
                });
            }


            app.UseMvc();
        }
    }
}
