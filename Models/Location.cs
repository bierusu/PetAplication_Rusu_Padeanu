namespace PetAplication.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string LocationName { get; set; }

        public string Picture { get; set; }
        public string Address { get; set; }
        public ICollection<Facility>? Facilities { get; set; } 

    }
}
