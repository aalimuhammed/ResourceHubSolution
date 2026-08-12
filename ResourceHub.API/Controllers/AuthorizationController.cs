using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.CQRS.Commands;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;

namespace ResourceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IJwtTokenJenerator _jwtTokenJenerator;
        private readonly IUserInterface _userRepository;
        private readonly IMediator _mediator;

        public AuthorizationController(
            IJwtTokenJenerator jwtTokenJenerator,
            IUserInterface userRepository,
            IMediator mediator
            )
        {
            _jwtTokenJenerator = jwtTokenJenerator;
            _userRepository = userRepository;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Login(
        [FromBody] LoginDto loginDto,
        CancellationToken cancellationToken)
        {
            try
            {
                var token = await _mediator.SendCommandAsync<LoginCommand, string>(
                    new LoginCommand(loginDto),
                    cancellationToken);

                return Ok(new
                {
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
