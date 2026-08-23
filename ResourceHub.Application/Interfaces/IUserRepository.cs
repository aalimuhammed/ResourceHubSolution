using ResourceHub.Application.Dtos;
using ResourceHub.Domain.Entities;

namespace ResourceHub.Application.Interfaces
{
    public interface IUserRepository
	{
        Task<Users> LoginAsync(
            LoginDto loginDto, 
            CancellationToken cancellationToken = default);
        Task InsertNewUserAsync(
            CreateUserDto userDto , 
            CancellationToken cancellationToken = default);
    }
}