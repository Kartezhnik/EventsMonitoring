using Microsoft.AspNetCore.Mvc;
using EventsMonitoring.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Infrastructure.Repositories;
using EventsMonitoring;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        IMapper mapper;
        UserRepository repo = new UserRepository();

        [Authorize]
        [HttpGet("user/{userId}")]
        public IActionResult GetUserById(Guid id, [FromServices] Context db)
        {
            var user = mapper.Map<UserDto>(repo.GetById(id, db));
 
            return Ok(user);
        }

        [Authorize]
        [HttpGet("user/{userName}")]
        public IActionResult GetUserByName(string name, [FromServices] Context db)
        {
            var user = mapper.Map<UserDto>(repo.GetByName(name, db));

            return Ok(user);
        }

        [Authorize]
        [HttpPost("event/{eventId}/addUser/{userId}")]
        public async Task<IActionResult> AddUserInEvent(UserAndEventDto dto, [FromServices] Context db)
        {
            User user = dto.User;
            Event @event = dto.Event;

            await repo.AddUserInEventAsync(@event, user, db);

            return Ok(user);
        }

        [Authorize]
        [HttpGet("event/{eventId}/users")]
        public async Task<IActionResult> GetUsersByEvent(Event @event, [FromServices] Context db)
        {
            List<User> users = await repo.GetUsersByEventAsync(@event, db);

            return Ok(users);
        }

        [Authorize]
        [HttpDelete("event/{eventId}/deleteUser/{userId}")]
        public async Task<IActionResult> DeleteUserInEvent(UserAndEventDto dto, [FromServices] Context db)
        {
            User user = dto.User;
            Event @event = dto.Event;

            await repo.DeleteUserFromEventAsync(@event, user, db);

            return Ok(user);
        }

        [Authorize]
        [HttpDelete("user/delete")]
        public async Task<IActionResult> DeleteUser(User user, [FromServices] Context db)
        {
            await repo.DeleteAsync(user, db);

            return Ok(user);
        }

        private IActionResult Ok(User user)
        {
            throw new NotImplementedException();
        }
    }
}
