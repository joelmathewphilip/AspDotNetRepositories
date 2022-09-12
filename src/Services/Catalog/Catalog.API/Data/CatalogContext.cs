using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    //create mongo connection and seed data
    public class CatalogContext : ICatalogContext
    {
        //data layer to perform data operations on the entity or model Catalog.
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            //to create a database when not it does not have one
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            //CatalogContextSeed.SeedData(Products);
        }
       public IMongoCollection<Product> Products {get;}
    }
}
