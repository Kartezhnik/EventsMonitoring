namespace Application.Services
{
    public interface ITokenService
    {
        public string GenerateJwtToken(string name);
    }
}
