namespace ResourceHub.Application.Dtos
{
    public class PaginatedResultDto<T>
    {
        public IEnumerable<T> ? Items { get; set; }
        public bool HasMore { get; set; }
        public int? Next { get; set; }
        public int TotalCount { get; set; }
    }
}