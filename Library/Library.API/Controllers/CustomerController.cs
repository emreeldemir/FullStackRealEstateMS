using Library.API.Context;
using Library.API.DTO.CustomersDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly LibraryContext context;

        public CustomerController(LibraryContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddCustomerDTO customer)
        {
            var response = context.Customers.Add(customer.ToCustomer());
            context.SaveChanges();

            return Ok(CustomerInfoDTO.FromCustomer(response.Entity));
        }

        [HttpPut]
        public IActionResult Put(EditCustomerDTO customerDTO)
        {
            int id = customerDTO.Id;
            string fname = customerDTO.FirstName;
            string lname = customerDTO.LastName;
            string mail = customerDTO.Email;

            var customer = context.Customers.FirstOrDefault(x => x.Id == id);
            if (customer is null) { return NotFound(); }
            customer.FirstName = fname;
            customer.LastName = lname;
            customer.Email = mail;

            context.SaveChanges();
            return Ok(CustomerInfoDTO.FromCustomer(customer));
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var customer = context.Customers.FirstOrDefault(x => x.Id == id && !x.isDeleted);
            if (customer == null) return NotFound();

            return Ok(CustomerInfoDTO.FromCustomer(customer));


        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var customer = context.Customers.FirstOrDefault(x => x.Id == id);
            if (customer == null) return NotFound();
            customer.isDeleted = true;

            context.SaveChanges();

            return NoContent();
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetAll()
        {

            var customer = context.Customers.Where(x => !x.isDeleted).ToList();
            if (customer == null) return NotFound();

            List<CustomerInfoDTO> listDTO = new List<CustomerInfoDTO>();

            customer.ForEach(x => listDTO.Add(CustomerInfoDTO.FromCustomer(x)));

            return Ok(listDTO);

        }


    }
}
