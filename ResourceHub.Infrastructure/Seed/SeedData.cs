using ResourceHub.Domain.Entities;
using ResourceHub.Infrastructure.Contexts;

namespace ResourceHub.Infrastructure.Seed
{
  public static class SeedData
  {
        public static async Task InitializeData(ResourceHubDbContext context)
        {
             await SeedDepartments(context);
        }

         public static async Task SeedDepartments(ResourceHubDbContext context)
         {
             if (!context.Users.Any())
                 {
                   await context.Users.AddRangeAsync(
                         new Users
                         {
                             Email = "Shada.adly@gmail.com",
                             Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                             FullName = "Shada Adly",
                             UserName= "shada.adly",
                             CreatedAt = DateTime.Now,
                         }
                   );
                    await context.SaveChangesAsync();
             }
         }
        }
}

