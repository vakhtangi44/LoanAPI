using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetLoansByUserId(int userId)
        {
            var loans = await _loanService.GetLoansByUserIdAsync(userId);
            return Ok(loans);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddLoan(int userId, LoanDto loanDto)
        {
            await _loanService.AddLoanAsync(loanDto, userId);
            return Ok(loanDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLoan(LoanDto loanDto)
        {
            await _loanService.UpdateLoanAsync(loanDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            await _loanService.DeleteLoanAsync(id);
            return NoContent();
        }
    }
}
