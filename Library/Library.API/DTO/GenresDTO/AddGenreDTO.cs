using Library.API.Entities;

namespace Library.API.DTO.GenresDTO
{
    public class AddGenreDTO : BaseGenreDTO
    {

        public Genre ToGenre()
        {
            return new Genre()
            {
                GenreName = this.GenreName,
            };
        }

    }
}