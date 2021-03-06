﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosMongoDemo.Models
{
    public class CatalogItem
    {
        public CatalogItem()
        {
            this.Vendors = new List<Vendor>();

        }
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public List<Vendor> Vendors { get; set; }
        
    }

    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
    }
}
