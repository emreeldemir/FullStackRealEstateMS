using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.API.Context;
using RealEstate.API.DTOs.Photo;
using RealEstate.API.DTOs.Property;
using RealEstate.API.Entities;
using RealEstate.API.Entities.Identity;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly RealEstateContext _context;

        public PropertyController(RealEstateContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Invalid property data.");
            }

            var property = new Property
            {
                Title = request.Title,
                Description = request.Description,
                TypeId = request.TypeId,
                StatusId = request.StatusId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Price = request.Price,
                CurrencyId = request.CurrencyId,
                UserId = request.UserId,
                Longitude = request.Longitude,
                Latitude = request.Latitude
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPropertyById), new { id = property.Id }, property);
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("GetPropertyById/{id}")]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            var property = await _context.Properties
                                         .Include(p => p.Type)
                                         .Include(p => p.Status)
                                         .Include(p => p.Currency)
                                         .Include(p => p.User)
                                         .Include(p => p.Photos) // Include Photos
                                         .Where(p => p.Id == id && !p.IsDeleted)
                                         .FirstOrDefaultAsync();

            if (property == null)
            {
                return NotFound();
            }

            var response = new PropertyResponseDTO
            {
                Id = property.Id,
                Title = property.Title,
                Description = property.Description,
                TypeId = property.TypeId,
                TypeName = property.Type.Name,
                StatusId = property.StatusId,
                StatusName = property.Status.Name,
                StartDate = property.StartDate,
                EndDate = property.EndDate,
                Price = property.Price,
                CurrencyId = property.CurrencyId,
                CurrencyName = property.Currency.Name,
                UserId = property.UserId,
                UserName = property.User.UserName,
                Longitude = property.Longitude,
                Latitude = property.Latitude,
                Photos = property.Photos.Select(photo => new PhotoResponseDTO
                {
                    Id = photo.Id,
                    PhotoData = photo.PhotoData // Ensure this matches PhotoResponseDTO
                }).ToList()
            };

            return Ok(response);
        }

        [Authorize(Roles = "admin, user")]
        [HttpGet("GetAllProperties")]
        public async Task<IActionResult> GetAllProperties()
        {
            var properties = await _context.Properties
                                           .Include(p => p.Type)
                                           .Include(p => p.Status)
                                           .Include(p => p.Currency)
                                           .Include(p => p.User)
                                           .Include(p => p.Photos) // Include Photos
                                           .Where(p => !p.IsDeleted) // Sadece silinmemiş olanları getir
                                           .ToListAsync();

            var response = properties.Select(property => new PropertyResponseDTO
            {
                Id = property.Id,
                Title = property.Title,
                Description = property.Description,
                TypeId = property.TypeId,
                TypeName = property.Type.Name,
                StatusId = property.StatusId,
                StatusName = property.Status.Name,
                StartDate = property.StartDate,
                EndDate = property.EndDate,
                Price = property.Price,
                CurrencyId = property.CurrencyId,
                CurrencyName = property.Currency.Name,
                UserId = property.UserId,
                UserName = property.User.UserName,
                Longitude = property.Longitude,
                Latitude = property.Latitude,
                Photos = property.Photos.Select(photo => new PhotoResponseDTO
                {
                    Id = photo.Id,
                    PhotoData = photo.PhotoData // Ensure this matches PhotoResponseDTO
                }).ToList()
            });

            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody] UpdatePropertyRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Invalid property data.");
            }

            var property = await _context.Properties
                                         .Where(p => p.Id == id && !p.IsDeleted)
                                         .FirstOrDefaultAsync();

            if (property == null)
            {
                return NotFound();
            }

            property.Title = request.Title ?? property.Title;
            property.Description = request.Description ?? property.Description;
            property.TypeId = request.TypeId != 0 ? request.TypeId : property.TypeId;
            property.StatusId = request.StatusId != 0 ? request.StatusId : property.StatusId;
            property.StartDate = request.StartDate != default ? request.StartDate : property.StartDate;
            property.EndDate = request.EndDate != default ? request.EndDate : property.EndDate;
            property.Price = request.Price != 0 ? request.Price : property.Price;
            property.CurrencyId = request.CurrencyId != 0 ? request.CurrencyId : property.CurrencyId;
            property.UserId = request.UserId != 0 ? request.UserId : property.UserId;
            property.Longitude = request.Longitude != 0 ? request.Longitude : property.Longitude;
            property.Latitude = request.Latitude != 0 ? request.Latitude : property.Latitude;

            _context.Properties.Update(property);
            await _context.SaveChangesAsync();

            var response = new PropertyResponseDTO
            {
                Id = property.Id,
                Title = property.Title,
                Description = property.Description,
                TypeId = property.TypeId,
                TypeName = property.Type.Name,
                StatusId = property.StatusId,
                StatusName = property.Status.Name,
                StartDate = property.StartDate,
                EndDate = property.EndDate,
                Price = property.Price,
                CurrencyId = property.CurrencyId,
                CurrencyName = property.Currency.Name,
                UserId = property.UserId,
                UserName = property.User.UserName,
                Longitude = property.Longitude,
                Latitude = property.Latitude,
                Photos = property.Photos.Select(photo => new PhotoResponseDTO
                {
                    Id = photo.Id,
                    PhotoData = photo.PhotoData // Ensure this matches PhotoResponseDTO
                }).ToList()
            };

            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null)
            {
                return NotFound();
            }

            // Soft delete: IsDeleted kolonunu true olarak işaretleme
            property.IsDeleted = true;

            _context.Properties.Update(property);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
