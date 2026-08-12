using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using ResourceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
