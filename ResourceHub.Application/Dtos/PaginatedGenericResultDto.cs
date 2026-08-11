using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.Dtos
{
    public class PaginatedGenericResultDto<T>
    {
        public IEnumerable<T>? ServicesDto { get; set; }
        public int? Next { get; set; }
    }
}
