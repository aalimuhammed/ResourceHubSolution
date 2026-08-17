using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;
using ResourceHub.Domain.Entities;
using ResourceHub.Infrastructure.Contexts;
using System.Data;

namespace ResourceHub.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ResourceHubDbContext _resourceHubDbContext;
        private readonly IPasswordService _passwordService;
        private readonly IGenericRepository<Users> _genericUserRepository;

        public UserRepository(
            ResourceHubDbContext resourceHubDbContext,
            IPasswordService passwordService,
            IGenericRepository<Users> genericUserRepository
            )
        {
            _resourceHubDbContext = resourceHubDbContext;
            _passwordService = passwordService;
            _genericUserRepository = genericUserRepository;
        }

        public async Task AddNewUserAsync(CreateUserDto userDto, CancellationToken cancellationToken)
        {

            if( await _genericUserRepository.FindByAnyAsync(u => u.Email == userDto.Email))
            {
                throw new DuplicateNameException("User already Exists");
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
            var user = await _genericUserRepository.GetByFirstOrDefault(u => u.Email == loginDto.email);

            if (user is null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            bool verifyPassword = _passwordService.VerifyPassword(loginDto.password, user.Password);

            if (!verifyPassword)
            {
                throw new UnauthorizedAccessException("Invalid Email Or Password.");
            }

            return user;
        }
    }
}
