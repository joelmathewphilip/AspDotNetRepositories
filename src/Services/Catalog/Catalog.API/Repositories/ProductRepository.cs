using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    //repository pattern to create/update data in the database. This separates business logic from
    //data access layer logic.
    public class ProductRepository : IProductRepository
    {
        //data driver
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task CreateProduct(Product product)
        {
            await _catalogContext
                .Products
                .InsertOneAsync(product);
        }

        public async Task<bool> DeleteProductById(string id)
        {
            var deletedResult = await _catalogContext
                .Products
                .DeleteOneAsync(p => p.Id == id);

            return deletedResult.IsAcknowledged && deletedResult.DeletedCount > 0;
            
        }

        public async Task<Product> GetProductById(string id)
        {
            return await _catalogContext
                .Products
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext
                    .Products
                    .Find(p => true) //mongo cli operation to find documents using mongo db driver
                    .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            return await _catalogContext
                .Products
                .Find(p => p.Category == category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _catalogContext
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updatedResult = await _catalogContext
                .Products
                .ReplaceOneAsync(p => p.Id == product.Id, replacement: product);

            return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
        }

        
    }
}
