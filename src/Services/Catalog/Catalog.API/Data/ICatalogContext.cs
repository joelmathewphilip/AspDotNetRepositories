using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    //Interface for CatalogContext
    public interface ICatalogContext
    {

        IMongoCollection<Product> Products { get; } 

    }
}
