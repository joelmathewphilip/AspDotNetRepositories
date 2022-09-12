using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    //repository layer for Product
    public interface IProductRepository
    {
        //read operations
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProductById(string id);

        Task<IEnumerable<Product>> GetProductsByName(string name);

        Task<IEnumerable<Product>> GetProductsByCategory(string category);

        //write operation
        Task CreateProduct(Product product);

        //update operation
        Task<bool> UpdateProduct(Product product);

        //delete operation
        Task<bool> DeleteProductById(string id);
    }
}
