using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;

namespace ResourceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IJwtTokenJenerator _jwtTokenJenerator;

        public AuthorizationController(
            IServiceRepository serviceRepository , 
            IJwtTokenJenerator jwtTokenJenerator)
        {
            _serviceRepository = serviceRepository;
            _jwtTokenJenerator = jwtTokenJenerator;
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _serviceRepository.LoginAsync(loginDto, cancellationToken);
                var token = _jwtTokenJenerator.GenerateToken(user);
                return Ok(new
                {
                    token = token
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);

            }
        }
    }
}
