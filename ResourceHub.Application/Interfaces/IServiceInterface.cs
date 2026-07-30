using ResourceHub.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.Interfaces
{
    public interface IServiceInterface
    {
        public Task<ICollection<ServiceDto>>GetAll();
    }
}
