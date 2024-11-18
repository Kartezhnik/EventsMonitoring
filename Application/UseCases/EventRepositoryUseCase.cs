using Domain.Entities;
using Domain.Abstractions;

namespace Application.UseCases
{
    public class EventRepositoryUseCase
    {
        IEventRepository repo;
        public EventRepositoryUseCase(IEventRepository _repo)
        {
            repo = _repo;
        }
        public Event GetById(Guid id)
        {
            return repo.GetById(id);
        }
        public Event GetByName(string name)
        {
            return repo.GetByName(name);
        }
        public async Task CreateAsync(Event @event)
        {
            await repo.CreateAsync(@event);
        }
        public Task Update(Event @event)
        {
            return repo.Update(@event);
        }
        public Task Delete(Event @event)
        {
            return repo.Delete(@event);
        }
        public async Task SaveAsync()
        {
            await repo.SaveAsync();
        }
    }
}
