using Library.API.Entities;

namespace Library.API.DTO.BooksDTO
{
    public class AddBookDTO : BaseBookDTO
    {
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public int PublisherId { get; set; }
        public int CustomerId { get; set; }

        public Book ToBook()
        {
            return new Book()
            {
                Name = this.Name,
                AuthorId = this.AuthorId,
                GenreId = this.GenreId,
                PublisherId = this.PublisherId,
                CustomerId = this.CustomerId,
            };
        }
    }
}
