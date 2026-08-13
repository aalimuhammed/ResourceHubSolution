using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.CQRS.Commands.Handlers
{
    internal class AddNewUserHandler : ICommandRequestHandler<AddNewUserCommand>
    {
        private readonly IUserInterface _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddNewUserHandler(IUserInterface userRepository ,IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task HandlerAsync(AddNewUserCommand request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentNullException("request is null");
                }

                await _userRepository.AddNewUserAsync(request.UserDto, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

            } catch (Exception ex)
            {
                throw new Exception($"An error occurred while inserting the new User: {ex.Message}");
            }
        }
    }
}
