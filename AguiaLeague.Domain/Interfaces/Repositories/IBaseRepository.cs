using System.Linq.Expressions;

namespace AguiaLeague.Domain.Interfaces.Repositories;

public interface IBaseRepository<T>
{
    bool Adicionar(T obj);
    IEnumerable<T> Obter(Expression<Func<T, bool>> expressaoLambda, string[]? includes = null);
}