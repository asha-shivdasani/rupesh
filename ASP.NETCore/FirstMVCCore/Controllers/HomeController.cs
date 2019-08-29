using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FirstMVCCore.Models;
using Microsoft.Extensions.Options;

namespace FirstMVCCore.Controllers
{
    public class HomeController : Controller
    {
        AppConfiguration appConfiguration = new AppConfiguration();
        public HomeController(IOptions<AppConfiguration> options)
        {
            this.appConfiguration = options.Value;

        }
        public IActionResult Index()
        {
            ViewBag.Company = appConfiguration.Company;
            ViewBag.Message = appConfiguration.Message;
            ViewBag.CourseDuration = appConfiguration.Duration;
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
