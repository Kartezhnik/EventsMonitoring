using EventsMonitoring.Models.Entities;
using Microsoft.Extensions.Logging;

namespace EventsMonitoring.Models.Repositories
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
            var existingEvent = await db.Events.FindAsync(entity.Id);
            if (existingEvent == null)
            {
                throw new KeyNotFoundException($"Событие с ID {entity.Id} не найдено");
            }

            existingEvent.Name = entity.Name;
            existingEvent.Description = entity.Description;
            existingEvent.Type = entity.Type;
            existingEvent.PlaceOfEvent = entity.PlaceOfEvent;
            existingEvent.DateOfEvent = entity.DateOfEvent;
            existingEvent.ImageUrl = entity.ImageUrl;

            await db.SaveChangesAsync();
        }
        public async Task DeleteAsync(Event entity, Context db)
        {
            if (entity != null)
            {
                db.Remove(entity);
            }
            await db.SaveChangesAsync();
        }
        public async Task SaveAsync(Event entity, Context db)
        {
            await db.SaveChangesAsync();
        }
    }
}
