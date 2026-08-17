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
    internal class TokenService: IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtsettings;

        public TokenService(IOptions<JwtSettings> jwtsettings)
        {
            _jwtsettings = jwtsettings.Value;
        }

        public string GenerateToken(Users user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsettings.secretKey));
            
            var credentials= new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token= new JwtSecurityToken(
                claims: claims,
                issuer: _jwtsettings.issuer,
                audience: _jwtsettings.audience,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
