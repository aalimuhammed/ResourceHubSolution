using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;
using ResourceHub.Domain.Entities;
using ResourceHub.Infrastructure.Contexts;
using ResourceHub.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

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
                throw new Exception("Invalid password.");
            }

            return user;
        }
    }
}
