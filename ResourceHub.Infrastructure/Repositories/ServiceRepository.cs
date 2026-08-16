using Microsoft.EntityFrameworkCore;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;
using ResourceHub.Domain.Entities;
using ResourceHub.Infrastructure.Contexts;

namespace ResourceHub.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository 
    {
        private readonly ResourceHubDbContext _context;

        public ServiceRepository(ResourceHubDbContext context)
        {
            _context = context;
        }
        public async Task<PaginatedServiceResultDto<ServiceResponseDto>> GetPagintedServices(
            SearchFilterType searchFilter,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Service> query = _context.Services;

            var pagesize = searchFilter.PageSize;

            query = ApplyFilter(query, searchFilter);

            #region CursorPaginationExplaination
            // pagesize = 3 
            // items will take 4 items 
            //to know if there are next items the condintion (items.count>pagesize) will decide
            // we have to retuen the same size of pagesize so when we return we take pagesize not pagesize+1
            #endregion

            query = query
                .OrderBy(s => s.CursorId)
                .Take(pagesize + 1); 

            var items = await query.Select(s => new ServiceResponseDto
            {
                CursorId = s.CursorId,
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
               
            }).ToListAsync(cancellationToken); 

            var result = new PaginatedServiceResultDto<ServiceResponseDto>
            {
                ServicesDto = items.Take(pagesize),

                Next = items.Count > pagesize ? items.Last().CursorId :null
            };

            return result;
        }
        
        public async Task InsertNewService(ServiceDto serviceDto, CancellationToken cancellationToken)
        {
            if (serviceDto is null)
            {
                throw new Exception("Fields cannot be null.");
            }
            try
            {
                int lastCursorId = 0;

                if (await _context.Services.AnyAsync(cancellationToken))
                {
                   lastCursorId = await _context.Services.MaxAsync(s => s.CursorId, cancellationToken);
                }

                Service service = new Service
                {
                    CursorId = lastCursorId + 1,
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
                await _context.Services.AddAsync(service);
            }
            catch (Exception ex) 
            { 
                throw new Exception($"Error inserting new service: {ex.Message}", ex);
            }
        }

        public async Task<bool> isActivityNoExists(string activityNumber)
        {
            return await _context.Services.AnyAsync(s=>s.ActivityNo == activityNumber);
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
