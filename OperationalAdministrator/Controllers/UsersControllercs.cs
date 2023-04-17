using System;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Principal;
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
        [HttpGet("getProfile")]
        [Authorize]
        public IActionResult MyProfile()
        {
            int id = getId(HttpContext.User.Identity as ClaimsIdentity);

            return Ok(userService.getUser(id));
        }

        // GET: api/<UsersControllercs>
        [HttpGet]
        [Authorize]
        public IActionResult Get() 
        {
            if(!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized(Enumerable.Empty<User>());

            return Ok(userService.GetUsers());
        }
        // GET api/<UsersControllercs>/5
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();

            return Ok(userService.getUser(id));
        }

        // POST api/<UsersControllercs>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] UserDTO user)
        {
            if(user.role == "admin")
            {
                if (!verifySuperAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            }
            else
            {
                if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
                user.role = "user";
            }
            return Ok(userService.createUser(user));
        }

        // PUT api/<UsersControllercs>/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] UserDTO user)
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            return Ok(userService.replaceUser(id, user));
        }

        // DELETE api/<UsersControllercs>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            return userService.deleteUser(id) ? Ok() : StatusCode(500);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthRequest model)
        {
            AuthResponse response = userService.Auth(model);

            return response == null ? Unauthorized("error") : Ok(response);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool verifyAdmin(ClaimsIdentity identity)
        {
            string role = JWT.verifyToken(identity);
            return (role == "admin" || role == "super_admin");
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool verifySuperAdmin(ClaimsIdentity identity)
        {
            string role = JWT.verifyToken(identity);
            return (role == "super_admin");
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public int getId(ClaimsIdentity identity)
        {
            string _id = JWT.checkId(identity);
            return (int.Parse(_id));
        }
    }
}