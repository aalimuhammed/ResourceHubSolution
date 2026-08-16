using FluentValidation;
using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;

namespace ResourceHub.Application.CQRS.Commands.Handlers
{
    internal class InsertNewServiceHandler :
        ICommandRequestHandler<InsertNewServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ServiceDto> _validator;

        public InsertNewServiceHandler(
            IServiceRepository serviceRepository,
            IUnitOfWork unitOfWork,
            IValidator<ServiceDto> validator
            )
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }
        public async Task HandlerAsync(InsertNewServiceCommand request, CancellationToken cancellationToken = default)
        { 
            var vaildationResult = await _validator.ValidateAsync(request.ServiceDto, cancellationToken);

            if (!vaildationResult.IsValid)
            {
                throw new ValidationException(vaildationResult.Errors);
            }

            bool isActivityNoFound = await _serviceRepository.IsActivityNoExists(request.ServiceDto.ActivityNo);

            if (isActivityNoFound)
            {
                throw new Exception($"Service with ActivityNo '{request.ServiceDto.ActivityNo}' already exists.");
            }

            try
            {
                if (request is null)
                {
                    throw new Exception("The Request is Empty");
                }

                await _serviceRepository.InsertNewService(request.ServiceDto, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex) {
                throw new Exception($"An error occurred while inserting the new service: {ex.Message}");
            }
            
        }
    }
}
