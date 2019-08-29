using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CatalogAPI.Infrastructure;
using CatalogAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private CatalogContext catalogContext;
        public CatalogController(CatalogContext catalogContext)
        {
            this.catalogContext = catalogContext;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CatalogInfo>>> GetCatalogInfoAsync()
        {
            return await catalogContext.CatalogInfo.FindAsync<CatalogInfo>(FilterDefinition<CatalogInfo>.Empty).Result.ToListAsync();
            
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<CatalogInfo>> GetCatalogInfoByIdAsync(string Id)
        {
            var catalogInfoId = await catalogContext.CatalogInfo.FindAsync(Id);
            if (catalogInfoId != null)
                return Ok(catalogInfoId);
            else
                return NotFound();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CatalogInfo>> AddCatalogInfoAsync(CatalogInfo catalogInfo)
        {
            TryValidateModel(catalogInfo);
            if (ModelState.IsValid)
            {
                await catalogContext.CatalogInfo.InsertOneAsync(catalogInfo);
                return catalogInfo;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}