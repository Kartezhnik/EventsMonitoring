using Common;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Abstractions;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        Context db;
        public UserRepository(Context _db) 
        {
             db = _db;
        }
        public User GetById(Guid id)
        {
            return db.Users.FirstOrDefault(x => x.Id == id);
        }
        public User GetByName(string name)
        {
            return db.Users.FirstOrDefault(x => x.Name == name);
        }
        public async Task CreateAsync(User user)
        {
            await db.Users.AddAsync(user);
        }
        public Task Update(User user)
        {
            db.Users.Update(user);
            return Task.CompletedTask;
        }
        public Task Delete(User user)
        {
            db.Remove(user);
            return Task.CompletedTask;
        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public async Task AddUserInEventAsync(Event @event, User user)
        {
            user.EventInfoKey = @event.Id;
            await db.Users.AddAsync(user);
        }

        public Task DeleteUserFromEventAsync(Event @event, User user)
        {
            user.EventInfoKey = @event.Id;
            db.Users.Remove(user);

            return Task.CompletedTask;
        }
        public async Task<List<User>> GetUsersByEventAsync(Event @event)
        {
            List<User> users = await db.Users.Where(user => user.EventInfoKey == @event.Id).ToListAsync();

            return users;
        }
        /*
        public async Task AddTokenAsync(User user, string token)
        {
            token.UserId = user.Id;
            await db.Tokens.AddAsync(token);
        }
        public async Task<Tokens> GetTokenAsync(User user)
        {
            Tokens token = new Tokens();
            token = await db.Tokens.FindAsync(user.Id);

            return token;
        }
        */
    }
}
