using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Domain.Entities;
using Application.UseCases;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        IMapper mapper;
        UserRepositoryUseCase userRepositoryUseCase;

        [Authorize]
        [HttpGet("user/{userId}")]
        public IActionResult GetUserById(Guid id)
        {
            User user = userRepositoryUseCase.GetById(id);
            UserDto userDto = mapper.Map<UserDto>(user);
 
            return Ok(userDto);
        }

        [Authorize]
        [HttpGet("user/{userName}")]
        public IActionResult GetUserByName(string name)
        {
            User user = userRepositoryUseCase.GetByName(name);
            UserDto userDto = mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [Authorize]
        [HttpPost("event/{eventId}/addUser/{userId}")]
        public async Task<IActionResult> AddUserInEvent(UserAndEventDto dto)
        {
            User user = dto.User;
            Event @event = dto.Event;

            await userRepositoryUseCase.AddUserInEventAsync(@event, user);
            UserDto userDto = mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [Authorize]
        [HttpGet("event/{eventId}/users")]
        public async Task<IActionResult> GetUsersByEvent(Event @event)
        {
            List<User> users = await userRepositoryUseCase.GetUsersByEventAsync(@event);
            List<UserDto> usersDto = mapper.Map<List<UserDto>>(users);

            return Ok(usersDto);
        }

        [Authorize]
        [HttpDelete("event/{eventId}/deleteUser/{userId}")]
        public async Task<IActionResult> DeleteUserInEvent(UserAndEventDto dto)
        {
            User user = dto.User;
            Event @event = dto.Event;

            await userRepositoryUseCase.DeleteUserFromEventAsync(@event, user);
            UserDto userDto = mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [Authorize]
        [HttpDelete("user/delete")]
        public async Task<IActionResult> DeleteUser(User user)
        {
            await userRepositoryUseCase.Delete(user);
            UserDto userDto = mapper.Map<UserDto>(user);

            return Ok(userDto);
        }
    }
}
