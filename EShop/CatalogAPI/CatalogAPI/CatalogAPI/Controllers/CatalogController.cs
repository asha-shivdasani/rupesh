using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CatalogAPI.Infrastructure;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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

        [HttpGet("GetString", Name = "GetString")]
        public string GetString()
        {
            return "catalogapi is working";
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CatalogInfo>>> GetCatalogInfoAsync()
        {
            //var items = catalogContext.CatalogInfo.FindAsync<CatalogInfo>(FilterDefinition<CatalogInfo>.Empty);
            //var catalogItems = items.Result.ToList();
            return await catalogContext.CatalogInfo.FindAsync<CatalogInfo>(FilterDefinition<CatalogInfo>.Empty).Result.ToListAsync();

        }

        [HttpGet("{id}", Name = "GetByCategoryId")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<CatalogInfo>> GetByCategoryIdAsync(int id)
        {

            FilterDefinition<CatalogInfo> filter = Builders<CatalogInfo>.Filter.Eq("CategoryId", id);
            IEnumerable<CatalogInfo> entity = null;
            using (IAsyncCursor<CatalogInfo> cursor = await this.catalogContext.CatalogInfo.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    entity = cursor.Current;
                }
            }
            if (entity != null)
                return Ok(entity);
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

        //[HttpGet("{id/itemid}", Name = "GetItemDetailsById")]
        [HttpGet("{id}/{itemId}",Name = "GetItemDetailsById")]
        //[Route("GetItemDetailsById")]       
        public async Task<ActionResult<CatalogInfo>> GetItemDetailsById(int id, int itemId)
        {

            FilterDefinition<CatalogInfo> filter = Builders<CatalogInfo>.Filter.Eq("CategoryId", id);
            // filter. Builders<CatalogInfo>.Filter.Eq("itemid", itemId);
            IEnumerable<CatalogInfo> entity = null;

            using (IAsyncCursor<CatalogInfo> cursor = await this.catalogContext.CatalogInfo.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    entity = cursor.Current;
                    // var itemData = entity.FirstOrDefault().Items[0].itemid == itemId;
                    var itemData = entity.ToArray()[0].Items.Where(c => c.itemid == itemId);
                    //var itemData1 = entity.FirstOrDefault(c => c.Items.FirstOrDefault(t => t.itemid == itemId));
                    if (entity != null)
                        return Ok(itemData);
                }
            }

            return NotFound();

        }
        //[HttpPut(Name="Update")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult<CatalogInfo>> UpdateCategoryAsync(string id, CatalogInfo catalogInfo)
        //{
        //    TryValidateModel(catalogInfo);
        //    if (ModelState.IsValid)
        //    {
        //        FilterDefinition<CatalogInfo> filter = Builders<CatalogInfo>.Filter.Eq("_id", ObjectId.Parse(id));
        //        await this.catalogContext.CatalogInfo.ReplaceOneAsync(filter, catalogInfo, new UpdateOptions() { IsUpsert = true });
        //        return catalogInfo;
        //    }
        //    else
        //    {
        //        return BadRequest(ModelState);
        //    }

        //}




    }
}