using EventsWebApplication.Core.Models;
using System.Security.Claims;

namespace EventsWebApplication.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
        string GenerateRefreshToken();
    }
}
