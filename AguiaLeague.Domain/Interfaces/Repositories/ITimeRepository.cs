using AguiaLeague.Domain.Models;

namespace AguiaLeague.Domain.Interfaces.Repositories;

public interface ITimeRepository : IBaseRepository<Time>
{
    Time? ObterPorId(Guid id, string[]? includes = null);
}