using Microsoft.EntityFrameworkCore;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;
using ResourceHub.Domain.Entities;
using ResourceHub.Infrastructure.Contexts;

namespace ResourceHub.Infrastructure.Repositories
{
    public class UserRepository : IUserInterface
    {
        private readonly ResourceHubDbContext _resourceHubDbContext;
        private readonly IPasswordService _passwordService;

        public UserRepository(ResourceHubDbContext resourceHubDbContext, IPasswordService passwordService)
        {
            _resourceHubDbContext = resourceHubDbContext;
            _passwordService = passwordService;
        }

        public async Task AddNewUserAsync(UserDto userDto, CancellationToken cancellationToken)
        {
            if (userDto is null || !CheckNullability(userDto))
            {
                throw new Exception($"Fields Can't be null");
            }
            try
            {
                var user = new Users
                {
                    FullName = userDto.FullName,
                    UserName = userDto.UserName,
                    Password = _passwordService.HashPassword(userDto.Password),
                    Email = userDto.Email
                };
               await _resourceHubDbContext.Users.AddAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding user: {ex.Message}");
            }
        }

        public async Task<Users> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var user = await _resourceHubDbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.email);

            if (user is null)
            {
                throw new Exception("User not found.");
            }

            bool verifyPassword = _passwordService.VerifyPassword(loginDto.password, user.Password);

            if (!verifyPassword)
            {
                throw new Exception("Invalid Email Or Password.");
            }

            return user;
        }

        private bool CheckNullability(UserDto userDto)
        {
            if (
                userDto.FullName is null ||
                userDto.UserName is null ||
                userDto.Password is null ||
                userDto.Email is null
                )
            {
                return false;
            }
            else { return true; }
        }
    }
}
