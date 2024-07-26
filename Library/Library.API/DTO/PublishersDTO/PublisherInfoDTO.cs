using Library.API.Entities;

namespace Library.API.DTO.PublishersDTO
{
    public class PublisherInfoDTO : BasePublisherDTO
    {
        public int Id { get; set; }

        public static PublisherInfoDTO FromPublisher(Publisher publisher)
        {
            return new PublisherInfoDTO
            {
                Id = publisher.Id,
                PublisherName = publisher.PublisherName,
               
            };
        }
    }
}
