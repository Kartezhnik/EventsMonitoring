using System.ComponentModel.DataAnnotations.Schema;

namespace EventsMonitoring.Models.Entities
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public int? EventInfoKey { get; set; }
        [ForeignKey("EventInfoKey")]
        public Event? Event { get; set; }
    }
}
