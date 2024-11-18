using Microsoft.AspNetCore.Mvc;
using Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Common;
using Domain.Entities;
using Domain.Entities;
using AutoMapper;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        Context db;
        EventRepositoryUseCase eventRepositoryUseCase;
        CreateEventUseCase createEventUseCase;
        IMapper mapper;
        
        public EventController(Context _db)
        {
            db = _db;
        }

        [Authorize]
        [HttpGet("event/{eventId}")]
        public IActionResult GetEventById(Guid id)
        {
            Event @event = eventRepositoryUseCase.GetById(id);
            EventDto eventDto = mapper.Map<EventDto>(@event);

            return Ok(eventDto);
        }

        [Authorize]
        [HttpGet("event/{eventName}")]
        public IActionResult GetUserByName(string name)
        {
            Event @event = eventRepositoryUseCase.GetByName(name);
            EventDto eventDto = mapper.Map<EventDto>(@event);

            return Ok(eventDto);
        }

        [Authorize]
        [HttpPost("event/create")]
        public async Task<IActionResult> CreateEvent(Event @event)
        {
            await eventRepositoryUseCase.CreateAsync(createEventUseCase.CreateEvent(@event));
            EventDto eventDto = mapper.Map<EventDto>(@event);

            return Ok(eventDto);
        }

        [Authorize]
        [HttpPost("event/uploadImage")]
        public async Task<IActionResult> UploadImage(Event @event)
        {
            await UploadImageUseCase.UploadImageAsync(@event);

            return Ok();
        }
        [Authorize]
        [HttpPost("event/update")]
        public async Task<IActionResult> UpdateEvent(Event @event)
        {
            await eventRepositoryUseCase.Update(@event);
            EventDto eventDto = mapper.Map<EventDto>(@event);

            return Ok(eventDto);
        }
        [Authorize]
        [HttpDelete("event/delete")]
        public async Task<IActionResult> DeleteEvent(Event @event)
        {
            await eventRepositoryUseCase.Delete(@event);

            return Ok();
        }
    }
}
