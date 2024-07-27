using Library.API.Context;
using Library.API.DTO.BooksDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly LibraryContext context;

        public BookController(LibraryContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddBookDTO book)
        {
            var newbook = context.Books.Add(book.ToBook());
            context.SaveChanges();
            var response = context.Books.Include(a => a.Author)
                                    .Include(a => a.Publisher)
                                    .Include(a => a.Customer)
                                    .Include(a => a.Genre)
                .FirstOrDefault(x => x.Id == newbook.Entity.Id && !x.isDeleted);
            return Ok(BookInfoDTO.FromBook(response));
        }

        [HttpPut]
        public IActionResult Put(EditBookDTO bookDTO)
        {
            int id = bookDTO.Id;

            string name = bookDTO.Name;
            int authorId = bookDTO.AuthorId;
            int? customerId = bookDTO.CustomerId;
            int genreId = bookDTO.GenreId;
            int publisherId = bookDTO.PublisherId;

            var book = context.Books.FirstOrDefault(x => x.Id == id);
            if (book is null) { return NotFound(); }

            book.PublisherId = publisherId;
            book.AuthorId = authorId;
            book.CustomerId = customerId;
            book.GenreId = genreId;
            book.Name = name;

            context.SaveChanges();
            return Ok(BookInfoDTO.FromBook(book));
        }

        [HttpPut]
        [Route("borrow")]

        public IActionResult Borrow(BorrowBookDTO borrowDTO)
        {
            var book = context.Books.FirstOrDefault(x => x.Id == borrowDTO.BookId && !x.isDeleted);

            if (book is null)
            {
                return NotFound();
            }

            if (book.CustomerId.HasValue)
            {
                return Ok("Book already borrowed.");
            }

            book.CustomerId = borrowDTO.CustomerId;
            context.SaveChanges();

            return Ok("Borrowed");
        }

        [HttpPut]
        [Route("return")]

        public IActionResult Return(BorrowBookDTO borrowDTO)
        {
            var book = context.Books.FirstOrDefault(x => x.Id == borrowDTO.BookId && !x.isDeleted);

            if (book is null)
            {
                return NotFound();
            }

            if (!book.CustomerId.HasValue)
            {
                return Ok("Book has not been borrowed.");
            }

            if (book.CustomerId != borrowDTO.CustomerId)
            {
                return Ok("Book has been borrowed by another customer.");
            }


            book.CustomerId = null;
            context.SaveChanges();

            return Ok("Returned");
        }


        [HttpGet]
        public IActionResult Get(int id)
        {
            var book = context.Books.Include(a => a.Author)
                                    .Include(a => a.Publisher)
                                    .Include(a => a.Customer)
                                    .Include(a => a.Genre)
                .FirstOrDefault(x => x.Id == id && !x.isDeleted);
            if (book == null) return NotFound();

            return Ok(BookInfoDTO.FromBook(book));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var book = context.Books.FirstOrDefault(x => x.Id == id);
            if (book == null) return NotFound();
            book.isDeleted = true;

            context.SaveChanges();

            return NoContent();
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAll()
        {
            var book = context.Books.Include(a => a.Author)
                                    .Include(a => a.Publisher)
                                    .Include(a => a.Customer)
                                    .Include(a => a.Genre)
                                    .Where(x => !x.isDeleted).ToList();
            if (book == null) return NotFound();

            List<BookInfoDTO> listDTO = new List<BookInfoDTO>();

            book.ForEach(x => listDTO.Add(BookInfoDTO.FromBook(x)));

            return Ok(listDTO);

        }
    }

}
