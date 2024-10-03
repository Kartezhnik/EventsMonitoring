using System.ComponentModel.DataAnnotations.Schema;

namespace EventsMonitoring.Entities
{
    public class Participant
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly BirthdayDate { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public string Email { get; set; } = string.Empty;

        public int? EventInfoKey { get; set; }
        [ForeignKey("EventInfoKey")]
        public Event? Event { get; set; }
    }
}
