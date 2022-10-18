using AvidaApi.Data;
using AvidaApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvidaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanApplicationController : ControllerBase
    {
        private readonly LoanDBContext _context;

        public LoanApplicationController(LoanDBContext Context)
        {
            _context = Context;
        }
        [HttpGet]
        public async Task<IEnumerable<LoanApplicationModel>> Get()
        {
            return await _context.LoanApplication.ToListAsync();
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(LoanApplicationModel),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(LoanApplicationModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var loanApplication = await _context.LoanApplication.FindAsync(id);
            return loanApplication == null ? NotFound() : Ok(loanApplication);
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoanApplicationModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(LoanApplicationModel loanApplication)
        {
            await _context.LoanApplication.AddAsync(loanApplication);
            await _context.SaveChangesAsync();
           return CreatedAtAction(nameof(GetById),new {id = loanApplication.Id}, loanApplication);   
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


           // _context.SaveChanges(); // Works
            await _context.SaveChangesAsync();

            return NoContent();
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

    }
}

