using System.Security.Claims;
using DB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationalAdministrator.Common;
using OperationalAdministrator.Models;
using OperationalAdministrator.Services;
using OperationalAdministrator.Services.Interfaces;

namespace OperationalAdministrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationalController : ControllerBase
    {
        private IOperationalService service { get; set; }

        public OperationalController(IOperationalService service)
        {
            this.service = service;
        }

        [HttpPost("move")]
        public IActionResult MoveEmpoyees([FromBody] MoveUserRequest request)
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            var res = service.MoveUser(request.userID, request.teamID);
            return Ok(res);
        }
        [HttpGet("history")]
        public IActionResult GetHistory()
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            return Ok(service.GetHistories());
        }
        [HttpPost("history/date")]
        public IActionResult GetHistoryByDates([FromBody] RequestHistoryByDate request)
        {

            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            return Ok(service.GetHistoriesByDates(request.startDate, request.endDate));
        }
        [HttpGet("history/user")]
        public IActionResult GetHistoryByUser([FromQuery] int userId)
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            return Ok(service.GetHistoriesByUser(userId));
        }
        [HttpGet("history/user/{userName}")]
        public IActionResult GetHistoryByUserName(string userName)
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            return Ok(service.GetHistoriesByUserName(userName));
        }
        [HttpGet("history/team")]
        public IActionResult GetHistoryByTeam([FromQuery] int teamId)
        {
            if (!verifyAdmin(HttpContext.User.Identity as ClaimsIdentity)) return Unauthorized();
            return Ok(service.GetHistoriesByTeam(teamId));
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool verifyAdmin(ClaimsIdentity identity)
        {
            string role = JWT.verifyToken(identity);
            return (role == "admin" || role == "super_admin");
        }
    }
}
