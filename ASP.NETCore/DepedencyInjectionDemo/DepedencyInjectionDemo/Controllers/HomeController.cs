using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DepedencyInjectionDemo.Models;
using DepedencyInjectionDemo.Services;
using Microsoft.Extensions.Configuration;

namespace DepedencyInjectionDemo.Controllers
{
    public class HomeController : Controller
    {
        private IDataManager dm;
        //public HomeController(IDataManager dataManager)
        //{
        //    this.dm = dataManager;

        //}
        public IActionResult Index([FromServices]IDataManager dm, DemoService demoService, [FromServices]IConfiguration configuration)
        {
            ViewBag.Message = dm.GetMessage("Asha");
            ViewBag.Data = demoService.GetData();
            //configuration.GetValue<string>("key");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
