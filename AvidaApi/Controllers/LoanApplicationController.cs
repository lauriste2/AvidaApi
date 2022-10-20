using AvidaApi.Data;
using AvidaApi.Models;
using AvidaApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvidaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanApplicationController : ControllerBase
    {
        private readonly LoanDBContext _context;
        private readonly ILoanService _loanService;
        private readonly IIndatavalidation _indatavalidation;

        public LoanApplicationController(LoanDBContext Context, ILoanService loanService, IIndatavalidation indatavalidation)
        {
            _context = Context;
            _loanService = loanService;
            _indatavalidation = indatavalidation;
        }
        [HttpGet]
        public async Task<IEnumerable<LoanApplicationModel>> Get()
        {
            return await _context.LoanApplication.ToListAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(LoanApplicationModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(LoanApplicationModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var loanApplication = await _context.LoanApplication.FindAsync(id);
            return loanApplication == null ? NotFound() : Ok(loanApplication);
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoanApplicationModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(LoanApplicationModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(LoanApplicationModel loanApplication)
        {
            string errorMessage = string.Empty;

            errorMessage = _indatavalidation.ValidateLoanApplication(loanApplication);

          
            if (errorMessage.Length > 0)
            {
                return BadRequest(errorMessage);
            }

            _loanService.MakeDecision(loanApplication);
            await _context.LoanApplication.AddAsync(loanApplication);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = loanApplication.Id }, loanApplication);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(LoanApplicationModel), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(LoanApplicationModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var loanApplicationToDelete = await _context.LoanApplication.FindAsync(id);

            if (loanApplicationToDelete == null) return NotFound();

            _context.LoanApplication.Remove(loanApplicationToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(LoanApplicationModel), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(LoanApplicationModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MakeDecision(int id)
        {
            var loanApplication = await _context.LoanApplication.FindAsync(id);

            if (loanApplication == null)
            {
                return NotFound();
            }

            _loanService.MakeDecision(loanApplication);

            _context.Entry(loanApplication).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //private void MakeDecision(LoanApplicationModel loanApplication)
        //{
        //    var person = loanApplication.Person;
        //    var loan = loanApplication.Loan;

        //    if (person.MonthlyIncome > 31000 && loan.LoanAmount > 0)
        //        loanApplication.Decision = true;
        //    else
        //        loanApplication.Decision = false;
        //}
    }
}

