using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{


    //presentation layer
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        //here we are defining the response, saying that we can expect a response of IEnumerable<Product> type with
        //status code 200
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        //ActionResult provides the responses written by Microsoft to the client
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            //returning Ok response to the client
            return Ok(await _repository.GetProducts());
        }

        [Route("{id:length(24)}", Name = "GetProduct")]
        [HttpGet]
        //here we are defining the response, saying that we can expect a response which contains Product and status code
        // is 200
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var result = await _repository.GetProductById(id);
            if (result == null)
            {
                _logger.LogError($"Product with id: {id} was not found");
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [Route("[action]/{category}", Name ="GetProductByCategory")]
        [HttpGet]
        //response definition
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string categoryName)
        {
            var resultProducts = await _repository.GetProductsByCategory(categoryName);
            if(resultProducts == null)
            {
                _logger.LogError($"Products with the categoryName was not found");
                return NotFound();
            }
            else
            {
                return Ok(resultProducts);
            }
        }

        
        [HttpPost]
        [ProducesResponseType(typeof (Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody]Product product)
        {
            await _repository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _repository.UpdateProduct(product)); 
            
        }

        [HttpDelete("{id:length(24)}",Name ="DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _repository.DeleteProductById(id));

        }
    }
}
