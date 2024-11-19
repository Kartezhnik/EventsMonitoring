using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateOnly BirthdayDate { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
