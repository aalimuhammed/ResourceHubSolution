using ResourceHub.Application.Common.Mediator;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Exceptions;
using ResourceHub.Application.Interfaces;
using ResourceHub.Domain.Entities;

namespace ResourceHub.Application.CQRS.Query.Handlers
{
    internal class GetServiceWithActivityNumberHandler :
        IQueryRequestHandler<GetServiceWithActivityNumberQuery, ServiceDto>
    {
        private readonly IGenericRepository<Service> _serviceRepository;
        public GetServiceWithActivityNumberHandler(IGenericRepository<Service> serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<ServiceDto> HandlerAsync(
            GetServiceWithActivityNumberQuery request,
            CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetByFirstOrDefault(
                s => s.ActivityNo == request.activityNumber,
                cancellationToken);

            if (service == null)
            {
                throw new NotFoundException($"Service with ActivityNo '{request.activityNumber}' not found.");
            }

            var serviceDto = new ServiceDto()
            {
                ActivityNo = service.ActivityNo,
                CreatedOn = service.CreatedOn,
                CreatedBy = service.CreatedBy,
                ChangedOn = service.ChangedOn,
                ChangedBy = service.ChangedBy,
                MaterialGroup = service.MaterialGroup,
                ServiceCat = service.ServiceCat,
                Division = service.Division,
                DeletionInd = service.DeletionInd,
                ValuationClass = service.ValuationClass,
                PrimaryLang = service.PrimaryLang,
                ShortTxt = service.ShortTxt,
                Unit = service.Unit,
                LongTxt = service.LongTxt
            };

            return serviceDto;
        }
    }
}
