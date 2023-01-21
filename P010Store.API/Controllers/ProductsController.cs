using Microsoft.AspNetCore.Mvc;
using P010Store.Entities;
using P010Store.Service.Absract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P010Store.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IService<Product> _service;

        public ProductsController(IProductService productService, IService<Product> service)
        {
            _productService = productService;
            _service = service;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await _productService.GetAllProductsByCategoriesBrandsAsync();
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<Product> GetAsync(int id)
        {
            return await _productService.GetProductByCategoriesBrandsAsync(id);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<Product> PostAsync([FromBody] Product value)
        {
            await _productService.AddAsync(value);
            await _productService.SaveChangesAsync();
            return value;
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Product value)
        {
            _productService.Update(value);
            _productService.SaveChanges();
            return NoContent();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _productService.Find(id);
            if (model == null)
            {
                return BadRequest();
            }
            _productService.Delete(model);
            _productService.SaveChanges();
            return StatusCode(StatusCodes.Status200OK);

        }
    }
}
