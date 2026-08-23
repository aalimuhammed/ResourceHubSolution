using Microsoft.EntityFrameworkCore;
using ResourceHub.Application.Interfaces;
using ResourceHub.Domain.Base;
using ResourceHub.Infrastructure.Contexts;
using System.Linq.Expressions;

namespace ResourceHub.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity 
    {
        private readonly DbSet<T> _set;
        public GenericRepository(ResourceHubDbContext context)
        {
            _set = context.Set<T>();
        }
        public async Task<T?> GetByFirstOrDefault(
            Expression<Func<T,bool>> ?filterByCondition = null ,
            CancellationToken cancellationToken = default)
        {
            if (filterByCondition is not null)
            {
                 return  await _set.Where(filterByCondition).FirstOrDefaultAsync(cancellationToken);
            }
            return await _set.FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<bool> FindByAnyAsync(
            Expression<Func<T,bool>>? filterByCondition = null,
            CancellationToken cancellationToken = default)
        {
            if(filterByCondition is not null)
            {
                return await _set.AnyAsync(filterByCondition,cancellationToken);
            }
            return await _set.AnyAsync(cancellationToken);
        }
        public async Task<int?> FindMaxAsync(
            Expression<Func<T,int?>> filterByCondition ,
            CancellationToken cancellationToken = default)
        {
            return await _set.MaxAsync(filterByCondition, cancellationToken);
        }
    }
}
