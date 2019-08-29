using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Threading.Tasks;

namespace CosmosSqlDemo
{
    class Program
    {
        static DocumentClient documentClient;
        static string endPoint;
        static string authKey;
        static void Main(string[] args)
        {
            endPoint = "https://asha-coresql.documents.azure.com:443/";
            authKey = "yXOIzYDeTKBMipJsisSvOYln1xlM3WGQYjdx72OeZ2cMRysikxMNq2srVu2vAi9FMxOiMwP7PIzEGaC0woG3Yg==";
            documentClient = new DocumentClient(new Uri(endPoint),authKey);

            //Create Database
            // CreateDatabaseAsync("eshopdb").Wait();

            //Create Collection
            // CreateCollectionAsync("eshopdb", "products", "/Category").Wait();

            //Create a Document
            //var product = new { Name = "iPhone", Category = "Mobile", Price = 70000, Quantity = 1 };
            // CreateDocumentAsync("eshopdb", "products", product).Wait();

            //Read Document
            ReadDocumentAsync("eshopdb", "products").Wait();


            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        static async Task CreateDatabaseAsync(string dbName)
        {
            await documentClient.CreateDatabaseIfNotExistsAsync(new Microsoft.Azure.Documents.Database { Id = dbName });

        }
        static async Task CreateCollectionAsync(string dbName, string collectionName, string partitionKey)
        {
            DocumentCollection documentCollection = new DocumentCollection();
            documentCollection.Id = collectionName;
            documentCollection.PartitionKey.Paths.Add(partitionKey);

            await documentClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(dbName),
                documentCollection, new RequestOptions { OfferThroughput=500});

        }

        static async Task CreateDocumentAsync(string dbName, string collectionName, dynamic item)
        {
            await documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbName,collectionName),
                item, new RequestOptions { PartitionKey= new PartitionKey(item.Category),
                ConsistencyLevel = ConsistencyLevel.ConsistentPrefix});

        }

        static async Task ReadDocumentAsync(string dbName, string collectionName)
        {
            string continuationToken = null;
            do {
                var response = await documentClient.ReadDocumentFeedAsync(UriFactory.CreateDocumentCollectionUri(dbName, collectionName),
                new FeedOptions
                {
                    MaxItemCount = 10,
                    RequestContinuation = continuationToken

                });
                continuationToken = response.ResponseContinuation;
                foreach (Document document in response)
                {
                    Console.WriteLine(document);
                }
            } while (continuationToken != null);         

        }

    }
}
