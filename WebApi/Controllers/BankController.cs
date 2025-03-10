using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly APIDBContext _context;
        private readonly ILogger<BankController> _logger;

        public BankController(APIDBContext context, ILogger<BankController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Bank
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bank>>> GetBanks()
        {
            try
            {
                if (_context.Banks == null)
                {
                    _logger.LogError("Database context is null.");
                    return StatusCode(500, "Database connection issue.");
                }

                var banks = await _context.Banks.ToListAsync();

                if (!banks.Any())
                {
                    return NotFound("No banks found.");
                }

                return Ok(banks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching banks: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        // GET: api/Bank/5
        [HttpGet("{id:int}")] // Restricts ID to be an integer
        public async Task<ActionResult<Bank>> GetBank(int id)
        {
            try
            {
                if (_context.Banks == null)
                {
                    return StatusCode(500, "Database connection issue.");
                }

                if (id <= 0) // Ensure ID is valid
                {
                    return BadRequest("ID must be a positive integer.");
                }

                var bank = await _context.Banks.FindAsync(id);

                if (bank == null)
                {
                    return NotFound($"Bank with ID {id} not found.");
                }

                return Ok(bank);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching bank ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }


        // POST: api/Bank
        [HttpPost]
        public async Task<IActionResult> PostBank([FromBody] Bank bank)
        {
            try
            {
                if (bank == null)
                {
                    return BadRequest("Invalid bank data.");
                }

                if (string.IsNullOrWhiteSpace(bank.BankName))
                {
                    return BadRequest("Bank name is required.");
                }

                _context.Banks.Add(bank);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBank), new { id = bank.BankId }, bank);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating bank: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        // PUT: api/Bank/5
        [HttpPut("{id:int}")] // Ensure ID is an integer
        public async Task<IActionResult> PutBank(int id, [FromBody] Bank bank)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID. ID must be a positive integer.");
            }

            if (bank == null || id != bank.BankId)
            {
                return BadRequest("Bank ID mismatch.");
            }

            _context.Entry(bank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Banks.Any(e => e.BankId == id))
                {
                    return NotFound($"Bank with ID {id} not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Successful update
        }


        // DELETE: api/Bank/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBank(int id)
        {
            try
            {
                var bank = await _context.Banks.FindAsync(id);
                if (bank == null)
                {
                    return NotFound($"Bank with ID {id} not found.");
                }

                _context.Banks.Remove(bank);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting bank ID {id}: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        private bool BankExists(int id)
        {
            return _context.Banks?.Any(e => e.BankId == id) ?? false;
        }
    }
}

    
