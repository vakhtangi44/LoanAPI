using Application.DTOs.LoanDtos;
using Application.DTOs.UserDtos;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Accountant")]
    public class AccountantController(ILoanService loanService, IUserService userService, IMapper mapper)
        : ControllerBase
    {
        [HttpGet("loans")]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetAllLoans()
        {
            var loans = await loanService.GetAllLoansAsync();
            return Ok(mapper.Map<IEnumerable<LoanDto>>(loans));
        }

        [HttpPut("loans/{id}/status")]
        public async Task<ActionResult<LoanDto>> UpdateLoanStatus(int id, LoanStatus status)
        {
            var loan = await loanService.UpdateLoanStatusAsync(id, status);
            return mapper.Map<LoanDto>(loan);
        }

        [HttpPost("users/{userId}/block")]
        public async Task<ActionResult> BlockUser(int userId, [FromBody] BlockUserDto? blockUserDto)
        {
            if (blockUserDto == null || blockUserDto.Until == default)
                return BadRequest("Invalid block duration provided.");

            await userService.BlockUserAsync(userId, blockUserDto.Until);
            return Ok();
        }

        [HttpPost("users/{userId}/unblock")]
        public async Task<ActionResult> UnblockUser(int userId)
        {
            await userService.UnblockUserAsync(userId);
            return Ok();
        }
    }
}
