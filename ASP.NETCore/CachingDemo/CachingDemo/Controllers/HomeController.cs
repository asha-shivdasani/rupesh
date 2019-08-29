using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CachingDemo.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Http;
using CachingDemo.Services;

namespace CachingDemo.Controllers
{
    public class HomeController : Controller
    {
        private IMemoryCache memoryCache;
        private IDistributedCache distributedCache;
        private StateDataService stateDataService;

        public HomeController(IMemoryCache memoryCache, IDistributedCache distributedCache, StateDataService stateDataService)
        {
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.stateDataService = stateDataService;
            
        }
        //[ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any, NoStore = false)]
        public IActionResult Index()
        {
            //ViewBag.Now = DateTime.Now;

            //InMemory Caching
            if (string.IsNullOrEmpty(memoryCache.Get<string>("time"))) //if cache is empty set it for first time
            {
                memoryCache.Set<string>("time", DateTime.Now.ToString(), new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromSeconds(10)

                });
            }
            ViewBag.Now = memoryCache.Get<string>("time"); // read from cache

            //InMemory Distributed Caching
            if (string.IsNullOrEmpty(distributedCache.GetString("distTime")))
            {
                distributedCache.SetString("distTime", DateTime.Now.ToString(), new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromSeconds(10)
                });
            }
            ViewBag.DistributedTime = distributedCache.GetString("distTime"); // read from cache

            HttpContext.Session.SetString("Message", "Session");

            stateDataService.Data = "Service Session";

            ViewBag.Item = HttpContext.Items["DatabaseName"];
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
            ViewBag.Message = HttpContext.Session.GetString("Message");
            ViewBag.Service = stateDataService.Data;


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
