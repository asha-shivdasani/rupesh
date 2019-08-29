using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogAPI.Models
{
    public class CatalogInfo
    {
        public CatalogInfo()
        {
            this.Items = new List<Item>();

        }
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }        
        public int CategoryId { get; set; }     
        public string Name { get; set; }        
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public int itemid { get; set; }
        public string itemname { get; set; }
        public double itemprice { get; set; }
        public string itemImageUrl { get; set; }
        public string itemDescription { get; set; }

    }
}
