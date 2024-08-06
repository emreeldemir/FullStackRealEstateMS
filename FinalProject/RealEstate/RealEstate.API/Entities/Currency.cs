namespace RealEstate.API.Entities
{
    public class Currency : BaseEntity
    {
        public string Name { get; set; } // Example: USD, EUR, TL
        public ICollection<Property> Properties { get; set; }
    }
}
