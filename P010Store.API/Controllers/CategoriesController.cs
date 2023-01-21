using Microsoft.AspNetCore.Mvc;
using P010Store.Entities;
using P010Store.Service.Absract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P010Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await _categoryService.GetAllAsync();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<Category> GetAsync(int id)
        {
            return await _categoryService.GetCategoryByProducts(id);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<Category> PostAsync([FromBody] Category value)
        {
            await _categoryService.AddAsync(value);
            await _categoryService.SaveChangesAsync();
            return value;
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category value)
        {
            _categoryService.Update(value);
            _categoryService.SaveChanges();
            return NoContent();
        }

        // DELETE api/<BrandsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _categoryService.Find(id);
            if (model == null)
            {
                return BadRequest();
            }
            _categoryService.Delete(model);
            _categoryService.SaveChanges();
            return StatusCode(StatusCodes.Status200OK);

        }
    }
}
