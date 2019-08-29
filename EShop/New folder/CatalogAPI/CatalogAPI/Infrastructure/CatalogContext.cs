using CatalogAPI.Model;
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
        private static IConfiguration configuration;
        private readonly IMongoDatabase mongoDatabase;
        private static readonly string databaseID = configuration.GetConnectionString("CosmosDBName");
        private static readonly string collectionID = configuration.GetConnectionString("CosmosCatalogCollectionName");
        

        public CatalogContext()
        {         
            MongoClientSettings mongoClientSettings = MongoClientSettings.FromConnectionString(configuration.GetConnectionString("CatalogCosmosConnection"));
            MongoClient client = new MongoClient(mongoClientSettings);
            if (client != null)
            {
                this.mongoDatabase = client.GetDatabase(configuration.GetValue<string>(databaseID));
            }
        }
        public IMongoCollection<CatalogInfo> CatalogInfo
        {
            get
            {
                return this.mongoDatabase.GetCollection<CatalogInfo>("CatalogInfo");
            }

        }


    }
}
