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
    public class BankAccountController : ControllerBase
    {
        private readonly APIDBContext _context;
        private readonly ILogger<BankAccountController> _logger;

        public BankAccountController(APIDBContext context, ILogger<BankAccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/BankAccount/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccount>> GetBankAccount(int id)
        {
            var bankAccount = await _context.BankAccounts.FindAsync(id);

            if (bankAccount == null)
            {
                return NotFound();
            }

            return bankAccount;
        }

        // GET: api/BankAccount
        [HttpGet]
        public async Task<IActionResult> GetBankAccounts()
        {
            try
            {
                var bankAccounts = await _context.BankAccounts.ToListAsync();

                if (bankAccounts == null || !bankAccounts.Any())
                {
                    return NotFound("No bank accounts found.");
                }

                return Ok(bankAccounts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving bank accounts: {ex.Message}");
                return StatusCode(500, "Internal server error while retrieving bank accounts.");
            }
        }

        // POST: api/BankAccount
        [HttpPost]
        public async Task<ActionResult<BankAccount>> PostBankAccount([FromBody] BankAccount bankAccount)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(bankAccount.AccountHolder))
                {
                    return BadRequest("AccountHolder is required.");
                }

                _context.BankAccounts.Add(bankAccount);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBankAccount), new { id = bankAccount.BankAccountId }, bankAccount);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Database error: {ex.InnerException?.Message ?? ex.Message}");
                return StatusCode(500, "An error occurred while saving the entity changes.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
        // PUT: api/BankAccount/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankAccount(int id, [FromBody] BankAccount bankAccount)
        {
            if (bankAccount == null)
            {
                return BadRequest("BankAccount object is null.");
            }

            if (id != bankAccount.BankAccountId)
            {
                return BadRequest("ID in the URL does not match the ID in the request body.");
            }

            try
            {
                var existingBankAccount = await _context.BankAccounts.FindAsync(id);

                if (existingBankAccount == null)
                {
                    return NotFound($"Bank account with ID {id} not found.");
                }

                // Update fields manually to prevent overwriting unintended fields
                existingBankAccount.AccountHolder = bankAccount.AccountHolder;
                existingBankAccount.AccountNumber = bankAccount.AccountNumber;
                // Remove the line that updates the Balance property
                // existingBankAccount.Balance = bankAccount.Balance;

                _context.BankAccounts.Update(existingBankAccount);
                await _context.SaveChangesAsync();

                return Ok(existingBankAccount); // Return updated entity
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError($"Concurrency error updating bank account {id}: {ex.Message}");
                return StatusCode(409, "A concurrency error occurred while updating the bank account.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error updating bank account {id}: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred while updating the bank account.");
            }
        }
        // DELETE: api/BankAccount/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankAccount(int id)
        {
            try
            {
                var bankAccount = await _context.BankAccounts.FindAsync(id);
                if (bankAccount == null)
                {
                    return NotFound($"Bank account with ID {id} not found.");
                }

                _context.BankAccounts.Remove(bankAccount);
                await _context.SaveChangesAsync();

                return Ok($"Bank account with ID {id} has been successfully deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting bank account {id}: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred while deleting the bank account.");
            }
        }


    }
}