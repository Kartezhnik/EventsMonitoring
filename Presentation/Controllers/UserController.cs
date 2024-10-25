using Microsoft.AspNetCore.Mvc;
using EventsMonitoring.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Infrastructure.Repositories;

namespace EventsMonitoring.Controllers
{
    public class UserController : Controller
    {
        IMapper mapper;
        UserRepository repo = new UserRepository();

        [Authorize]
        [HttpGet("user/{userId}")]
        public IActionResult GetUserById(Guid id, Context db)
        {
            var user = mapper.Map<UserDto>(repo.GetById(id, db));
 
            return Ok(user);
        }

        [Authorize]
        [HttpGet("user/{userName}")]
        public IActionResult GetUserByName(string name, Context db)
        {
            var user = mapper.Map<UserDto>(repo.GetByName(name, db));

            return Ok(user);
        }

        [Authorize]
        [HttpPost("event/{eventId}/addUser/{userId}")]
        public async Task<IActionResult> AddUserInEvent(Guid id, User user, Context db)
        {
            await repo.AddUserInEventAsync(id, user, db);

            return Ok(user);
        }

        [Authorize]
        [HttpGet("event/{eventId}/users")]
        public async Task<IActionResult> GetUsersByEvent(Guid id, Context db)
        {
            List<User> users = await repo.GetUsersByEventAsync(id, db);

            return View(users);
        }

        [Authorize]
        [HttpDelete("event/{eventId}/deleteUser/{userId}")]
        public async Task<IActionResult> DeleteUserInEvent(Guid id, User user, Context db)
        {
            await repo.DeleteUserFromEventAsync(id, user, db);

            return Ok(user);
        }

        [Authorize]
        [HttpDelete("user/delete")]
        public async Task<IActionResult> DeleteUser(User user, Context db)
        {
            await repo.DeleteAsync(user, db);

            return Ok(user);
        }
    }
}
