using Microsoft.AspNetCore.Mvc;
using P010Store.Entities;
using P010Store.Service.Absract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P010Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IService<User> _service;

        public UsersController(IService<User> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _service.GetAllAsync();
        }

        // GET api/<BrandsController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _service.Find(id);
        }

        // POST api/<BrandsController>
        [HttpPost]
        public User Post([FromBody] User value)
        {
            _service.Add(value);
            _service.SaveChanges();
            return value;
        }

        // PUT api/<BrandsController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] User value)
        {
            _service.Update(value);
            _service.SaveChanges();
            return NoContent();
        }

        // DELETE api/<BrandsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _service.Find(id);
            if (model == null)
            {
                return BadRequest();
            }
            _service.Delete(model);
            _service.SaveChanges();
            return StatusCode(StatusCodes.Status200OK);

        }
    }
}
