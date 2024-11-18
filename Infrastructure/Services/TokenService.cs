using Domain;
using Domain.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        IUserRepository repo;
        private readonly ConcurrentDictionary<string, (string refreshToken, DateTime expiration)> storedRefreshTokens = new();
        public string GenerateAccessToken(string name)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, name) };

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        public string GenerateRefreshToken(string name)
        {
            var refreshToken = Guid.NewGuid().ToString();
            var expirationDate = DateTime.UtcNow.Add(TimeSpan.FromHours(10));

            storedRefreshTokens[name] = (refreshToken, expirationDate);

            return refreshToken;
        }
        public ClaimsPrincipal TokenValidate(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidAudience = AuthOptions.AUDIENCE,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool IsRefreshTokenValid(string name, string refreshToken)
        {
            if (storedRefreshTokens.TryGetValue(name, out var storedToken))
            {
                if (storedToken.refreshToken == refreshToken && storedToken.expiration > DateTime.UtcNow)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
