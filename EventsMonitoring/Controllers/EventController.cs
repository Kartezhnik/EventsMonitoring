using AutoMapper;
using EventsMonitoring.Models.Entities;
using EventsMonitoring.Models.Repositories;
using EventsMonitoring.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace EventsMonitoring.Controllers
{
    public class EventController : Controller
    {
        IRepository<Event> repo = new EventRepository();
        IEventService eventService;

        [Authorize]
        [HttpGet("event/{eventId}")]
        public IActionResult GetEventById(Guid id, Context db)
        {
            var @event = repo.GetById(id, db);
            if (@event == null)
            {
                return NotFound();
            }
            return Ok(@event);
        }

        [Authorize]
        [HttpGet("event/{eventName}")]
        public IActionResult GetUserByName(string name, Context db)
        {
            var @event = repo.GetByName(name, db);
            if (@event == null)
            {
                return NotFound();
            }
            return Ok(@event);
        }

        [Authorize]
        [HttpPost("event/create")]
        public async Task<IActionResult> CreateEvent(Event request, Context db)
        {
            await repo.CreateAsync(request, db);
            if (request == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [Authorize]
        [HttpPost("event/uploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile, Guid eventId, Context db)
        {
            await eventService.UploadImageAsync(imageFile, eventId, db);
            if (!string.IsNullOrEmpty(imageFile.FileName))
            {
                return NotFound();
            }
            return Ok();
        }
        [Authorize]
        [HttpPost("event/update")]
        public async Task<IActionResult> UpdateEvent(Event entity, Context db)
        {
            await repo.UpdateAsync(entity, db);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok();
        }
        [Authorize]
        [HttpDelete("event/delete")]
        public async Task<IActionResult> DeleteEvent(Event entity, Context db)
        {
            await repo.DeleteAsync(entity, db);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
