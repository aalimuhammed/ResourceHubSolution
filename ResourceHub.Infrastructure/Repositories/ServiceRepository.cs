using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;
using ResourceHub.Domain.Entities;
using ResourceHub.Infrastructure.Contexts;
using ResourceHub.Infrastructure.UOW;

namespace ResourceHub.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ResourceHubDbContext _context;

        public ServiceRepository(ResourceHubDbContext context )
        {
            _context = context;
        }
        public async Task<PaginatedServiceResultDto<ServiceDto>> GetPagintedServices(
            SearchFilterType searchFilter,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Service> query = _context.Services;

            var pagesize = searchFilter.PageSize;

            query = ApplyFilter(query, searchFilter);

            // pagesize = 3 
            // items will take 4 items 
            //to know if there are next items the condintion (items.count>pagesize) will decide
            // we have to retuen the same size of pagesize so when we return we take pagesize not pagesize+1

            query = query
                .OrderBy(s => s.CursorId)
                .Take(pagesize + 1); //21 item

            var items = await query.Select(s => new ServiceDto
            {
                DeletionInd = s.DeletionInd,
                Unit = s.Unit,
                ChangedOn = s.ChangedOn,
                PrimaryLang = s.PrimaryLang,
                ActivityNo = s.ActivityNo,
                ChangedBy = s.ChangedBy,
                CreatedBy = s.CreatedBy,
                CreatedOn = s.CreatedOn,
                Division = s.Division,
                LongTxt = s.LongTxt,
                MaterialGroup = s.MaterialGroup,
                ServiceCat = s.ServiceCat,
                ShortTxt = s.ShortTxt,
                ValuationClass = s.ValuationClass,
                CursorId = s.CursorId
               
            }).ToListAsync(cancellationToken); //21 

            var result = new PaginatedServiceResultDto<ServiceDto>
            {
                ServicesDto = items.Take(pagesize),

                Next = items.Count > pagesize ? items.Last().CursorId :null
            };

            return result;
        }
        public async Task InsertNewService(ServiceDto serviceDto, CancellationToken cancellationToken)
        {
            if (serviceDto is not null)
            {
                Service service = new Service
                {
                    DeletionInd = serviceDto.DeletionInd,
                    Unit = serviceDto.Unit,
                    ChangedOn = serviceDto.ChangedOn,
                    PrimaryLang = serviceDto.PrimaryLang,
                    ActivityNo = serviceDto.ActivityNo,
                    ChangedBy = serviceDto.ChangedBy,
                    CreatedBy = serviceDto.CreatedBy,
                    CreatedOn = serviceDto.CreatedOn,
                    Division = serviceDto.Division,
                    LongTxt = serviceDto.LongTxt,
                    MaterialGroup = serviceDto.MaterialGroup,
                    ServiceCat = serviceDto.ServiceCat,
                    ShortTxt = serviceDto.ShortTxt,
                    ValuationClass = serviceDto.ValuationClass
                };
                await _context.AddAsync(service);
            }
            else
            {
                throw new Exception("ServiceDto cannot be null.");
            }
        }
        private IQueryable<Service> ApplyFilter(
            IQueryable<Service> query,
            SearchFilterType searchFilter)
        {
            if (searchFilter.lastCursorId.HasValue)
            {
                query = query.Where(s => s.CursorId > searchFilter.lastCursorId);
            }

            if (!string.IsNullOrWhiteSpace(searchFilter.ActivityNo))
            {
                query = query.Where(s => s.ActivityNo == searchFilter.ActivityNo);
            }
            if (!string.IsNullOrWhiteSpace(searchFilter.MaterialGroup))
            {
                query = query.Where(s => s.MaterialGroup == searchFilter.MaterialGroup);
            }
            if (!string.IsNullOrWhiteSpace(searchFilter.ServiceCat))
            {
                query = query.Where(s => s.ServiceCat == searchFilter.ServiceCat);
            }
            if (!string.IsNullOrWhiteSpace(searchFilter.ShortTxt))
            {
                query = query.Where(s => s.ShortTxt.Contains(searchFilter.ShortTxt));
            }
            if (!string.IsNullOrWhiteSpace(searchFilter.LongTxt))
            {
                query = query.Where(s => s.LongTxt.Contains(searchFilter.LongTxt));
            }
            if (searchFilter.CreatedOn is not null)
            {
                query = query.Where(s => s.CreatedOn == searchFilter.CreatedOn);
            }
            return query;
        }
    }
}
