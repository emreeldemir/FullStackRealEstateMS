using Library.API.Entities;

namespace Library.API.DTO.PublishersDTO
{
    public class AddPublisherDTO : BasePublisherDTO
    {
        public Publisher ToPublisher()
        {
            return new Publisher
            {
                PublisherName = this.PublisherName
            };
        }
    }
}
