using ResourceHub.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ResourceHub.Application.Interfaces
{
    public interface IGenericReposetory<T> 
    {
         Task<T?> GetByFirstOrDefault(
            Expression<Func<T, bool>> filterByCondition,
            CancellationToken cancellationToken = default);
         Task<bool> FindByAnyAsync(
            Expression<Func<T, bool>>? filterByCondition = null,
            CancellationToken cancellationToken = default);
        public Task<int> FindMaxAsync(
            Expression<Func<T, int>> filterByCondition,
            CancellationToken cancellationToken = default);
    }
}
