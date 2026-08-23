namespace ResourceHub.Application.Dtos
{
    public class SearchFilter
    {
        public  int PageSize { get;} = 10;
        public int ? lastCursorId { get; set; }
        public string ? ActivityNo { get; set; }
        public string? MaterialGroup { get; set; }
        public string? ServiceCat { get; set; }
        public string? ShortTxt { get; set; }
        public string? LongTxt { get; set; }
        public DateOnly? CreatedOn { get; set; }
    }
}
