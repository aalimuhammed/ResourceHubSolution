using ResourceHub.Application.Dtos;
using ResourceHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.Interfaces
{
    public interface IUserInterface
    {
        Task<Users> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken);
        Task AddNewUserAsync(UserDto userDto , CancellationToken cancellationToken);
    }
}
