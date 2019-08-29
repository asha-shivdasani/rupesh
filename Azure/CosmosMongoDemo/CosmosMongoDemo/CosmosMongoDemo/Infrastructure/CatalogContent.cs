using CosmosMongoDemo.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosMongoDemo.Infrastructure
{
    public class CatalogContent
    {
        private readonly IMongoDatabase mongoDatabase;
        public CatalogContent()
        {
            var connectionString = "mongodb://asha-mongooapi:ljfei6ftGUlES5AFdGM1s1wQ7RWUdJPEpcd1ZnPuAO5hh8BpD7ptpbP1KC0S2wME0vraIzPIwJ1ivEnrVfpUHA==@asha-mongooapi.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
            var databaseName = "eshopdb";
            MongoClientSettings mongoClientSettings = MongoClientSettings.FromConnectionString(connectionString);
            MongoClient client = new MongoClient(mongoClientSettings);
            if (client != null)
            {
                this.mongoDatabase = client.GetDatabase(databaseName);
            }
        }

        public IMongoCollection<CatalogItem> CatalogItems
        {
            get
            {
                return this.mongoDatabase.GetCollection<CatalogItem>("CatalogItems");
            }

        }
    }
}
