using Microsoft.EntityFrameworkCore.Storage;
using ResourceHub.Application.Interfaces;
using ResourceHub.Infrastructure.Contexts;
using System.Data;

namespace ResourceHub.Infrastructure.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ResourceHubDbContext _context;
        private IDbContextTransaction? transaction;
        public UnitOfWork(ResourceHubDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            if (transaction is not null)
            {
                throw new InvalidOperationException("Transaction still in Progress");
            }

            transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            if (transaction is null)
            {
                throw new InvalidOperationException("There is no Transactions");
            }

            await transaction.CommitAsync(cancellationToken);
            await transaction.DisposeAsync();
            transaction = null;
        }


        public async Task RollBackTransactionAsync(CancellationToken cancellationToken)
        {
            if (transaction is null)
            {
                throw new InvalidOperationException("There is no Transactions");
            }

            await transaction.RollbackAsync(cancellationToken);
            await transaction.DisposeAsync();
            transaction = null;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}