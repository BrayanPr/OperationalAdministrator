using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using DB.Models;
using DB.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OperationalAdministrator.Common;
using OperationalAdministrator.Models;
using OperationalAdministrator.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OperationalAdministrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService service)
        {
            userService = service;
        }

        // GET: api/<UsersControllercs>
        [HttpGet]
        [Authorize]
        public IEnumerable<User> Get() => userService.GetUsers();

        // GET api/<UsersControllercs>/5
        [HttpGet("{id}")]
        [Authorize]
        public User Get(int id)
        {
            return userService.getUser(id);
        }

        // POST api/<UsersControllercs>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] UserDTO user)
        {
            return Ok(userService.createUser(user));
        }

        // PUT api/<UsersControllercs>/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] UserDTO user)
        {
            userService.replaceUser(id, user);
        }

        // DELETE api/<UsersControllercs>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;

            int? userId = JWT.verifyToken(identity);

            if (userId == null)
            {
                return Unauthorized("Token incorrecto");
            }

            return userService.deleteUser(id) ? Ok() : StatusCode(500);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthRequest model)
        {
            AuthResponse response = userService.Auth(model);

            return response == null ? Unauthorized("error") : Ok(response);
        }
    }
}