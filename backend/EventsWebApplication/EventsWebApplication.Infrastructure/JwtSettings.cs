namespace EventsWebApplication.Infrastructure
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int AccessTokenLifetime { get; set; } // В минутах
        public int RefreshTokenLifetime { get; set; } // В днях
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
