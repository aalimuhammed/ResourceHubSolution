using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceHub.Domain.Entities;

namespace ResourceHub.Infrastructure.Configuration
{
    internal sealed class ServicesConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasIndex(b => new { b.ActivityNo })
               .IsUnique();
        }
    }
}