using Library.API.Entities;

namespace Library.API.DTO.CustomersDTO
{
    public class CustomerInfoDTO : BaseCustomerDTO
    {
        public int Id { get; set; }

        public static CustomerInfoDTO FromCustomer(Customer customer)
        {
            return new CustomerInfoDTO
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
               
            };
        }
    }
}
