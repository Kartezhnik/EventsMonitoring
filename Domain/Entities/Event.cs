#nullable enable
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public DateTime DateOfEvent { get; set; }
        public string PlaceOfEvent { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public List<User> Users { get; set; } = new();
    }
}
