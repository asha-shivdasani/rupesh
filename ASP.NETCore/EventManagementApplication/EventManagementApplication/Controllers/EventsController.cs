using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagementApplication.Infrastructure;
using EventManagementApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementApplication.Controllers
{
    [Route("events")]
    public class EventsController : Controller
    {
        private EventDbContext eventDbContext;

        public EventsController(EventDbContext eventDbContext)
        {
            this.eventDbContext = eventDbContext;
        }
        [HttpGet("", Name ="ListEvents")]
        public IActionResult Index()
        {
            var model = eventDbContext.Events.ToList();
            return View(model);
        }

        [HttpGet("new", Name = "AddEvent")]
        public IActionResult Create()
        {           
            return View();
        }

        [HttpPost("new", Name = "AddEvent")]
        public async Task<IActionResult> CreateAsync(EventInfo eventInfo)
        {
            if (!ModelState.IsValid)
            {
                return View("Create",eventInfo);
            }
            else
            {
                await eventDbContext.Events.AddAsync(eventInfo);
                await eventDbContext.SaveChangesAsync();
                return RedirectToRoute("ListEvents");
            }
            
        }
    }
}