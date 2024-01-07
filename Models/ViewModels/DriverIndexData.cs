using System.Security.Policy;
using PetAplication.Models;


namespace PetAplication.Models.ViewModels
{
    public class LocationIndexData
    {
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Facility> Facilities { get; set; }
    }
}
