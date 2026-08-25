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
        private readonly IGenericRepository<Service> _genericServicesReposetory;
        public ServiceRepository(
            ResourceHubDbContext context ,
            IGenericRepository<Service> genericServicesReposetory)
        {
            _context = context;
            _genericServicesReposetory = genericServicesReposetory;
        }
        public async Task<PaginatedResultDto<ServiceResponseDto>> GetPagintedServices(
            SearchFilter searchFilter,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Service> query = _context.Services;

            // current items count
            var pagesize = searchFilter.PageSize;

            query = ApplyFilter(query, searchFilter);

            #region CursorPaginationExplaination
            // pagesize = 100
            // items will take 101 items 
            // to know if there are next items the condintion (items.count>pagesize) will decide
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

            var result = new PaginatedResultDto<ServiceResponseDto>
            {
                Items = items.Take(pagesize),

                Next = items.Count > pagesize ? items.Last().CursorId :null,

                TotalCount = await query.CountAsync(cancellationToken)
            };

            return result;
        }
        public async Task InsertNewService(
            ServiceDto serviceDto, 
            CancellationToken cancellationToken = default)
        {
               var lastCursorId = await _genericServicesReposetory.FindMaxAsync(
                    s => s.CursorId, 
                    cancellationToken) ?? 0;

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
        public async Task<bool> IsActivityNoExists(string activityNumber , CancellationToken cancellationToken)
        {
            return await _genericServicesReposetory.FindByAnyAsync(
                s => s.ActivityNo == activityNumber ,
                cancellationToken);
        }
        private IQueryable<Service> ApplyFilter(
            IQueryable<Service> query,
            SearchFilter searchFilter)
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
            if (!string.IsNullOrWhiteSpace(searchFilter.Description))
            {
                query = query.Where(s => s.LongTxt.Contains(searchFilter.Description) ||
                s.ShortTxt.Contains(searchFilter.Description));
            }
            if(searchFilter.DeletionInd.HasValue)
            {
                query = query.Where(s => s.DeletionInd == searchFilter.DeletionInd);
            }
            return query;
        }
    }
}
