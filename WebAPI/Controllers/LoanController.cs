using Application.DTOs.LoanDtos;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LoanController(ILoanService loanService, IUserService userService, IMapper mapper)
        : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanDto>> GetLoan(int id)
        {
            var loan = await loanService.GetLoanByIdAsync(id);

            if (User.IsInRole("User") && loan.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                return Forbid();

            return mapper.Map<LoanDto>(loan);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetUserLoans(int userId)
        {
            if (User.IsInRole("User") && userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                return Forbid();

            var loans = await loanService.GetUserLoansAsync(userId);
            return Ok(mapper.Map<IEnumerable<LoanDto>>(loans));
        }

        [HttpPost]
        public async Task<ActionResult<LoanDto>> CreateLoan(CreateLoanDto createLoanDto)
        {

            if (await userService.IsUserBlockedAsync(createLoanDto.Id))
                return BadRequest("User is blocked from creating loans");

            var loan = mapper.Map<Loan>(createLoanDto);
            loan.UserId = createLoanDto.Id;

            var createdLoan = await loanService.CreateLoanAsync(loan);
            return CreatedAtAction(nameof(GetLoan), new { id = createdLoan.Id }, mapper.Map<LoanDto>(createdLoan));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LoanDto>> UpdateLoan(int id, UpdateLoanDto updateLoanDto)
        {
            var loan = await loanService.GetLoanByIdAsync(id);

            if (User.IsInRole("User") && loan.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                return Forbid();

            if (loan.Status != LoanStatus.InProcess)
                return BadRequest("Can only update loans that are in process");

            var updatedLoan = mapper.Map<Loan>(updateLoanDto);
            var result = await loanService.UpdateLoanAsync(id, updatedLoan);
            return mapper.Map<LoanDto>(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLoan(int id)
        {
            var loan = await loanService.GetLoanByIdAsync(id);

            if (User.IsInRole("User") && loan.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value))
                return Forbid();

            if (loan.Status != LoanStatus.InProcess)
                return BadRequest("Can only delete loans that are in process");

            await loanService.DeleteLoanAsync(id);
            return NoContent();
        }
    }
}
