using Library.API.Context;
using Library.API.DTO.PublishersDTO;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly LibraryContext context;

        public PublisherController(LibraryContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddPublisherDTO publisher)
        {
            var response = context.Publishers.Add(publisher.ToPublisher());
            context.SaveChanges();

            return Ok(PublisherInfoDTO.FromPublisher(response.Entity));
        }


        [HttpPut]
        public IActionResult Put([FromBody] EditPublisherDTO editPublisherDTO)
        {
            int id = editPublisherDTO.Id;
            string name = editPublisherDTO.PublisherName;

            var publisher = context.Publishers.FirstOrDefault(x => x.Id == editPublisherDTO.Id);
            if (publisher == null) return NotFound();

            publisher.PublisherName = name;
            context.SaveChanges();
            
            return Ok(PublisherInfoDTO.FromPublisher(publisher));
            

        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var publisher = context.Publishers.FirstOrDefault(x => x.Id == id);
            if (publisher == null) return NotFound();

            return Ok(PublisherInfoDTO.FromPublisher(publisher));
        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var publisher = context.Publishers.FirstOrDefault(x => x.Id == id);
            if (publisher == null) return NotFound();
            publisher.IsDeleted = true;

            context.SaveChanges();

            return NoContent();
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAll()
        {
            var publisher = context.Publishers.Where(x => x.IsDeleted == false).ToList();
            if (publisher == null) return NotFound();

            List<PublisherInfoDTO> publisherInfoDTOs = new List<PublisherInfoDTO>();
            publisher.ForEach(x => publisherInfoDTOs.Add(PublisherInfoDTO.FromPublisher(x)));

            return Ok(publisherInfoDTOs);

        }

    }
}
