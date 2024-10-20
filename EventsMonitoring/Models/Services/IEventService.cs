using EventsMonitoring.Models.Entities;

namespace EventsMonitoring.Models.Services
{
    public interface IEventService
    {
        Task<Event> CreateEvent(Event request, Context db);
        Task UploadImageAsync(IFormFile imageFile, Guid eventId, Context db);
    }
}
