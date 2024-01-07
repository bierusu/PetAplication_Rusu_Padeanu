using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace PetAplication.Models
{
    public class Request
    {
        public int ID { get; set; }
        public int? ClientID { get; set; }
        public Client? Client { get; set; }
        public int? FacilityID { get; set; }
        public Facility? Facility { get; set; }

        public int? PetID { get; set; } 
        public Pet? Pet { get; set; }  

        [DataType(DataType.Date)]
        [Display(Name = "Check In")]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check Out")]
        public DateTime CheckOut { get; set; }

        [Display(Name = "Total Price")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }


    }

}
