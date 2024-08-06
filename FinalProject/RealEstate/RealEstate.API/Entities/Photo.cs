﻿namespace RealEstate.API.Entities
{
    public class Photo : BaseEntity
    {
        public int PropertyId { get; set; }
        public Property Property { get; set; }
        public string PhotoData { get; set; } // Base64 formatında fotoğraf verisi
    }
}
