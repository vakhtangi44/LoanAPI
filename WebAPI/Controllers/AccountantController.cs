using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Accountant")]
    public class AccountantController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountantController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _accountService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("block/{userId}")]
        public async Task<IActionResult> BlockUser(int userId)
        {
            await _accountService.BlockUserAsync(userId);
            return NoContent();
        }

        [HttpPost("unblock/{userId}")]
        public async Task<IActionResult> UnblockUser(int userId)
        {
            await _accountService.UnblockUserAsync(userId);
            return NoContent();
        }
    }
}