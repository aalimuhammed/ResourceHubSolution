using System;
using System.Collections.Generic;
using System.Text;

namespace ResourceHub.Application.Dtos
{
    public class ServiceResponseDto
    {
        public int ? CursorId { get; set; }
        public string ActivityNo { get; set; } = null!;
        public DateOnly CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateOnly ChangedOn { get; set; }
        public string ChangedBy { get; set; } = null!;
        public string MaterialGroup { get; set; } = null!;
        public string ServiceCat { get; set; } = null!;
        public string Division { get; set; } = null!;
        public bool DeletionInd { get; set; }
        public string ValuationClass { get; set; } = null!;
        public string PrimaryLang { get; set; } = null!;
        public string ShortTxt { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public string LongTxt { get; set; } = null!;
    }
}
