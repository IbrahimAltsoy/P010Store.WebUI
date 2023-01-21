using Microsoft.AspNetCore.Mvc;
using P010Store.Entities;
using P010Store.Service.Absract;
using P010Store.Service.Concreate;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P010Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IService<Contact> _contactService;

        public ContactsController(IService<Contact> contactService)
        {
            _contactService = contactService;
        }

        // GET: api/<ContactsController>
        [HttpGet]
        public async Task<IEnumerable<Contact>> GetAsync()
        {
            return await _contactService.GetAllAsync();
        }

        // GET api/<ContactsController>/5
        [HttpGet("{id}")]
        public async Task<Contact> GetAsync(int id)
        {
            return await _contactService.FindAsync(id);
        }

        // POST api/<ContactsController>
        [HttpPost]
        public async Task<Contact> PostAsync([FromBody] Contact value)
        {
            await _contactService.AddAsync(value);
            await _contactService.SaveChangesAsync();
            return value;
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public IActionResult Put( [FromBody] Contact value)
        {
            _contactService.Update(value);
            _contactService.SaveChanges();
            return NoContent();
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _contactService.Find(id);
            if (model == null)
            {
                return BadRequest();
            }
            _contactService.Delete(model);
            _contactService.SaveChanges();
            return StatusCode(StatusCodes.Status200OK);

        }
    }
}
