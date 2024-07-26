namespace Library.API.Entities
{
    public class Customer : Citizen
    {
        public ICollection<Book> Books { get; set; }
        public string Email { get; set; }
    }
}
