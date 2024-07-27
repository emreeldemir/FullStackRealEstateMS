using Library.API.Context;
using Library.API.DTO.GenresDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly LibraryContext context;

        public GenreController(LibraryContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddGenreDTO genre)
        {
            var response = context.Genres.Add(genre.ToGenre());
            context.SaveChanges();

            return Ok(GenreInfoDTO.FromGenre(response.Entity));
        }

        [HttpPut]
        public IActionResult Put([FromBody] EditGenreDTO editGenreDTO)
        {
            int id = editGenreDTO.Id;
            string name = editGenreDTO.GenreName;

            var genre = context.Genres.FirstOrDefault(x => x.Id == editGenreDTO.Id);
            if (genre == null) return NotFound();

            genre.GenreName = name;
            context.SaveChanges();
            
            return Ok(GenreInfoDTO.FromGenre(genre));
            

        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var genre = context.Genres.FirstOrDefault(x => x.Id == id);
            if (genre == null) return NotFound();

            return Ok(GenreInfoDTO.FromGenre(genre));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var genre = context.Genres.FirstOrDefault(x => x.Id == id);
            if (genre == null) return NotFound();
            genre.isDeleted = true;

            context.SaveChanges();

            return NoContent();
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAll()
        {
            var genre = context.Genres.Where(x => x.isDeleted == false).ToList();
            if (genre == null) return NotFound();

            List<GenreInfoDTO> genreInfoDTOs = new List<GenreInfoDTO>();
            genre.ForEach(x => genreInfoDTOs.Add(GenreInfoDTO.FromGenre(x)));

            return Ok(genreInfoDTOs);

        }

    }
}