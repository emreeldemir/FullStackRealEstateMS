using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.API.Context;
using RealEstate.API.DTOs.Type;
using RealEstate.API.Entities;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeController : ControllerBase
    {
        private readonly RealEstateContext _context;

        public TypeController(RealEstateContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateType([FromBody] CreateTypeRequestDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Invalid type data.");
            }

            // Aynı isimde aktif bir type olup olmadığını kontrol et
            var existingType = await _context.Types
                                             .Where(t => t.Name == request.Name && !t.IsDeleted)
                                             .FirstOrDefaultAsync();

            if (existingType != null)
            {
                return Conflict(new { Message = "This type already exists!" });
            }

            var type = new Entities.Type
            {
                Name = request.Name
            };

            _context.Types.Add(type);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTypeById), new { id = type.Id }, type);
        }


        [HttpGet("GetTypeById/{id}")]
        public async Task<IActionResult> GetTypeById(int id)
        {
            var type = await _context.Types
                                     .Where(t => t.Id == id && !t.IsDeleted)
                                     .FirstOrDefaultAsync();

            if (type == null)
            {
                return NotFound();
            }

            var response = new TypeResponseDTO
            {
                Id = type.Id,
                Name = type.Name
            };

            return Ok(response);
        }


        [HttpGet("GetAllTypes")]
        public async Task<IActionResult> GetAllTypes()
        {
            var types = await _context.Types
                                      .Where(t => !t.IsDeleted) // Sadece silinmemiş olanları getir
                                      .ToListAsync();
            var response = types.Select(t => new TypeResponseDTO
            {
                Id = t.Id,
                Name = t.Name
            });

            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateType(int id, [FromBody] UpdateTypeRequestDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Invalid type data.");
            }

            var type = await _context.Types
                                     .Where(t => t.Id == id && !t.IsDeleted) // Sadece silinmemiş olanları getir
                                     .FirstOrDefaultAsync();

            if (type == null)
            {
                return NotFound();
            }

            // Aynı isimde aktif bir type olup olmadığını kontrol et
            var existingType = await _context.Types
                                             .Where(t => t.Name == request.Name && t.Id != id && !t.IsDeleted)
                                             .FirstOrDefaultAsync();

            if (existingType != null)
            {
                return Conflict(new { Message = "This type already exists!" });
            }

            type.Name = request.Name;

            _context.Types.Update(type);
            await _context.SaveChangesAsync();

            var response = new TypeResponseDTO
            {
                Id = type.Id,
                Name = type.Name
            };

            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            var type = await _context.Types.FindAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            // Soft delete: IsDeleted kolonunu true olarak işaretleme
            type.IsDeleted = true;

            _context.Types.Update(type);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
