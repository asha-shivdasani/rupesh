using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace FirstMVCCore
{
    public class Startup
    {
        static Dictionary<string, string> appSettings = new Dictionary<string, string>()
        {
            {"AuthorName","Asha"},
            {"AuthorEmail","ashas2@hexaware.com"},
            {"Mesage","In Memory"}
        };
        public Startup()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                 .AddInMemoryCollection(appSettings)
                 .AddXmlFile("mysettings.xml", optional: true)
                 .AddJsonFile("mysettings.json", optional: true)
                 .AddKeyPerFile("FileDir", optional: true)
                 .AddJsonFile("appsettings.json")
                 .AddEnvironmentVariables();
            Configuration = configBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Read config values

            Console.WriteLine(Configuration.GetValue<string>("Company"));
            Console.WriteLine(Configuration.GetValue<string>("Address:City"));
            Console.WriteLine(Configuration.GetValue<string>("ProjectName"));
            Console.WriteLine(Configuration.GetValue<int>("Duration"));
            Console.WriteLine(Configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT"));
            Console.WriteLine(Configuration.GetValue<string>("AuthorName"));
            Console.WriteLine(Configuration.GetValue<string>("Message"));

            var course = Configuration.GetSection("Courses");

            Console.WriteLine(course["Title"]);

            //Read config values using class.

            services.Configure<AppConfiguration>(Configuration);


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
                //app.UseExceptionHandler("/Home/Error"); //default Exception Handling

                //Custom Exception Handling
                app.UseExceptionHandler((builder)=>{
                    builder.Run(async(context) =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.Headers["Content-Type"] = "text/html";
                        await context.Response.WriteAsync("<h2>Some Error Ocuured</h2>");
                        var exceptionPathFearture = context.Features.Get<IExceptionHandlerPathFeature>();
                        await context.Response.WriteAsync($"<p>{exceptionPathFearture.Error.Message}</p>");
                    });

                });
                app.UseHsts();
            }

            app.UseStatusCodePages("text/html", "Client side error occured with status code {0}"); //used for Status code messages

            app.UseStatusCodePagesWithRedirects("/index.html");

            app.UseStatusCodePagesWithReExecute("/index.html");

            app.UseHttpsRedirection();

            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");
            options.DefaultFileNames.Add("default.html");

            //app.UseDefaultFiles(options);

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    RequestPath = "/docs",
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "mydocs"))
            //});
            app.UseStaticFiles(); //configured to serve static files from wwwroot

            //app.UseDirectoryBrowser(new DirectoryBrowserOptions
            //{
            //    RequestPath = "/docs",
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "mydocs"))
            //});

            var fileOptions = new FileServerOptions
            {
                RequestPath = "/docs",
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "mydocs")),
                EnableDirectoryBrowsing = true

            };
            fileOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            fileOptions.DefaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseFileServer(fileOptions);
           
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
