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
        public History MoveEmpoyees([FromBody] MoveUserRequest request)
        {
            return service.MoveUser(request.userID, request.teamID);
        }
        [HttpGet("history")]
        public IEnumerable<History> GetHistory()
        {
            return service.GetHistories();
        }
        [HttpPost("history/date")]
        public IEnumerable<History> GetHistoryByDates([FromBody] RequestHistoryByDate request)
        {
            return service.GetHistoriesByDates(request.startDate, request.endDate);
        }
        [HttpGet("history/user")]
        public IEnumerable<History> GetHistoryByUser([FromQuery] int userId)
        {
            return service.GetHistoriesByUser(userId);
        }
        [HttpGet("history/team")]
        public IEnumerable<History> GetHistoryByTeam([FromQuery] int teamId)
        {
            return service.GetHistoriesByTeam(teamId);
        }
    }
}
