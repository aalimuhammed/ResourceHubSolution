using FluentValidation;
using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;
using ResourceHub.Application.Validators;

namespace ResourceHub.Application.CQRS.Commands.Handlers
{
    internal class LoginHandler : ICommandRequestHandler<LoginCommand , LoginResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenJenerator;
        private readonly IValidator<LoginDto> _loginValidator;

        public LoginHandler(
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenJenerator,
            IValidator<LoginDto> loginValidator
            )
        {
            _userRepository = userRepository;
            _jwtTokenJenerator = jwtTokenJenerator;
            _loginValidator = loginValidator;
        }
        public async Task<LoginResponseDto> HandlerAsync(LoginCommand request, CancellationToken cancellationToken = default)
        {
            var validationResult = await _loginValidator.ValidateAsync(request.LoginDto,cancellationToken);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.First().ErrorMessage);
            }

            var user = await _userRepository.LoginAsync(request.LoginDto, cancellationToken);

            var token = _jwtTokenJenerator.GenerateToken(user);

             return new LoginResponseDto
             {
                 Token = token,
                 userName = user.FullName
             };
        }
    }
}
