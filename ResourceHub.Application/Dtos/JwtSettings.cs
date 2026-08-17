namespace ResourceHub.Application.Dtos
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string ExpirationInMinutes { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
    }
}
