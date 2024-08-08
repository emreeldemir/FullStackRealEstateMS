using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.API.Context;
using RealEstate.API.DTOs.Status;
using RealEstate.API.Entities;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly RealEstateContext _context;

        public StatusController(RealEstateContext context)
        {
            _context = context;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateStatus([FromBody] CreateStatusRequestDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Invalid status data!");
            }

            // Aynı isimde aktif bir status olup olmadığını kontrol et
            var existingStatus = await _context.Statuses
                                               .Where(s => s.Name == request.Name && !s.IsDeleted)
                                               .FirstOrDefaultAsync();

            if (existingStatus != null)
            {
                return Conflict(new { Message = "This status already exists!" });
            }

            var status = new Status
            {
                Name = request.Name
            };

            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStatusById), new { id = status.Id }, status);
        }


        [HttpGet("GetStatusById/{id}")]
        public async Task<IActionResult> GetStatusById(int id)
        {
            var status = await _context.Statuses
                                       .Where(s => s.Id == id && !s.IsDeleted)
                                       .FirstOrDefaultAsync();

            if (status == null)
            {
                return NotFound();
            }

            var response = new StatusResponseDTO
            {
                Id = status.Id,
                Name = status.Name
            };

            return Ok(response);
        }


        [HttpGet("GetAllStatuses")]
        public async Task<IActionResult> GetAllStatuses()
        {
            var statuses = await _context.Statuses
                                         .Where(s => !s.IsDeleted) // Sadece silinmemiş olanları getir
                                         .ToListAsync();
            var response = statuses.Select(s => new StatusResponseDTO
            {
                Id = s.Id,
                Name = s.Name
            });

            return Ok(response);
        }


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusRequestDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.Name))
            {
                return BadRequest("Invalid status data.");
            }

            var status = await _context.Statuses
                                       .Where(s => s.Id == id && !s.IsDeleted) // Sadece silinmemiş olanları getir
                                       .FirstOrDefaultAsync();

            if (status == null)
            {
                return NotFound();
            }

            // Aynı isimde aktif bir status olup olmadığını kontrol et
            var existingStatus = await _context.Statuses
                                               .Where(s => s.Name == request.Name && s.Id != id && !s.IsDeleted)
                                               .FirstOrDefaultAsync();

            if (existingStatus != null)
            {
                return Conflict(new { Message = "Bu status zaten var." });
            }

            status.Name = request.Name;

            _context.Statuses.Update(status);
            await _context.SaveChangesAsync();

            var response = new StatusResponseDTO
            {
                Id = status.Id,
                Name = status.Name
            };

            return Ok(response);
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            // Soft delete: IsDeleted kolonunu true olarak işaretleme
            status.IsDeleted = true;

            _context.Statuses.Update(status);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
