using FluentValidation;
using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Exceptions;
using ResourceHub.Application.Interfaces;

namespace ResourceHub.Application.CQRS.Commands.Handlers
{
    internal class InsertNewUserHandler : ICommandRequestHandler<InsertNewUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateUserDto> _validator;
        public InsertNewUserHandler(
            IUserRepository userRepository ,
            IUnitOfWork unitOfWork,
            IValidator<CreateUserDto> validator)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task HandlerAsync(InsertNewUserCommand request, CancellationToken cancellationToken = default)
        {

            var validationResult = await _validator.ValidateAsync(request.UserDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new VaildateException(validationResult.Errors.First().ErrorMessage);
            }

            await _userRepository.InsertNewUserAsync(request.UserDto, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
