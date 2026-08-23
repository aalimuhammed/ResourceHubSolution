using System.Linq.Expressions;

namespace ResourceHub.Application.Interfaces
{
    public interface IGenericRepository<T> 
    {
         Task<T?> GetByFirstOrDefault(
            Expression<Func<T, bool>> filterByCondition,
            CancellationToken cancellationToken = default);
         Task<bool> FindByAnyAsync(
            Expression<Func<T, bool>>? filterByCondition = null,
            CancellationToken cancellationToken = default);
         Task<int?> FindMaxAsync(
            Expression<Func<T, int?>> filterByCondition,
            CancellationToken cancellationToken = default);
    }
}
