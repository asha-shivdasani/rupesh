using CatalogAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogAPI.Infrastructure
{
    public class CatalogContext
    { 
        private readonly IMongoDatabase mongoDatabase;
        public CatalogContext(IConfiguration configuration)
        {
            var databaseID = configuration.GetValue<string>("CosmosDBName");
            MongoClientSettings mongoClientSettings = MongoClientSettings.FromConnectionString(configuration.GetConnectionString("CatalogCosmosConnection"));
            MongoClient client = new MongoClient(mongoClientSettings);

            if (client != null)
            {
                this.mongoDatabase = client.GetDatabase(databaseID);


            }
        }
        public IMongoCollection<CatalogInfo> CatalogInfo
        {

            get
            {
                return this.mongoDatabase.GetCollection<CatalogInfo>("Catalog");
            }

        }
    }
}
