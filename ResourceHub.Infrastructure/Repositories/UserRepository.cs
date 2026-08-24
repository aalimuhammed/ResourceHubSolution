using ResourceHub.Application.Dtos;
using ResourceHub.Application.Exceptions;
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
            IGenericRepository<Users> genericUserRepository)
        {
            _resourceHubDbContext = resourceHubDbContext;
            _passwordService = passwordService;
            _genericUserRepository = genericUserRepository;
        }

        public async Task InsertNewUserAsync(CreateUserDto userDto, CancellationToken cancellationToken)
        {
            if(await _genericUserRepository.FindByAnyAsync(u => u.Email == userDto.Email))
            {
                throw new DuplicateValueException("User already Exists");
            }

            var user = new Users
            {
                FullName = userDto.FullName,
                UserName = userDto.UserName,
                Password = _passwordService.HashPassword(userDto.Password),
                Email = userDto.Email
            };

            await _resourceHubDbContext.Users.AddAsync(user);
        }

        public async Task<Users> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var user = await _genericUserRepository.GetByFirstOrDefault(u => u.Email == loginDto.email);

            if (user is null)
            {
                throw new NotFoundException("User not found.");
            }

            bool verifyPassword = _passwordService.VerifyPassword(loginDto.password, user.Password);

            if (!verifyPassword)
            {
                throw new UnAuthenticatedException("Invalid Email Or Password.");
            }

            return user;
        }
    }
}
