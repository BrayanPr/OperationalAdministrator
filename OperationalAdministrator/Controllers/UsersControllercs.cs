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
            User user = new User();

            // Set the properties of the user instance
            user = context.Users.Find(id);
            if (user == null) return user;
            user.HashPassword();  // Hash the user's password

            return user;
        }

        // POST api/<UsersControllercs>
        [HttpPost]
        public void Post([FromBody] User user)
        {
            user.HashPassword(); // Hash the user's password

            // Add the user to the context and save changes
            context.Users.Add(user);
            context.SaveChanges();
        }

        // PUT api/<UsersControllercs>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            User existingUser = context.Users.Find(id);

            if (existingUser != null)
            {
                // Update the existing user's properties with the new values
                existingUser.Name = user.Name;
                existingUser.Password = user.Password;
                existingUser.Balance = user.Balance;
                existingUser.HashPassword(); // Hash the user's password

                // Save changes to the context
                context.SaveChanges();
            }
        }

        // DELETE api/<UsersControllercs>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            User userToDelete = context.Users.Find(id);

            if (userToDelete != null)
            {
                // Remove the user from the context and save changes
                context.Users.Remove(userToDelete);
                context.SaveChanges();
            }
        }
    }
}
