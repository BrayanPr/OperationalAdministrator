using System.Security.Claims;
using DB.Models;
using DB.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OperationalAdministrator.Common;
using OperationalAdministrator.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OperationalAdministrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService service;
        public AccountController(IAccountService _service) 
        {
            service = _service;
        }

        // GET: api/<AccountController>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {

            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            return Ok(service.GetAccounts());
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            return Ok(service.getAccount(id));
        }

        // POST api/<AccountController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] AccountDTO account)
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            return Ok(service.createAccount(account));
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AccountDTO account)
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            return Ok(service.replaceAccount(id, account));
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            return Ok(service.deleteAccount(id));
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool verifyAdmin(ClaimsIdentity identity)
        {
            string role = JWT.verifyToken(identity);
            return (role == "admin" || role == "super_admin");
        }
    }
}
