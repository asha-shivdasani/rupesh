﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventAPI.CustomFormatters;
using EventAPI.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace EventAPI
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
            services.AddDbContext<EventDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("EventSqlConnection"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title ="Event API",
                    Description ="Contains the API operations for adding, deleting and querying events",
                    Version="1.0",
                    Contact =new Contact { Name="Asha Shivdasani", Email="ashas2@hexaware.com"}
                });
            });

            services.AddCors(c=> 
            {
                c.AddPolicy("HexClients", config =>
                 {
                     config.WithOrigins("https://hexaware.com")
                     .WithHeaders("Content-Type", "Accept", "Authorization")
                     .WithMethods("GET", "POST", "PUT", "DELETE");

                 });
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
            services.AddAuthentication(c =>
            {
                c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(c =>
            {
                c.RequireHttpsMetadata = false;
                c.SaveToken = true;
                c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidIssuer = Configuration.GetValue<string>("Jwt:Issuer"),
                    ValidAudience = Configuration.GetValue<string>("Jwt:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Jwt:Secret")))
                };
            });
            services.AddMvc(c=> {
                c.OutputFormatters.Add(new CsvFormatter());
            }).AddXmlDataContractSerializerFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            InitializeDatabase(app);
            app.UseAuthentication();

            app.UseSwagger();
            app.UseCors();


            if (env.IsDevelopment())
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event API");
                    c.RoutePrefix = "";
                });
            }
                

            app.UseMvc();
        }

        private void InitializeDatabase(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var eventDBContext = serviceScope.ServiceProvider.GetRequiredService<EventDbContext>())
                {
                    eventDBContext.Database.Migrate();
                }
            }
        }
    }
}
