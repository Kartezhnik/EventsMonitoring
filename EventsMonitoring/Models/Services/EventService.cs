using EventsMonitoring.Models.Entities;
using EventsMonitoring.Models.Repositories;

namespace EventsMonitoring.Models.Services
{
    public class EventService
    {
        EventRepository repo = new EventRepository();
        public async Task<Event> CreateEvent(Event request, Context db)
        {
            Event @event = new Event();
            @event.Id = Guid.NewGuid();
            @event.Name = request.Name;
            @event.Description = request.Description;
            @event.Type = request.Type;
            @event.PlaceOfEvent = request.PlaceOfEvent;
            @event.DateOfEvent = request.DateOfEvent;
            @event.ImageUrl = request.ImageUrl;

            await repo.CreateAsync(request, db);

            return @event;
        }
        public async Task UploadImageAsync(IFormFile imageFile, Guid eventId, Context db)
        {
            var @event = repo.GetById(eventId, db); 

            if (imageFile == null || imageFile.Length == 0)
                throw new ArgumentException("Изображение не загружено");

            var uploadsDirectory = Path.Combine("uploads");

            var fileName = $"{Guid.NewGuid()}_{imageFile.FileName}";
            var filePath = Path.Combine(uploadsDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            @event.ImageUrl = $"/uploads/{fileName}";

            await repo.UpdateAsync(@event, db);
        }
    }
}
