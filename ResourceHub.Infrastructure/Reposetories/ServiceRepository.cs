using Microsoft.EntityFrameworkCore;
using ResourceHub.Application.Dtos;
using ResourceHub.Application.Interfaces;
using ResourceHub.Domain.Entities;
using ResourceHub.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
namespace ResourceHub.Infrastructure.Reposetories
{
    //public class ServiceRepository : IServiceInterface
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly ResourceHubDbContext _resourceHubDbContext;

    //    public ServiceRepository(IUnitOfWork unitOfWork , ResourceHubDbContext resourceHubDbContext)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _resourceHubDbContext = resourceHubDbContext;
    //    }
    //    public async Task<ICollection<ServiceDto>> GetAll(CancellationToken cancellationToken = default)
    //    {
    //        IQueryable<Service> query = _resourceHubDbContext.Services;

    //        var services= await query.Select(s => new ServiceDto()
    //        {
    //            ActivityNo= s.ActivityNo,
    //            ChangedBy= s.ChangedBy,
    //            ServiceCat= s.ServiceCat,
    //            ShortTxt= s.ShortTxt,
    //            ChangedOn= s.ChangedOn,
    //            CreatedBy= s.CreatedBy,
    //            CreatedOn= s.CreatedOn,
    //            DeletionInd= s.DeletionInd,
    //            Division= s.Division,
    //            LongTxt= s.LongTxt,
    //            MaterialGroup= s.MaterialGroup,
    //            PrimaryLang= s.PrimaryLang,
    //            Unit= s.Unit,
    //            ValuationClass= s.ValuationClass,
    //        }).ToListAsync() ;

    //        return services ;
    //    }
    //}
}
