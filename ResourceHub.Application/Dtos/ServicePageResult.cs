namespace ResourceHub.Application.Dtos
{
    public class ServicePageResult
    {
        public IEnumerable<ServiceDto> Services { get; set; } = new List<ServiceDto>();
        public bool HasMore { get; set; }
        public int TotalCount { get; set; }
    }
}