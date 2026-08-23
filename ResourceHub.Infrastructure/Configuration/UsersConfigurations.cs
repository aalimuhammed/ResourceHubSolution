using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceHub.Domain.Entities;

namespace ResourceHub.Infrastructure.Configuration
{
    internal class UsersConfigurations : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasIndex(u => new { u.Email })
                .IsUnique();

            builder.HasIndex(u => new { u.UserName })
                .IsUnique();
        }
    }
}