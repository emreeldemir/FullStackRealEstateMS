namespace RealEstate.API.Entities
{
    public class Status : BaseEntity
    {
        public string Name { get; set; } // Example: Satılık, Kiralık
        public ICollection<Property> Properties { get; set; }
    }
}
