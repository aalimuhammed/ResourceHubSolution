namespace ResourceHub.Application.Dtos
{
    public class JwtSettings
    {
        public string issuer { get; set; } = null!;
        public string audience { get; set; } = null!;
        public string expirationInMinutes { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
    }
}
