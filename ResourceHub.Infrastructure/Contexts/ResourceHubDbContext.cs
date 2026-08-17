using Microsoft.EntityFrameworkCore;
using ResourceHub.Domain.Entities;

namespace ResourceHub.Infrastructure.Contexts
{
    public class ResourceHubDbContext:DbContext
    { 
        public ResourceHubDbContext(DbContextOptions<ResourceHubDbContext> options) : base(options){ }
        
        public DbSet<Service> Services { get; set; }
        public DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ResourceHubDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}