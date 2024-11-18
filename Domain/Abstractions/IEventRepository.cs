using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IEventRepository
    {
        public Event GetById(Guid id);
        public Event GetByName(string name);
        public Task CreateAsync(Event @event);
        public Task Update(Event @event);
        public Task Delete(Event @event);
        public Task SaveAsync();
    }
}
