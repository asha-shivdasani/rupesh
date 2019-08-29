using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CachingDemo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CachingDemo
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
            services.AddMemoryCache(); //enabling non distributed memory cache

            //services.AddDistributedMemoryCache(); // enabling in memory distributed cache.

            // enabling sql server distributed cache.
            services.AddDistributedSqlServerCache(config =>
            {
                config.ConnectionString = Configuration.GetConnectionString("SqlConnection");
                config.SchemaName = "dbo";
                config.TableName = "CacheTableA";
            });

            // enabling redis distributed cache.
            //Microsoft.Extensions.Caching.Redis package for dotnet core 2.1
            //services.AddDistributedRedisCache(config =>
            //{
            //    config.InstanceName = "redis";
            //    config.Configuration = "localhost:6379";

            //});

            //dotnet sql-cache create "connectionnString" "schemaname" "tablename"

            services.AddSession(config=> 
            {
                config.Cookie.Name = "MySessionCookie";
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddTransient<StateDataService>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.Use(async (context, next) =>
            {
                if (context.Request.Headers.ContainsKey("x-data"))
                {
                    context.Items["KeyExists"] = true;
                    context.Items["DatabaseName"] = "abc";
                }
                else
                {
                    context.Items["KeyExists"] = false;
                    context.Items["DatabaseName"] = "xyz";
                }
                await next.Invoke();
            });
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
