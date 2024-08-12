namespace RealEstate.API.DTOs.Auth
{
    public class UpdateUserInfoRequestDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
