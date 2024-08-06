namespace RealEstate.API.Entities
{
    public class Type : BaseEntity
    {
        public string Name { get; set; } // Example: Villa, Arsa, Daire
        public ICollection<Property> Properties { get; set; }
    }
}
