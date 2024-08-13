using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.API.Context;
using RealEstate.API.DTOs.Photo;
using RealEstate.API.Entities;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoController : ControllerBase
    {
        private readonly RealEstateContext _context;

        public PhotoController(RealEstateContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin, user")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreatePhoto([FromBody] CreatePhotoRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Invalid photo data.");
            }

            // Property'nin var olup olmadığını kontrol et
            var propertyExists = await _context.Properties.AnyAsync(p => p.Id == request.PropertyId && !p.IsDeleted);
            if (!propertyExists)
            {
                return NotFound("Property not found or it has been deleted.");
            }

            var photo = new Photo
            {
                PropertyId = request.PropertyId,
                PhotoData = request.PhotoData
            };

            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPhotoById), new { id = photo.Id }, photo);
        }


        [Authorize(Roles = "admin, user")]
        [HttpGet("GetPhotoById/{id}")]
        public IActionResult GetPhotoById(int id)
        {
            var photo = _context.Photos
                .Where(p => p.Id == id && !p.IsDeleted)
                .Select(p => new PhotoResponseDTO
                {
                    Id = p.Id,
                    PropertyId = p.PropertyId,
                    PhotoData = p.PhotoData
                })
                .FirstOrDefault();

            if (photo == null)
                return NotFound("Photo not found");

            return Ok(photo);
        }


        [Authorize(Roles = "admin, user")]
        [HttpGet("GetPhotosByPropertyId/{propertyId}")]
        public async Task<IActionResult> GetPhotosByPropertyId(int propertyId)
        {
            var photos = await _context.Photos
                                       .Where(p => p.PropertyId == propertyId && !p.IsDeleted)
                                       .ToListAsync();

            if (photos == null || !photos.Any())
            {
                return NotFound("No photos found for the given property.");
            }

            var response = photos.Select(photo => new PhotoResponseDTO
            {
                Id = photo.Id,
                PropertyId = photo.PropertyId,
                PhotoData = photo.PhotoData
            }).ToList();

            return Ok(response);
        }


        [Authorize(Roles = "admin, user")]
        [HttpGet("GetAllPhotos")]
        public IActionResult GetAllPhotos()
        {
            var photos = _context.Photos
                .Where(p => !p.IsDeleted)
                .Select(p => new PhotoResponseDTO
                {
                    Id = p.Id,
                    PropertyId = p.PropertyId,
                    PhotoData = p.PhotoData
                })
                .ToList();

            return Ok(photos);
        }


        [Authorize(Roles = "admin")]
        [HttpDelete("DeletePhoto/{id}")]
        public IActionResult DeletePhoto(int id)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id && !p.IsDeleted);

            if (photo == null)
                return NotFound("Photo not found");

            photo.IsDeleted = true;
            _context.SaveChanges();

            return NoContent();
        }
    }
}