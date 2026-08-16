using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Interfaces;

namespace ResourceHub.Application.CQRS.Commands.Handlers
{
    internal class LoginHandler : ICommandRequestHandler<LoginCommand , string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenJenerator;

        public LoginHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenJenerator)
        {
            _userRepository = userRepository;
            _jwtTokenJenerator = jwtTokenJenerator;
        }
        public async Task<string> HandlerAsync(LoginCommand request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _userRepository.LoginAsync(request.LoginDto, cancellationToken);
                var token = _jwtTokenJenerator.GenerateToken(user);
                return token;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while processing the login request: {ex.Message}");
            }
        }
    }
}
