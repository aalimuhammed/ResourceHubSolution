using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Interfaces;

namespace ResourceHub.Application.CQRS.Commands.Handlers
{
    internal class InsertNewServiceHandler :
        ICommandRequestHandler<InsertNewServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;

        private readonly IUnitOfWork _unitOfWork;
        public InsertNewServiceHandler(IServiceRepository serviceRepository,IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task HandlerAsync(InsertNewServiceCommand ? request, CancellationToken cancellationToken = default)
        {
            if(request is null)
            {
                throw new Exception("The Request is Empty");
            }

            await _serviceRepository.InsertNewService(request.ServiceDto, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
