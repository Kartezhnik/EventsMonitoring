using Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Abstractions;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        UserRepositoryUseCase userRepositoryUseCase;
        IAuthService authService;
      
        public AuthController(IAuthService _authService)
        {
            authService = _authService;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(User user)
        {
            AuthResult result = authService.Register(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginModel model)
        {
            AuthResult result = authService.Login(model);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpPost("refresh")]
        public IActionResult RefreshToken(RefreshTokenRequest request)
        {
            AuthResult result = authService.RefreshToken(request.Name, request.RefreshToken);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
