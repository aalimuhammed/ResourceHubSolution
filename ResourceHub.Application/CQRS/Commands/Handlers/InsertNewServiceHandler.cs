using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.CQRS.Commands.Handlers
{
    public class InsertNewServiceHandler :
        ICommandRequestHandler<InsertNewServiceCommand>
    {
        private readonly IServiceRepository _serviceRepository;

        private readonly IUnitOfWork _unitOfWork;

        public InsertNewServiceHandler(IServiceRepository serviceRepository,IUnitOfWork unitOfWork)
        {
            _serviceRepository = serviceRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task HandlerAsync(InsertNewServiceCommand request, CancellationToken cancellationToken = default)
        {
           await _serviceRepository.InsertNewService(request.ServiceDto, cancellationToken);

           await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
