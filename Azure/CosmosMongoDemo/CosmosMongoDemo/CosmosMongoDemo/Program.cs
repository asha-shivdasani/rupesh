using CosmosMongoDemo.Infrastructure;
using CosmosMongoDemo.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace CosmosMongoDemo
{
    class Program
    {
        static CatalogContent catalogContent;
        static void Main(string[] args)
        {
            catalogContent = new CatalogContent();
            CatalogItem catalogItem = new CatalogItem {
                Name = "Orange", Price=80,Quantity=50,
                Vendors = new List<Vendor> {
                    new Vendor{ Id=1, Name="ABC"},
                    new Vendor{ Id=2, Name="XYZ"},
                    new Vendor{ Id=3, Name="DEF"},
                }
            };
            InsertItem(catalogItem);
            GetCatalogItems();
            Console.ReadLine();            
        }
        private static void InsertItem(CatalogItem catalogItem)
        {
            catalogContent.CatalogItems.InsertOne(catalogItem);
        }
        private static void GetCatalogItems()
        {
            var items = catalogContent.CatalogItems.FindAsync<CatalogItem>(FilterDefinition<CatalogItem>.Empty);
            var catalogItems = items.Result.ToList();
            foreach (var item in catalogItems)
            {
                Console.WriteLine(item.Name,item.Price,item.Quantity);
            }
        }
    }
}
