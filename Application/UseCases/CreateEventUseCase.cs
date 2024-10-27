using Common.Models.Entities;
using Microsoft.IdentityModel.SecurityTokenService;


namespace Application.UseCases
{
    public class CreateEventUseCase
    {
        public Event CreateEvent(Event request)
        {
            if (request == null) throw new BadRequestException(nameof(request));

            Event @event = new Event();
            @event.Id = Guid.NewGuid();
            @event.Name = request.Name;
            @event.Description = request.Description;
            @event.Type = request.Type;
            @event.PlaceOfEvent = request.PlaceOfEvent;
            @event.DateOfEvent = request.DateOfEvent;
            @event.ImageFile = request.ImageFile;

            return @event;

        }
    }
}
