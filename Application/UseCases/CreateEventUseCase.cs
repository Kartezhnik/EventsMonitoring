using EventsMonitoring.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CreateEventUseCase
    {
        public Event CreateEvent(Event request)
        {
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
