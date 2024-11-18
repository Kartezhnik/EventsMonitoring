using Application.UseCases;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        UserRegistrationUseCase userRegistrationUseCase;
        ITokenService tokenService;
        IUserRepository repo;
        IHttpContextAccessor httpContextAccessor;

        public AuthService(UserRegistrationUseCase userRegistrationUseCase, 
            ITokenService tokenService, 
            IUserRepository repo, 
            IHttpContextAccessor httpContextAccessor)
        {
            this.userRegistrationUseCase = userRegistrationUseCase;
            this.tokenService = tokenService;
            this.repo = repo;
            this.httpContextAccessor = httpContextAccessor;
        }

        public AuthResult Register(User user)
        {
            var createdUser = userRegistrationUseCase.Register(user);

            if (createdUser == null)
            {
                return new AuthResult { Success = false };
            }

            var accessToken = tokenService.GenerateAccessToken(user.Name);
            var refreshToken = tokenService.GenerateRefreshToken(user.Name);

            SetTokensInCookies(accessToken, refreshToken);

            return new AuthResult
            {
                Success = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        public AuthResult Login(UserLoginModel model)
        {
            User user = repo.GetByName(model.Name);

            if (user == null)  
            {
                return new AuthResult { Success = false };  
            }

            if (user.Password == model.Password)
            {
                var accessToken = tokenService.GenerateAccessToken(model.Name);
                var refreshToken = tokenService.GenerateRefreshToken(model.Name);

                SetTokensInCookies(accessToken, refreshToken);

                return new AuthResult
                {
                    Success = true,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
            }
            else { return new AuthResult { Success = false };}
        }
        public AuthResult RefreshToken(string name, string refreshToken)
        {
            if (!tokenService.IsRefreshTokenValid(name, refreshToken))
            {
                return new AuthResult { Success = false };
            }

            var newAccessToken = tokenService.GenerateAccessToken(name);

            SetTokensInCookies(newAccessToken, refreshToken);

            return new AuthResult
            {
                Success = true,
                AccessToken = newAccessToken,
            };
        }
        private void SetTokensInCookies(string accessToken, string refreshToken)
        {
            var accessTokenOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(10)
            };
            var refreshTokenOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(10)
            };

            httpContextAccessor.HttpContext.Response.Cookies.Append("AccessToken", accessToken, accessTokenOptions);
            httpContextAccessor.HttpContext.Response.Cookies.Append("RefreshToken", refreshToken, refreshTokenOptions);
        }
    }
}
