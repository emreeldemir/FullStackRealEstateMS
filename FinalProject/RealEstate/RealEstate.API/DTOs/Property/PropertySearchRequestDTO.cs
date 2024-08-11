namespace RealEstate.API.DTOs.Property
{
    public class PropertySearchRequestDTO
    {
        public string? Status { get; set; }
        public string? Type { get; set; }
        public string? Currency { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}
