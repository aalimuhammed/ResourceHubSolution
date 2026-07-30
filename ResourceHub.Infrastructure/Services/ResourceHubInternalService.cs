using Microsoft.EntityFrameworkCore;
using ResourceHub.Application.Interfaces;
using ResourceHub.Domain.Entities;
using ResourceHub.Infrastructure.Contexts;

namespace ResourceHub.Infrastructure.Services
{
    public class ResourceHubInternalService : IResourceHubInternalService
    {
        private readonly IResourceHubExternalService _resourceHubExternalService;
        private readonly ResourceHubDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public ResourceHubInternalService(
            IResourceHubExternalService resourceHubExternalService ,
            ResourceHubDbContext context,
            IUnitOfWork unitOfWork)
        {
            _resourceHubExternalService = resourceHubExternalService;
            _context = context;
            _unitOfWork = unitOfWork;
        }
        public async Task ImportFromSapAsync(CancellationToken cancellationToken=default)
        {
            int pagenumber = 1;
            int pagesize = 100;

            while(true)
            {
                var result = await _resourceHubExternalService.GetServicePageAsync(pagenumber , pagesize , cancellationToken);
   
                var dto = result.Services;

                foreach (var servicedto in dto)
                {
                    var isExist = await _context.Services.FirstOrDefaultAsync(
                        x => x.ActivityNo == servicedto.ActivityNo,
                        cancellationToken);

                    if (isExist != null)
                    {
                        continue;
                    }
                    else
                    {
                        Service service = new Service()
                        {
                            ActivityNo = servicedto.ActivityNo,
                            ChangedBy = servicedto.ChangedBy,
                            MaterialGroup = servicedto.MaterialGroup,
                            LongTxt = servicedto.LongTxt,
                            ChangedOn = servicedto.ChangedOn,
                            DeletionInd = servicedto.DeletionInd,
                            CreatedBy = servicedto.CreatedBy,
                            CreatedOn = servicedto.CreatedOn,
                            Division = servicedto.Division,
                            PrimaryLang = servicedto.PrimaryLang,
                            ServiceCat = servicedto.ServiceCat,
                            ShortTxt = servicedto.ShortTxt,
                            Unit = servicedto.Unit,
                            ValuationClass = servicedto.ValuationClass,
                        };

                        _context.Services.Add(service);
                    }
                }


                await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (!result.HasMore)
                {
                    break;
                }
                pagenumber++;

            }


        }
    }
}
