using ResourceHub.Domain.Base;

namespace ResourceHub.Domain.Entities
{
    public class Service : BaseEntity
    {
        public string ActivityNo { get; set; } = null!;
        public string CreatedOn { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public string ChangedOn { get; set; } = null!;
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