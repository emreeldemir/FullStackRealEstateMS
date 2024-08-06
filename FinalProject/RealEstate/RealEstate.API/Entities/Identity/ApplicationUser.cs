using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RealEstate.API.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Navigational properties
        public ICollection<Property> Properties { get; set; }
    }
}
