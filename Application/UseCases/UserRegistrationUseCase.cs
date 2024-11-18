using Application.Validators;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.IdentityModel.SecurityTokenService;

namespace Application.UseCases
{
    public class UserRegistrationUseCase
    {
        IUserRepository repo;
        UserValidator userValidator;
        public UserRegistrationUseCase(IUserRepository _repo, UserValidator _userValidator)
        {
            repo = _repo;
            userValidator = _userValidator;
        }
        public async Task<User> Register(User request)
        {
            try
            {
                userValidator.ValidateUser(request);

                User user = new User();
                user.Id = new Guid();
                user.Name = request.Name;
                user.Email = request.Email;
                user.Password = request.Password;
                user.RegistrationDate = DateTime.Now;
                user.BirthdayDate = request.BirthdayDate;

                await repo.CreateAsync(user);
                await repo.SaveAsync();

                return user;
            }
            catch(ValidationException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
