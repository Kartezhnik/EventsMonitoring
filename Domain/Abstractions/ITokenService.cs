using System.Security.Claims;

namespace Domain.Abstractions
{
    public interface ITokenService
    {
        public string GenerateAccessToken(string name);
        public string GenerateRefreshToken(string name);
        public ClaimsPrincipal TokenValidate(string token);
        public bool IsRefreshTokenValid(string name, string refreshToken);
    }
}
