using Application.DTOs.UserDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService, IMapper mapper) : ControllerBase
    {
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await userService.GetUserByIdAsync(id);
            return mapper.Map<UserDto>(user);
        }

        [HttpPost]
        [Authorize(Roles = "Accountant")]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createUserDto)
        {
            var user = mapper.Map<User>(createUserDto);
            var createdUser = await userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, mapper.Map<UserDto>(createdUser));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var user = mapper.Map<User>(updateUserDto);
            var updatedUser = await userService.UpdateUserAsync(id, user);
            return mapper.Map<UserDto>(updatedUser);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Accountant")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
