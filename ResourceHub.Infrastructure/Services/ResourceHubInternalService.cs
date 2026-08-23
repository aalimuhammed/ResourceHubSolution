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
            IResourceHubExternalService resourceHubExternalService,
            ResourceHubDbContext context,
            IUnitOfWork unitOfWork)
        {
            _resourceHubExternalService = resourceHubExternalService;
            _context = context;
            _unitOfWork = unitOfWork;
        }
        public async Task ImportFromSapAsync(
            CancellationToken cancellationToken = default)
        {
            const int pageSize = 100;

            int pageNumber = 1;
            int totalPages = 0;

            var existingActivityNos = await _context.Services
                .Select(x => x.ActivityNo)
                .ToHashSetAsync(cancellationToken);

            while (pageNumber <= totalPages || totalPages == 0)
            {
                var result = await _resourceHubExternalService.GetServicePageAsync(
                    pageNumber,
                    pageSize,
                    cancellationToken);

                if (totalPages == 0)
                {
                    totalPages = (int)Math.Ceiling(
                        (double)result.TotalCount / pageSize);
                }


                foreach (var dto in result.Services)
                {
                    // Skip duplicates already in DB or previous pages
                    // we create Hashset that contains all activity numbers so it holds only unique activity numbers 
                    // if there are any duplicate activity no it won't be saved into the hashset cause it can't include any duplicates
                    // so when we try to add a new item we first try to add its activityNo into this hashset 
                    // if it's added it means it's not repeated before so we can add this record into our table 
                    // else it means it's duplicated so we can't add it into our table.

                    if (!existingActivityNos.Add(dto.ActivityNo))
                        continue;

                    _context.Services.Add(new Service
                    {
                        ActivityNo = dto.ActivityNo,
                        ChangedBy = dto.ChangedBy,
                        MaterialGroup = dto.MaterialGroup,
                        LongTxt = dto.LongTxt,
                        ChangedOn = dto.ChangedOn,
                        DeletionInd = dto.DeletionInd,
                        CreatedBy = dto.CreatedBy,
                        CreatedOn = dto.CreatedOn,
                        Division = dto.Division,
                        PrimaryLang = dto.PrimaryLang,
                        ServiceCat = dto.ServiceCat,
                        ShortTxt = dto.ShortTxt,
                        Unit = dto.Unit,
                        ValuationClass = dto.ValuationClass
                    });
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                pageNumber++;
            }
        }
    }
}