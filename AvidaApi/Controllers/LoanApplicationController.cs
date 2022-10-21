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
        private readonly IDecisionRulesService _loanService;
        private readonly IIndatavalidation _indatavalidation;
        private readonly ILogger<LoanApplicationController> _logger;

        public LoanApplicationController(LoanDBContext Context, IDecisionRulesService loanService, IIndatavalidation indatavalidation, ILogger<LoanApplicationController> logger)
        {
            _context = Context;
            _loanService = loanService;
            _indatavalidation = indatavalidation;
            _logger = logger;
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
            LoanApplicationModel? loanApplication = await PupulateLoanApplicationModel(id);

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
                _logger.LogError("Logging BadRequest", errorMessage);

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
        [ProducesResponseType(typeof(LoanApplicationModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, LoanApplicationModel loanApplication)
        {

            if (id != loanApplication.Id)
            {
                return BadRequest();
            }


            _context.Entry(loanApplication).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<LoanApplicationModel> PupulateLoanApplicationModel(int id)
        {
            LoanApplicationModel? loanApplication = await _context.LoanApplication.FindAsync(id);

            if (loanApplication != null)
            {
                PersonModel? person = await _context.Person.FindAsync(loanApplication.PersonId);

                if (person != null)
                {

                    var adress = await _context.Adress.FindAsync(person.AdressID);
                    loanApplication.Person = person;

                    if (adress != null)

                        loanApplication.Person.Adress = adress;
                }

                LoanModel? loan = await _context.LoanModel.FindAsync(loanApplication.LoanID);

                if (loan != null)
                {
                    loanApplication.Loan = loan;
                }
            }

            return loanApplication;
        }
    }
}

