using Common.Models.Entities;
using Common;

namespace Infrastructure.Repositories
{
    public class EventRepository : IRepository<Event>
    {
        public Event GetById(Guid id, Context db)
        {
            return db.Events.First(x => x.Id == id);
        }
        public Event GetByName(string name, Context db)
        {
            return db.Events.First(x => x.Name == name);
        }
        public async Task CreateAsync(Event entity, Context db)
        {
            await db.Events.AddAsync(entity);
            await db.SaveChangesAsync();
        }
        public async Task UpdateAsync(Event entity, Context db)
        {
            db.Events.Update(entity);
            await db.SaveChangesAsync();
        }
        public Task DeleteAsync(Event entity, Context db)
        {
            db.Remove(entity);

            return Task.CompletedTask;
        }
        public async Task SaveAsync(Event entity, Context db)
        {
            await db.SaveChangesAsync();
        }
    }
}
