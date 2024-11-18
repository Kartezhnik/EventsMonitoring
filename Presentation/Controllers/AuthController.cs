using AutoMapper;
using Common;
using Application.Services;
using Application.UseCases;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Common.Entities;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserRegistrationUseCase userRegistrationUseCase;
        private readonly UserAuthorizationUseCase userAuthorizationUseCase;
        private IRepository<User> repo;
        IMapper mapper;
        ITokenService tokenService;

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto, [FromServices] Context db)
        {
            var user = mapper.Map<User>(userDto);
            await repo.CreateAsync(user, db);

            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto request, [FromServices] Context db)
        {
            var token = userAuthorizationUseCase.Authorizate(request, db);

            return Ok(new { token });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] Guid id, [FromServices]  Context db)
        {
            var user = repo.GetById(id, db);
            string token = tokenService.GenerateJwtToken(user.Name);

            return Ok(new { token });
        }
    }
}
