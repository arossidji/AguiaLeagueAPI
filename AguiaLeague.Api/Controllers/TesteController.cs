using AguiaLeague.Data;
using AguiaLeague.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AguiaLeague.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteController : ControllerBase
    {
        private readonly AguiaLeagueContext _aguiaLeagueContext;
        public TesteController(AguiaLeagueContext aguiaLeagueContext)
        {
            _aguiaLeagueContext = aguiaLeagueContext;
        }

        [HttpGet]
        public string Teste()
        {
            var time = new Time
            {
                Nome = "teste",
                Tag = "TES"
            };
            _aguiaLeagueContext.Entry(time).State = EntityState.Added;
            var registros = _aguiaLeagueContext.SaveChanges();
            return "Testando" + registros;
        }
    }
}