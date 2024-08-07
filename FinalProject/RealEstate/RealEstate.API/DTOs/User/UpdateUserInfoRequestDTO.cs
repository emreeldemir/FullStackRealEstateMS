namespace RealEstate.API.DTOs.User
{
    public class UpdateUserInfoRequestDTO
    {
        public string UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? NewPassword { get; set; }
    }
}
