namespace RealEstate.API.Models
{
    public class ApiUsers
    {
        public static List<ApiUser> Users = new()
        {
            new ApiUser { Id = 1, UserName = "emre", Password = "emre123456", Role = "User" },
            new ApiUser { Id = 2, UserName = "deniz", Password = "deniz789", Role = "Admin" }
        };
        
    }
}
