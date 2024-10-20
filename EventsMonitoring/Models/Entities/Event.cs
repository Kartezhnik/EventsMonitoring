namespace EventsMonitoring.Models.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } 
        public string Type { get; set; }
        public DateTime DateOfEvent { get; set; }
        public string PlaceOfEvent { get; set; }
        public string ImageUrl { get; set; }

        public List<User> Users { get; set; } = new();
    }
}
