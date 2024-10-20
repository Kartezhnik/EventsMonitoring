﻿using EventsMonitoring.Models.Entities;

namespace EventsMonitoring.Models.Repositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(Guid id, Context db);
        T GetByName(string name, Context db);
        Task CreateAsync(T entity, Context db);
        Task UpdateAsync(T entity, Context db);
        Task DeleteAsync(T entity, Context db);
        Task SaveAsync(T entity, Context db);
    }
}
