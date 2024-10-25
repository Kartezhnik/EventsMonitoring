namespace EventsMonitoring.Models.Services
{
    public interface ITokenService
    {
        public string GenerateJwtToken(string name);
    }
}
