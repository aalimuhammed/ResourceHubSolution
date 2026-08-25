namespace ResourceHub.Application.Dtos
{
    public class SearchFilter
    {
        public int PageSize { get;} = 10;
        public int? lastCursorId { get; set; }
        public string? ActivityNo { get; set; }
        public string? MaterialGroup { get; set; }
        public string? ServiceCat { get; set; }
        public string? Description { get; set; }
        public bool? DeletionInd { get; set; }
    }
}