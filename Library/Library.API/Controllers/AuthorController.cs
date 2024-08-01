using Library.API.Context;
using Library.API.DTO.AuthorsDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly LibraryContext context;

        public AuthorController(LibraryContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddAuthorDTO author)
        {
            var response = context.Authors.Add(author.ToAuthor());
            context.SaveChanges();

            return Ok(AuthorInfoDTO.FromAuthor(response.Entity));
        }

        [HttpPut]
        public IActionResult Put([FromBody] EditAuthorDTO editAuthorDTO)
        {
            int id = editAuthorDTO.Id;
            string firstName = editAuthorDTO.FirstName;
            string lastName = editAuthorDTO.LastName;

            var author = context.Authors.FirstOrDefault(x => x.Id == editAuthorDTO.Id);
            if (author == null) return NotFound();

            author.FirstName = firstName;
            author.LastName = lastName;
            context.SaveChanges();

            return Ok(AuthorInfoDTO.FromAuthor(author));

        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var author = context.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null) return NotFound();

            return Ok(AuthorInfoDTO.FromAuthor(author));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var author = context.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null) return NotFound();
            author.isDeleted = true;

            context.SaveChanges();

            return NoContent();
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAll()
        {
            var author = context.Authors.Where(x => x.isDeleted == false).ToList();
            if (author == null) return NotFound();

            List<AuthorInfoDTO> authorInfoDTOs = new List<AuthorInfoDTO>();
            author.ForEach(x => authorInfoDTOs.Add(AuthorInfoDTO.FromAuthor(x)));


            return Ok(authorInfoDTOs);

        }
    }
}
