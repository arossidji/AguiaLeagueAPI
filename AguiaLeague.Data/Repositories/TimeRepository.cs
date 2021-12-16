using AguiaLeague.Domain.Interfaces.Repositories;
using AguiaLeague.Domain.Models;

namespace AguiaLeague.Data.Repositories;

public class TimeRepository : BaseRepository<Time>, ITimeRepository
{
    public TimeRepository(AguiaLeagueContext context) : base(context) {}
    
    public Time? ObterPorId(Guid id, string[]? includes = null)
    {
        return Obter(x => x.Id == id).FirstOrDefault();
    }
}