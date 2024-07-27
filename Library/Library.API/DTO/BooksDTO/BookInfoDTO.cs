using Library.API.Entities;

namespace Library.API.DTO.BooksDTO
{
    public class BookInfoDTO : BaseBookDTO
    {
        public int Id { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string PublisherName { get; set; }
        public string GenreName { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }

        public static BookInfoDTO FromBook(Book book)
        {
            return new BookInfoDTO()
            {
                Name = book.Name,

                AuthorFirstName = book.Author.FirstName,
                AuthorLastName = book.Author.LastName,
                PublisherName = book.Publisher.PublisherName,
                GenreName = book.Genre.GenreName,
                CustomerFirstName = book.Customer?.FirstName,
                CustomerLastName = book.Customer?.LastName,

                Id = book.Id,
                AuthorId = book.AuthorId,
                GenreId = book.GenreId,
                PublisherId = book.PublisherId,
                CustomerId = book.CustomerId,
            };
        }
    }
}
