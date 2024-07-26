using Library.API.Entities;

namespace Library.API.DTO.AuthorsDTO
{
    public class AddAuthorDTO : BaseAuthorDTO
    {
        
        public Author ToAuthor()
        {
            return new Author
            {
                FirstName = this.FirstName,
                LastName = this.LastName
            };
        }

    }
}
