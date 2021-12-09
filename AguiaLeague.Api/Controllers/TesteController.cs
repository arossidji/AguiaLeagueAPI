using Microsoft.AspNetCore.Mvc;

namespace AguiaLeague.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteController : ControllerBase
    {
        [HttpGet]
        public string Teste()
        {
            return "Testando";
        }
    }
}