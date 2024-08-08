using RealEstate.API.DTOs.Photo;

namespace RealEstate.API.DTOs.Property
{
    public class PropertyResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Price { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public List<PhotoResponseDTO> Photos { get; set; }
    }
}
