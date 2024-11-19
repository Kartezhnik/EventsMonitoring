#nullable enable
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EventDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public DateTime DateOfEvent { get; set; }
        public string PlaceOfEvent { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
