namespace ResourceHub.Application.Dtos
{
    public class PaginatedServiceResultDto<T>
    {
        public IEnumerable<T> ? ServicesDto { get; set; } 
        public int ? Next { get; set; }
    }
}