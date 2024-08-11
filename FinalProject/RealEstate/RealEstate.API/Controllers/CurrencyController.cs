using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.API.Context;
using RealEstate.API.DTOs.Currency;
using RealEstate.API.Entities;


namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  
    public class CurrencyController : ControllerBase
    {
        private readonly RealEstateContext _context;

        public CurrencyController(RealEstateContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCurrency([FromBody] CreateCurrencyRequestDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Invalid currency data!");
            }

            // Aynı isimde aktif bir currency olup olmadığını kontrol et
            var existingCurrency = await _context.Currencies
                                                 .Where(c => c.Name == request.Name && !c.IsDeleted)
                                                 .FirstOrDefaultAsync();

            if (existingCurrency != null)
            {
                return Conflict(new { Message = "This currency already exists!" });
            }

            var currency = new Currency
            {
                Name = request.Name
            };

            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCurrencyById), new { id = currency.Id }, currency);
        }



        [HttpGet("GetCurrencyById/{id}")]
        public async Task<IActionResult> GetCurrencyById(int id)
        {
            var currency = await _context.Currencies
                                         .Where(c => c.Id == id && !c.IsDeleted) // Sadece silinmemiş olanları getir
                                         .FirstOrDefaultAsync();

            if (currency == null)
            {
                return NotFound();
            }

            var response = new CurrencyResponseDTO
            {
                Id = currency.Id,
                Name = currency.Name
            };

            return Ok(response);
        }


        
        [HttpGet("GetAllCurrencies")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var currencies = await _context.Currencies
                                            .Where(c => !c.IsDeleted) // Sadece silinmemiş olanları getir
                                            .ToListAsync();
            var response = currencies.Select(c => new CurrencyResponseDTO
            {
                Id = c.Id,
                Name = c.Name
            });

            return Ok(response);
        }


        [Authorize(Roles = "admin")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCurrency(int id, [FromBody] UpdateCurrencyRequestDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Invalid currency data.");
            }

            var currency = await _context.Currencies
                                         .Where(c => c.Id == id && !c.IsDeleted) // Sadece silinmemiş olanları getir
                                         .FirstOrDefaultAsync();

            if (currency == null)
            {
                return NotFound();
            }

            currency.Name = request.Name;

            _context.Currencies.Update(currency);
            await _context.SaveChangesAsync();

            var response = new CurrencyResponseDTO
            {
                Id = currency.Id,
                Name = currency.Name
            };

            return Ok(response);
        }


        [Authorize(Roles = "admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            var currency = await _context.Currencies.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }

            // Soft delete: IsDeleted kolonunu true olarak işaretleme
            currency.IsDeleted = true;

            _context.Currencies.Update(currency);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
