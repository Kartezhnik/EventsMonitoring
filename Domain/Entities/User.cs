#nullable enable
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthdayDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        
        public Guid? EventInfoKey { get; set; }
        [ForeignKey("EventInfoKey")]
        public Event? Event { get; set; }
    }
}
