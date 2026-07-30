using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.Dtos
{
    public class ServicePageResult
    {
        public ICollection<ServiceDto> Services { get; set; } = new List<ServiceDto>();

        public bool HasMore { get; set; }
    }
}
