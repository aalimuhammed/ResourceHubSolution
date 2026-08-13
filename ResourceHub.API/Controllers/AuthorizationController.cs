using Microsoft.AspNetCore.Mvc;
using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.CQRS.Commands;
using ResourceHub.Application.Dtos;

namespace ResourceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationController(
            IMediator mediator
            )
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
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
        [HttpPost("AddNewUser")]
        public async Task<ActionResult> AddNewUserController([FromBody] UserDto userDto, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.SendCommandAsync<AddNewUserCommand>(
                     new AddNewUserCommand(userDto), cancellationToken
                     );
                return Ok(new
                {
                    Message = "User added successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
