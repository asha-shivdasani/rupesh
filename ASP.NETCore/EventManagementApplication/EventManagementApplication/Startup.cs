using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagementApplication.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace EventManagementApplication
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
                //options.UseInMemoryDatabase(databaseName: "EventDb");
                options.UseSqlServer(Configuration.GetConnectionString("EventSqlConnection"));
            });
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
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
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            InitializeDatabase(app);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeDatabase(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<EventDbContext>().Database.Migrate();
                // var serviceProvider = serviceScope.ServiceProvider;
                // using (var db = new EventDbContext(serviceProvider.GetRequiredService<DbContextOptions<EventDbContext>>()))
                // {
                //     if (db.Events.Any())
                //     {
                //         return;
                //     }
                //     else
                //     {
                //         db.Events.Add(new Models.EventInfo
                //         {
                //             EventTitle = "Modern applications",
                //             StartDate = DateTime.Now.AddDays(5),
                //             EndDate = DateTime.Now.AddDays(7),
                //             Location = "Mumbai",
                //             Organizer = "Microsoft",
                //             RegistrationUrl = "https://events.microsoft.com/MDAZ001"
                //         });
                //         db.SaveChanges();

                //     }
                // }
            }
        }
    }
}
