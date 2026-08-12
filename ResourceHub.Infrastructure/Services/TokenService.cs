using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;
using ResourceHub.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ResourceHub.Infrastructure.Services
{
    internal class TokenService: IJwtTokenJenerator
    {
        private readonly IOptions<JwtSettings> _userOptions;

        public TokenService(IOptions<JwtSettings> userOptions)
        {
            _userOptions = userOptions;
        }

        public string GenerateToken(Users user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_userOptions.Value.SecretKey));
            
            var credentials= new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token= new JwtSecurityToken(
                claims: claims,
                issuer: _userOptions.Value.issuer,
                audience: _userOptions.Value.audience,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
