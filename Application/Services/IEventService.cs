using EventsMonitoring.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace EventsMonitoring.Models.Services
{
    public interface IEventService
    {
        Event CreateEvent(Event request);
        Task UploadImageAsync(IFormFile imageFile);
    }
}
