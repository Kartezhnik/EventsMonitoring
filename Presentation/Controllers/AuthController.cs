using EventsMonitoring.Models.Entities;
using EventsMonitoring.Models.Services;
using EventsMonitoring.Models.UseCases;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventsMonitoring.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly UserRegistrationUseCase userRegistrationUseCase;
        private readonly UserAuthorizationUseCase userAuthorizationUseCase;
        private IRepository<User> repo;  

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User request, Context db)
        {
            await repo.CreateAsync(userRegistrationUseCase.Register(request), db);

            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User request, string name, string password, Context db)
        {
            string token = userAuthorizationUseCase.Authorizate(request, name, password);

            return Ok(new { token });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] Guid id, Context db)
        {
            ITokenService tokenService = new TokenService();

            string name = repo.GetById(id, db).Name;
            string token = tokenService.GenerateJwtToken(name);
            return Ok(new { token });
        }
    }
}
