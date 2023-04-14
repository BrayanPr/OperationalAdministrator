using DB.Models;
using DB.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<Team> Get()
        {
            return service.GetTeams();
        }

        // GET api/<TeamController>/5
        [HttpGet("{id}")]
        public Team Get(int id)
        {
            return service.getTeam(id);
        }

        // POST api/<TeamController>
        [HttpPost]
        public Team Post([FromBody] TeamDTO team)
        {
            return service.createTeam(team);
        }

        // PUT api/<TeamController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] TeamDTO team)
        {
            service.replaceTeam(id, team);
        }

        // DELETE api/<TeamController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            service.deleteTeam(id);
        }
    }
}
