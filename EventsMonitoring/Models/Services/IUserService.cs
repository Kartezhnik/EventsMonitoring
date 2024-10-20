using EventsMonitoring.Models.Entities;

namespace EventsMonitoring.Models.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(User request, Context db);
        string Authorizate(string name, string password, Context db);
    }
}
