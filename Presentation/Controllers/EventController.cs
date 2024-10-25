using EventsMonitoring.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Repositories;
using Application.UseCases;
using EventsMonitoring.Models.Services;
using Microsoft.AspNetCore.Http;

namespace EventsMonitoring.Controllers
{
    public class EventController : Controller
    {
        IRepository<Event> repo;
        CreateEventUseCase createEventUseCase;

        [Authorize]
        [HttpGet("event/{eventId}")]
        public IActionResult GetEventById(Guid id, Context db)
        {
            var @event = repo.GetById(id, db);

            return Ok(@event);
        }

        [Authorize]
        [HttpGet("event/{eventName}")]
        public IActionResult GetUserByName(string name, Context db)
        {
            var @event = repo.GetByName(name, db);

            return Ok(@event);
        }

        [Authorize]
        [HttpPost("event/create")]
        public async Task<IActionResult> CreateEvent(Event request, Context db)
        {
            await repo.CreateAsync(createEventUseCase.CreateEvent(request), db);

            return Ok();
        }

        [Authorize]
        [HttpPost("event/uploadImage")]
        public async Task<IActionResult> UploadImage(Event request)
        {
            await UploadImageUseCase.UploadImageAsync(request);

            return Ok();
        }
        [Authorize]
        [HttpPost("event/update")]
        public async Task<IActionResult> UpdateEvent(Event request, Context db)
        {
            await repo.UpdateAsync(request, db);

            return Ok();
        }
        [Authorize]
        [HttpDelete("event/delete")]
        public async Task<IActionResult> DeleteEvent(Event request, Context db)
        {
            await repo.DeleteAsync(request, db);

            return Ok();
        }
    }
}
