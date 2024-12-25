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
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public LoanController(ILoanService loanService, IUserService userService, IMapper mapper)
        {
            _loanService = loanService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoanDto>> GetLoan(int id)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);

            if (User.IsInRole("User") && loan.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Forbid();

            return _mapper.Map<LoanDto>(loan);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<LoanDto>>> GetUserLoans(int userId)
        {
            if (User.IsInRole("User") && userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Forbid();

            var loans = await _loanService.GetUserLoansAsync(userId);
            return Ok(_mapper.Map<IEnumerable<LoanDto>>(loans));
        }

        [HttpPost]
        public async Task<ActionResult<LoanDto>> CreateLoan(CreateLoanDto createLoanDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (await _userService.IsUserBlockedAsync(userId))
                return BadRequest("User is blocked from creating loans");

            var loan = _mapper.Map<Loan>(createLoanDto);
            loan.UserId = userId;

            var createdLoan = await _loanService.CreateLoanAsync(loan);
            return CreatedAtAction(nameof(GetLoan), new { id = createdLoan.Id }, _mapper.Map<LoanDto>(createdLoan));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LoanDto>> UpdateLoan(int id, UpdateLoanDto updateLoanDto)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);

            if (User.IsInRole("User") && loan.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Forbid();

            if (loan.Status != LoanStatus.InProcess)
                return BadRequest("Can only update loans that are in process");

            var updatedLoan = _mapper.Map<Loan>(updateLoanDto);
            var result = await _loanService.UpdateLoanAsync(id, updatedLoan);
            return _mapper.Map<LoanDto>(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLoan(int id)
        {
            var loan = await _loanService.GetLoanByIdAsync(id);

            if (User.IsInRole("User") && loan.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Forbid();

            if (loan.Status != LoanStatus.InProcess)
                return BadRequest("Can only delete loans that are in process");

            await _loanService.DeleteLoanAsync(id);
            return NoContent();
        }
    }
}
