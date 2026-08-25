using ResourceHub.Domain.Entities;
using ResourceHub.Infrastructure.Contexts;

namespace ResourceHub.Infrastructure.Seed
{
  public static class SeedData
  {
        public static async Task InitializeData(ResourceHubDbContext context)
        {
             await SeedUsers(context);
        }
         public static async Task SeedUsers(ResourceHubDbContext context)
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
                         }
                   );
                    await context.SaveChangesAsync();
             }
         }
        }
}
