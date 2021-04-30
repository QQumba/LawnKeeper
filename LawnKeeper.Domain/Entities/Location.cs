using System.Collections.Generic;
using LawnKeeper.Domain.Entities.NotMapped;

namespace LawnKeeper.Domain.Entities
{
    public class Location : BaseEntity
    {
        public int CityId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        
        public float Longitude { get; set; }
        public float Latitude { get; set; }

        //relations
        public List<Lawn> Lawns { get; set; }
    }
}