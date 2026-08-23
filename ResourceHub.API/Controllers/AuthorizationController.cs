using Microsoft.AspNetCore.Mvc;
using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.CQRS.Commands;
using ResourceHub.Application.Dtos;
using System.Data;

namespace ResourceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(
        [FromBody] LoginDto loginDto,
        CancellationToken cancellationToken)
        {

                var loginResponse = await _mediator.SendCommandAsync<LoginCommand, LoginResponseDto>(
                    new LoginCommand(loginDto),
                    cancellationToken);

                return Ok(loginResponse);


        }
        [HttpPost("AddNewUser")]
        public async Task<ActionResult> AddNewUser([FromBody] CreateUserDto userDto, CancellationToken cancellationToken)
        {

                await _mediator.SendCommandAsync<AddNewUserCommand>(
                     new AddNewUserCommand(userDto), cancellationToken
                     );
                return Ok(new
                {
                    Message = "User added successfully"
                });

        }
    }
}
