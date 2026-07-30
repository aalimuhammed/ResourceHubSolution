using Microsoft.EntityFrameworkCore;
using ResourceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Infrastructure.Contexts
{
    public class ResourceHubDbContext:DbContext
    { 
        public ResourceHubDbContext(DbContextOptions<ResourceHubDbContext> options) : base(options){ }
        public DbSet<Service> Services { get; set; }
    }
}
