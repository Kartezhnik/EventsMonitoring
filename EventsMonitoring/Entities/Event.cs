namespace EventsMonitoring.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime DateOfEvent { get; set; }
        public string PlaceOfEvent { get; set; }
        public string ImageUrl { get; set; }
        public List<Participant> Participants { get; set; } = new();
    }
}
