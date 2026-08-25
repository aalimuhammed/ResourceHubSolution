using ResourceHub.Domain.Entities;

namespace ResourceHub.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Users user);
    }
}