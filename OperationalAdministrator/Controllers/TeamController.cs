using System.Security.Claims;
using DB.Models;
using DB.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OperationalAdministrator.Common;
using OperationalAdministrator.Services;
using OperationalAdministrator.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OperationalAdministrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        ITeamService service;
        public TeamController(ITeamService _service) {
            service = _service;
        }

        // GET: api/<TeamController>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized(Enumerable.Empty<Team>());
            return Ok(service.GetTeams());
        }

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            return Ok(service.getTeam(id));
        }

        // POST api/<TeamController>
        [HttpPost]
        public IActionResult Post([FromBody] TeamDTO team)
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            return Ok(service.createTeam(team));
        }

        // PUT api/<TeamController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TeamDTO team)
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            return Ok(service.replaceTeam(id, team));
        }

        // DELETE api/<TeamController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();

            return Ok(service.deleteTeam(id));
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool verifyAdmin(ClaimsIdentity identity)
        {
            string role = JWT.verifyToken(identity);
            return (role == "admin" || role == "super_admin");
        }
    }
}
