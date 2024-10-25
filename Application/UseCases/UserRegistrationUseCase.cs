using EventsMonitoring.Models.Entities;
using EventsMonitoring.Models.Services;

namespace EventsMonitoring.Models.UseCases
{
    public class UserRegistrationUseCase
    {
        public User Register(User request)
        {
            User user = new User();
            user.Id = new Guid();
            user.Name = request.Name;
            user.Email = request.Email;
            user.Password = request.Password;
            user.RegistrationDate = DateTime.Now;
            user.BirthdayDate = request.BirthdayDate;

            return user;
        }
    }
}
