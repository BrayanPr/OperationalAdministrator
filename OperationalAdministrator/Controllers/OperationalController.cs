using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationalAdministrator.Models;

namespace OperationalAdministrator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationalController : ControllerBase
    {
        [HttpPost("move")]
        public void MoveEmpoyees([FromBody] AuthRequest model)
        {

        }
    }
}
