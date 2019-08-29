using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FirstMVCCore
{
    public class Program
    {
        //static Dictionary<string, string> appSettings = new Dictionary<string, string>()
        //{
        //    {"AuthorName","Asha"},
        //    {"AuthorEmail","ashas2@hexaware.com"}
        //};
        public static void Main(string[] args)
        {

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            // Customizing Host Configuration 
            //.ConfigureKestrel(options =>
            //{
            //    options.Limits.MaxRequestBodySize = 1024;
            //    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(30);

            //})
            .ConfigureLogging(config =>
            {
                config.AddConsole();
                config.AddDebug();
                config.AddEventSourceLogger();
                config.SetMinimumLevel(LogLevel.Warning);
            })
            //.ConfigureAppConfiguration(options =>
            // {
            //     options.SetBasePath(Directory.GetCurrentDirectory())
            //     .AddInMemoryCollection(appSettings)
            //     .AddXmlFile("mysettings.xml", optional: true)
            //     .AddJsonFile("mysettings.json", optional: true)
            //     .AddKeyPerFile("FileDir", optional:true)
            //     .AddEnvironmentVariables()
            //     .AddCommandLine(args);
            // })
            .UseStartup<Startup>();       
    }
}
