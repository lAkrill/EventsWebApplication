namespace EventsWebApplication.Infrastructure
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int RefreshTokenLifetime { get; set; }
    }
}
