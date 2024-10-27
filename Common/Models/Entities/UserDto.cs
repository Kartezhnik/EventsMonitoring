using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Models.Entities
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }    

        public int? EventInfoKey { get; set; }
        [ForeignKey("EventInfoKey")]
        public Event? Event { get; set; }
    }
}
