using FluentValidation;
using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ResourceHub.Application.CQRS.Commands.Handlers
{
    internal class AddNewUserHandler : ICommandRequestHandler<AddNewUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateUserDto> _validator;

        public AddNewUserHandler(
            IUserRepository userRepository ,
            IUnitOfWork unitOfWork,
            IValidator<CreateUserDto> validator 
            )
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task HandlerAsync(AddNewUserCommand request, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(request.UserDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors.First().ErrorMessage);
            }

                await _userRepository.AddNewUserAsync(request.UserDto, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}
