using EventsMonitoring.Models.Entities;
using EventsMonitoring.Models.Repositories;
using EventsMonitoring.Models.Services;
using EventsMonitoring.Models.UseCases;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Xml.Linq;

namespace EventsMonitoring.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly UserRegistrationUseCase userRegistrationUseCase;
        private readonly UserService userService;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User request, Context db)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await userRegistrationUseCase.UserRegistration(request, db);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] string name, string password, Context db)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string token = userService.Authorizate(name, password, db);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefrashToken([FromBody] Guid id, Context db)
        {
            UserRepository repo = new UserRepository();
            ITokenService tokenService = new TokenService();

            if(await repo.GetTokenAsync(id, db) != null)
            {
                string name = repo.GetById(id, db).Name;
                string token = tokenService.GenerateJwtToken(name);
                return Ok(new { token });
            }
            else
            {
                return Unauthorized(); ;
            }
        }
    }
}
