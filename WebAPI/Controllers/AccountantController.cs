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
    //[Authorize(Roles = "Accountant")]
    public class AccountantController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountantController(ILoanService loanService, IUserService userService, IMapper mapper)
        {
            _loanService = loanService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("loans")]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetAllLoans()
        {
            var loans = await _loanService.GetAllLoansAsync();
            return Ok(_mapper.Map<IEnumerable<LoanDto>>(loans));
        }

        [HttpPut("loans/{id}/status")]
        public async Task<ActionResult<LoanDto>> UpdateLoanStatus(int id, LoanStatus status)
        {
            var loan = await _loanService.UpdateLoanStatusAsync(id, status);
            return _mapper.Map<LoanDto>(loan);
        }

        [HttpPost("users/{userId}/block")]
        public async Task<ActionResult> BlockUser(int userId, [FromBody] BlockUserDto? blockUserDto)
        {
            if (blockUserDto == null || blockUserDto.Until == default)
                return BadRequest("Invalid block duration provided.");

            await _userService.BlockUserAsync(userId, blockUserDto.Until);
            return Ok();
        }

        [HttpPost("users/{userId}/unblock")]
        public async Task<ActionResult> UnblockUser(int userId)
        {
            await _userService.UnblockUserAsync(userId);
            return Ok();
        }
    }
}
