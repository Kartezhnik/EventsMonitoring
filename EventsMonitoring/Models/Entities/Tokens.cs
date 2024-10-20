using EventsMonitoring.Models.Services;

namespace EventsMonitoring.Models.Entities
{
    public class Tokens
    {
        public string Token { get; set; } = TokenService.GenerateRefrashToken();

        public Guid UserId { get; set; }
        public User? User { get; set; } 
    }

}
