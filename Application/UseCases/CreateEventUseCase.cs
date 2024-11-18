using Application.Validators;
using Domain.Entities;

namespace Application.UseCases
{
    public class CreateEventUseCase
    {
        EventValidator validator;
        Event @event = new Event();
        public CreateEventUseCase(EventValidator _validator) 
        {
            validator = _validator;
        }
        public Event CreateEvent(Event request)
        {
            try
            {
                validator.ValidateEvent(request);

                @event.Id = Guid.NewGuid();
                @event.Name = request.Name;
                @event.Description = request.Description;
                @event.Type = request.Type;
                @event.PlaceOfEvent = request.PlaceOfEvent;
                @event.DateOfEvent = request.DateOfEvent;
                @event.ImageFile = request.ImageFile;

                return @event;
            }
            catch (ValidationException ex) 
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
