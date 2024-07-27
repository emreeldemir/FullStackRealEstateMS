namespace Library.API.DTO.BooksDTO
{
    public class BaseBookDTO
    {
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public int PublisherId { get; set; }
        public int? CustomerId { get; set; }



    }
}
