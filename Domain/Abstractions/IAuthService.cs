using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IAuthService
    {
        public AuthResult Register(User model);
        public AuthResult Login(UserLoginModel model);
        public AuthResult RefreshToken(string name, string refreshToken);
    }
}
