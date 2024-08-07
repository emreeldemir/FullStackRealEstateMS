using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RealEstate.API.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        // Additional properties
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}
