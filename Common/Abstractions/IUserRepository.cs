using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IUserRepository
    {
        public User GetById(Guid id);
        public User GetByName(string name);
        public Task CreateAsync(User user);
        public Task Update(User user);
        public Task Delete(User user);
        public Task SaveAsync();
        public Task AddUserInEventAsync(Event @event, User user);
        public Task DeleteUserFromEventAsync(Event @event, User user);
        public Task<List<User>> GetUsersByEventAsync(Event @event);
        /*
        public Task AddTokenAsync(User user, Tokens token);
        public Task<Tokens> GetTokenAsync(User user);
        */
    }
}
