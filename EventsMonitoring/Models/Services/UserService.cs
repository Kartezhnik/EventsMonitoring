using EventsMonitoring.Models.Entities;
using EventsMonitoring.Models.Repositories;

namespace EventsMonitoring.Models.Services
{
    public class UserService : IUserService
    {
        IRepository<User> repo;
        ITokenService token;
        public async Task<User> RegisterAsync(User request, Context db) 
        {
            User user = new User();
            user.Id = new Guid();
            user.Name = request.Name;
            user.Email = request.Email;
            user.Password = request.Password;
            user.RegistrationDate = DateTime.Now;
            user.BirthdayDate = request.BirthdayDate;
            
            await repo.CreateAsync(user, db);
            
            return user;
        }  
        public string Authorizate(string name, string password, Context db)
        {     
            if(repo.GetByName(name, db) != null && password == repo.GetByName(name, db).Password)
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
