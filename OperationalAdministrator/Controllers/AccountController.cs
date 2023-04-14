using DB.Models;
using DB.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<Account> Get()
        {
            return service.GetAccounts();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public Account Get(int id)
        {
            return service.getAccount(id);
        }

        // POST api/<AccountController>
        [HttpPost]
        public Account Post([FromBody] AccountDTO account)
        {
            return service.createAccount(account);
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.deleteAccount(id);
        }
    }
}
