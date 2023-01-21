using Microsoft.AspNetCore.Mvc;
using P010Store.Entities;
using P010Store.Service.Absract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P010Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IService<Brand> _brandService;

        public BrandsController(IService<Brand> brandService)
        {
            _brandService = brandService;
        }

        // GET: api/<BrandsController>
        [HttpGet]
        public IEnumerable<Brand> Get()
        {
            return _brandService.GetAll(a=>a.IsActive);
        }

        // GET api/<BrandsController>/5
        [HttpGet("{id}")]
        public Brand Get(int id)
        {
            return _brandService.Find(id);
        }

        // POST api/<BrandsController>
        [HttpPost]
        public Brand Post([FromBody] Brand value)
        {
            _brandService.Add(value);
            _brandService.SaveChanges();
            return value;
        }

        // PUT api/<BrandsController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Brand value)
        {
            _brandService.Update(value);
            _brandService.SaveChanges();
            return NoContent();
        }

        // DELETE api/<BrandsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _brandService.Find(id);
            if (model==null)
            {
                return BadRequest();
            }
            _brandService.Delete(model);
            _brandService.SaveChanges();
            return StatusCode(StatusCodes.Status200OK);

        }
    }
}
