using ResourceHub.Application.Interfaces;
using ResourceHub.Infrastructure.Contexts;

namespace ResourceHub.Infrastructure.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ResourceHubDbContext _context;
        public UnitOfWork(ResourceHubDbContext context)
        {
            _context = context;
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}