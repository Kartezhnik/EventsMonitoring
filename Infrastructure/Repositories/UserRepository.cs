using EventsMonitoring.Models.Entities;
using EventsMonitoring;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        public User GetById(Guid id, Context db)
        {
            return db.Users.First(x => x.Id == id);
        }
        public User GetByName(string name, Context db)
        {
            return db.Users.First(x => x.Name == name);
        }
        public async Task CreateAsync(User entity, Context db)
        {
            await db.Users.AddAsync(entity);
            await db.SaveChangesAsync();
        }
        public async Task UpdateAsync(User entity, Context db)
        {
            db.Users.Update(entity);
            await db.SaveChangesAsync();
        }
        public async Task DeleteAsync(User entity, Context db)
        {
            db.Remove(entity);
            await db.SaveChangesAsync();
        }
        public async Task SaveAsync(User entity, Context db)
        {
            await db.SaveChangesAsync();
        }

        public async Task AddUserInEventAsync(Event @event, User entity, Context db)
        {
            entity.EventInfoKey = @event.Id;
            await db.Users.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteUserFromEventAsync(Event @event, User entity, Context db)
        {
            entity.EventInfoKey = @event.Id;
            db.Remove(entity);
            await db.SaveChangesAsync();
        }
        public async Task<List<User>> GetUsersByEventAsync(Event @event, Context db)
        {
            List<User> users = await db.Users.Where(user => user.EventInfoKey == @event.Id).ToListAsync();

            return users;
        }
        public async Task AddTokenAsync(User user, Tokens token, Context db)
        {
            token.UserId = user.Id;
            await db.Tokens.AddAsync(token);
            await db.SaveChangesAsync();
        }
        public async Task<Tokens> GetTokenAsync(User user, Context db)
        {
            Tokens? token = new Tokens();
            token = await db.Tokens.FindAsync(user.Id);

            return token;
        }
    }
}
