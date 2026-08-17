using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;

namespace ResourceHub.Application.CQRS.Commands.Handlers
{
    internal class LoginHandler : ICommandRequestHandler<LoginCommand , LoginResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenJenerator;

        public LoginHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenJenerator)
        {
            _userRepository = userRepository;
            _jwtTokenJenerator = jwtTokenJenerator;
        }
        public async Task<LoginResponseDto> HandlerAsync(LoginCommand request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _userRepository.LoginAsync(request.LoginDto, cancellationToken);
                var token = _jwtTokenJenerator.GenerateToken(user);

                return new LoginResponseDto
                {
                    Token = token,
                    userName = user.FullName
                };
            }
            catch (KeyNotFoundException ex) 
            {
                throw new KeyNotFoundException($"{ex.Message}");
            } 
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException($"{ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while processing the login request :{ex.Message}",ex);
            }
        }
    }
}
