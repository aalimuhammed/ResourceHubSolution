using ResourceHub.Application.Dtos;
using ResourceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.Interfaces
{
    public interface IUserRepository
	{
        Task<Users> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken);
        Task AddNewUserAsync(CreateUserDto userDto , CancellationToken cancellationToken);
    }
}
