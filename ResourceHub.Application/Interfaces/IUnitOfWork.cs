using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}
