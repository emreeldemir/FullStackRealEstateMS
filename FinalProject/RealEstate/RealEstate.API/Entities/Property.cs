using RealEstate.API.Entities.Identity;

namespace RealEstate.API.Entities
{
    public class Property : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Price { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
