using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AguiaLeague.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class TesteController : ControllerBase
    {
        [HttpGet]
        public object Teste()
        {
            return "oi";
        }
    }
}