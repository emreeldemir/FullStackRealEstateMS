using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("CreatePhoto")]
        public IActionResult CreatePhoto([FromBody] CreatePhotoRequestDTO createPhotoRequestDTO)
        {
            var newPhoto = new Photo
            {
                PropertyId = createPhotoRequestDTO.PropertyId,
                PhotoData = createPhotoRequestDTO.PhotoData
            };

            _context.Photos.Add(newPhoto);
            _context.SaveChanges();

            return Ok(new PhotoResponseDTO
            {
                Id = newPhoto.Id,
                PropertyId = newPhoto.PropertyId,
                PhotoData = newPhoto.PhotoData
            });
        }


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