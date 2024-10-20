using EventsMonitoring.Models.Entities;
using EventsMonitoring.Models.Services;
using Microsoft.AspNetCore.Identity;

namespace EventsMonitoring.Models.UseCases
{
    public class UserAuthorizationUseCase
    {
        public string UserAuthorization(string name, string password, Context db)
        {
            IUserService user = new UserService();

            return user.Authorizate(name, password, db);
        }
    }
}
