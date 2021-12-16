using System.Linq.Expressions;
using AguiaLeague.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AguiaLeague.Data.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly AguiaLeagueContext _aguiaLeagueContext;

    public BaseRepository(AguiaLeagueContext aguiaLeagueContext)
    {
        _aguiaLeagueContext = aguiaLeagueContext;
    }

    public bool Adicionar(TEntity obj)
    {
        _aguiaLeagueContext.Entry(obj).State = EntityState.Added;
        var registros = _aguiaLeagueContext.SaveChanges();
        return registros > 0;
    }

    public IEnumerable<TEntity> Obter(Expression<Func<TEntity, bool>> expressaoLambda, string[]? includes = null)
    {
        var query = ObterEntidadeComIncludes(includes);
        return query.AsNoTracking().Where(expressaoLambda);
    }

    private IQueryable<TEntity> ObterEntidadeComIncludes(IReadOnlyList<string>? includes)
    {
        IQueryable<TEntity> query = _aguiaLeagueContext.Set<TEntity>();
        return includes == null ? query : includes.Aggregate(query, (current, include) => current.Include(include));
    }
}