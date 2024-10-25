using EventsMonitoring.Models.Entities;
using EventsMonitoring.Models.Services;
using Microsoft.AspNetCore.Identity;

namespace EventsMonitoring.Models.UseCases
{
    public class UserAuthorizationUseCase
    {
        ITokenService token;

        public string Authorizate(User request, string name, string password)
        {
            if (name == request.Name && password == request.Password)
            {
                return token.GenerateJwtToken(name);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
