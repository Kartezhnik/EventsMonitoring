using EventsMonitoring.Models.Entities;
using EventsMonitoring.Models.Services;
using Microsoft.IdentityModel.SecurityTokenService;

namespace EventsMonitoring.Models.UseCases
{
    public class UserRegistrationUseCase
    {
        public User Register(User request)
        {
            if (request == null) throw new BadRequestException(nameof(request));

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
