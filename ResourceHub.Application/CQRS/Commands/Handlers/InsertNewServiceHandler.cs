using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Interfaces;
using ResourceHub.Domain.Entities;

namespace ResourceHub.Application.CQRS.Commands.Handlers
{
    internal class InsertNewServiceHandler :
        ICommandRequestHandler<InsertNewServiceCommand>
    {
        private readonly IGenericReposetory<Service> _serviceGenericRepo;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;
        public InsertNewServiceHandler(
            IGenericReposetory<Service> serviceGenericRepo,
            IServiceRepository serviceRepository,
            IUnitOfWork unitOfWork)
        {
            _serviceGenericRepo = serviceGenericRepo;
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task HandlerAsync(InsertNewServiceCommand ? request, CancellationToken cancellationToken = default)
        {
            bool isfound = _serviceRepository.isActivityNoFounded(request.ServiceDto.ActivityNo);

            if (isfound)
            {
                throw new Exception($"Service with ActivityNo '{request.ServiceDto.ActivityNo}' already exists.");
            }

            if (request is null)
            {
                throw new Exception("The Request is Empty");
            }

            await _serviceRepository.InsertNewService(request.ServiceDto, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
