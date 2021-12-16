using AguiaLeague.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AguiaLeague.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TesteController : ControllerBase
    {
        [HttpGet]
        public object Teste()
        {
            return new
            {
                teste  = "teste"
            };
        }
    }
}