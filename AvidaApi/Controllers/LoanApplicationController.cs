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
        private readonly IPersonService _personService;
        private readonly IAddressService _addressService;
        private readonly IRepositoryLoanApplication _repositoryLoanApplication;
        private readonly IDecisionRulesService _decisionRulesService;
        private readonly ILogger<LoanApplicationController> _logger;

        public LoanApplicationController(LoanDBContext Context, IDecisionRulesService decisionRulesService, IIndatavalidation indatavalidation, IPersonService personService, IAddressService addressService, IRepositoryLoanApplication repositoryLoanApplication, ILoanService loanService, ILogger<LoanApplicationController> logger)
        {
            _context = Context;
            _loanService = loanService;
            _indatavalidation = indatavalidation;
            _personService = personService;
            _addressService = addressService;
            _repositoryLoanApplication = repositoryLoanApplication;
            _decisionRulesService = decisionRulesService;
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

            _decisionRulesService.MakeDecision(loanApplication);
            await _context.LoanApplication.AddAsync(loanApplication);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = loanApplication.Id }, loanApplication);
        }

        [HttpPut("{loanApplicationID:int}")]
        public async Task<ActionResult<LoanApplicationModel>> UpdateloanApplication(int loanApplicationID, LoanApplicationModel loanApplication)
        {
            var person = loanApplication.Person;
            if (loanApplicationID != loanApplication.Id)
            {
                return BadRequest();
            }

            if (person == null)
            {
                return BadRequest();
            }

            try
            {
                await _personService.UpdateAsync(person);

                if (person.Adress != null)
                {
                    await _addressService.UpdateAsync(person.Adress);
                }

                loanApplication.Person = person;
                await _repositoryLoanApplication.Update(loanApplication);
                var loan = loanApplication.Loan;
                await _loanService.UpdateAsync(loan, loanApplication.Decision);

                return loanApplication == null ? NotFound() : Ok(loanApplication);

                return NoContent();

            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
            }
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

