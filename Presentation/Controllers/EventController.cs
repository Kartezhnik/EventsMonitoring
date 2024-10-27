using Common.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Repositories;
using Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Common;

namespace Presentation.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        IRepository<Event> repo;
        CreateEventUseCase createEventUseCase;

        [Authorize]
        [HttpGet("event/{eventId}")]
        public IActionResult GetEventById(Guid id, [FromServices] Context db)
        {
            var @event = repo.GetById(id, db);

            return Ok(@event);
        }

        [Authorize]
        [HttpGet("event/{eventName}")]
        public IActionResult GetUserByName(string name, [FromServices] Context db)
        {
            var @event = repo.GetByName(name, db);

            return Ok(@event);
        }

        [Authorize]
        [HttpPost("event/create")]
        public async Task<IActionResult> CreateEvent(Event request, [FromServices] Context db)
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
        public async Task<IActionResult> UpdateEvent(Event request, [FromServices] Context db)
        {
            await repo.UpdateAsync(request, db);

            return Ok();
        }
        [Authorize]
        [HttpDelete("event/delete")]
        public async Task<IActionResult> DeleteEvent(Event request, [FromServices] Context db)
        {
            await repo.DeleteAsync(request, db);

            return Ok();
        }
    }
}
