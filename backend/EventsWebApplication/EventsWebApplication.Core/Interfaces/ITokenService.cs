using EventsWebApplication.Core.Models;

namespace EventsWebApplication.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
        string GenerateRefreshToken();
    }
}
