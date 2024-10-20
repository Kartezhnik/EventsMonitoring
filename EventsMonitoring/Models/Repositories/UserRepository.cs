using EventsMonitoring.Models.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using NuGet.Protocol;
using System.Data.Entity;
using EventsMonitoring.Models.Services;

namespace EventsMonitoring.Models.Repositories
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
            if (entity != null)
            {
                db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            await db.SaveChangesAsync();
        }
        public async Task DeleteAsync(User entity, Context db)
        {
            if(entity != null)
            {
                db.Remove(entity);
            }
            await db.SaveChangesAsync();
        }
        public async Task SaveAsync(User entity, Context db)
        {
            await db.SaveChangesAsync();
        }

        public async Task AddUserInEventAsync(Guid id, User entity, Context db)
        {
            entity.EventInfoKey = id;
            await db.Users.AddAsync(entity);
            await db.SaveChangesAsync();
        }

        public async Task DeleteUserFromEventAsync(Guid id, User entity, Context db)
        {
            entity.EventInfoKey = id;
            db.Remove(entity);
            await db.SaveChangesAsync();
        }
        public async Task<List<User>> GetUsersByEventAsync(Guid id, Context db)
        {
            await db.Events.FindAsync(id);
            List<User> users = await db.Users.Where(user => user.EventInfoKey == id).ToListAsync();

            return users;
        }
        public async Task AddTokenAsync(Guid id, Tokens token, Context db)
        {
            token.UserId = id;
            await db.Tokens.AddAsync(token);
            await db.SaveChangesAsync();
        }
        public async Task<Tokens> GetTokenAsync(Guid id, Context db)
        {
            Tokens? token = new Tokens();
            User user = GetById(id, db); 
            token = await db.Tokens.FindAsync(user.Id);
            
            if (token != null)
            {
                return token;
            }
            else
            {
                throw new UnauthorizedAccessException();
            }           
        }
    }
}
