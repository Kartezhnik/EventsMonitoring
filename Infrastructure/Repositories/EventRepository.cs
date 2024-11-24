using Domain;
using Domain.Abstractions;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class EventRepository 
    {
        Context db;
        private EventRepository(Context _db) 
        {
            db = _db;
        }
        public Event GetById(Guid id)
        {
            return db.Events.First(x => x.Id == id);
        }
        public Event GetByName(string name)
        {
            return db.Events.First(x => x.Name == name);
        }
        public async Task CreateAsync(Event entity)
        {
            await db.Events.AddAsync(entity);
        }
        public Task Update(Event entity)
        {
            db.Events.Update(entity);
            return Task.CompletedTask;
        }
        public Task Delete(Event entity)
        {
            db.Events.Remove(entity);
            return Task.CompletedTask;
        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
