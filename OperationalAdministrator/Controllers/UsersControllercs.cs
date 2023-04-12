using DB;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OperationalAdministrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        OperationalAdministratorContext context;

        public UsersController(OperationalAdministratorContext _context)
        {
            context = _context;
        }

        // GET: api/<UsersControllercs>
        [HttpGet]
        public IEnumerable<User> Get() => context.Users.ToList();

        // GET api/<UsersControllercs>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return context.Users.Find(id);
        }

        // POST api/<UsersControllercs>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsersControllercs>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersControllercs>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
