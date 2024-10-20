using EventsMonitoring.Models.Entities;
using EventsMonitoring.Models.Services;

namespace EventsMonitoring.Models.UseCases
{
    public class UserRegistrationUseCase
    {
        public Task<User> UserRegistration(User request, Context db)
        {
            IUserService user = new UserService();

            return user.RegisterAsync(request, db);
        }
    }
}
