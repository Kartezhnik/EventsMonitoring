using Application.Validators;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.UseCases
{
    public class UserRepositoryUseCase
    {
        IUserRepository repo;
        UserValidator userValidator;
        public UserRepositoryUseCase(IUserRepository _repo, UserValidator _userValidator)
        {
            repo = _repo;
            userValidator = _userValidator;
        }   
        public User GetById(Guid id)
        {
            return repo.GetById(id);
        }
        public User GetByName(string name)
        {
            return repo.GetByName(name);
        }
        public Task Update(User user)
        {
            userValidator.ValidateUser(user);
            repo.Update(user);
            repo.SaveAsync();
            return Task.CompletedTask;
        }
        public Task Delete(User user)
        {
            repo.Delete(user);
            repo.SaveAsync();
            return Task.CompletedTask;
        }
        public async Task SaveAsync()
        {
            await repo.SaveAsync();
        }
        public async Task AddUserInEventAsync(Event @event, User user)
        {
            userValidator.ValidateUser(user);

            await repo.AddUserInEventAsync(@event, user);
            await repo.SaveAsync();
        }
        public async Task DeleteUserFromEventAsync(Event @event, User user)
        {
            userValidator.ValidateUser(user);
            await repo.DeleteUserFromEventAsync(@event, user);
            await repo.SaveAsync();
        }
        public async Task<List<User>> GetUsersByEventAsync(Event @event)
        {
            return await repo.GetUsersByEventAsync(@event);
        }
        /*
        public async Task AddTokenAsync(User user, Tokens token)
        {
            userValidator.ValidateUser(user);
            await repo.AddTokenAsync(user, token);
        }
        public async Task<Tokens> GetTokenAsync(User user)
        {
            userValidator.ValidateUser(user);
            return await repo.GetTokenAsync(user);
        }
        */
    }
}
