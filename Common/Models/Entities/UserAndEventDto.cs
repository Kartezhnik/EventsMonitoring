using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsMonitoring.Models.Entities
{
    public class UserAndEventDto
    {
        public User User { get; set; }
        public Event Event { get; set; }
    }
}
