using AguiaLeague.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AguiaLeague.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteController : ControllerBase
    {
        private readonly ITimeService _timeService;

        public TesteController(ITimeService timeService)
        {
            _timeService = timeService;
        }

        [HttpGet]
        public string Teste()
        {
            var result = _timeService.ObterPorId(Guid.Empty);
            return "Ok";
        }
    }
}