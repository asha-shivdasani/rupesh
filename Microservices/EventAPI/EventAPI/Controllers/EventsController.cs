using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EventAPI.Infrastructure;
using EventAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private EventDbContext eventDbContext;

        public EventsController(EventDbContext eventDbContext)
        {
            this.eventDbContext = eventDbContext;
        }
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<EventInfo>> GetEvents()
        {
            return eventDbContext.Events.ToList();
        }

        [HttpGet("{id}", Name = "GetEventById")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<IEnumerable<EventInfo>> GetEventById(int id)
        {
            var item = eventDbContext.Events.Find(id);
            if (item != null)
                return Ok(item);
            else
                return NotFound();
           
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<EventInfo>>> AddEvent(EventInfo eventInfo)
        {
            TryValidateModel(eventInfo);
            if (ModelState.IsValid)
            {
                var result = await eventDbContext.Events.AddAsync(eventInfo);
                await eventDbContext.SaveChangesAsync();
                //return Created($"/api/events/{result.Entity.Id}", result.Entity);
                //return CreatedAtAction(nameof(GetEventById), new { id = result.Entity.Id }, result.Entity);
                return CreatedAtRoute("GetEventById", new { id = result.Entity.Id }, result.Entity);
            }
            else
            {
                return BadRequest(ModelState);
            }
            

        }
    }
}