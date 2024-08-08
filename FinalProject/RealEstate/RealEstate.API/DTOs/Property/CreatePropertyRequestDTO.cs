namespace RealEstate.API.DTOs.Property
{
    public class CreatePropertyRequestDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Price { get; set; }
        public int CurrencyId { get; set; }
        public int UserId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
