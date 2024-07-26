namespace Library.API.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }

        public Author Author { get; set; }
        public int AuthorId { get; set; }

        public Genre Genre { get; set; }
        public int GenreId { get; set; }

        public Publisher Publisher { get; set; }
        public int PublisherId { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public Book()
        {
            
        }
    }
}
