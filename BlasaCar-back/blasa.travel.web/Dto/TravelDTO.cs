using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace blasa.travel.web.Models
{
    public class TravelDTO
    {
        public int Id{ get; set; }
        [Required(ErrorMessage = "DepartureCity is required")]
        public string DepartureCity { get; set; }
        [Required(ErrorMessage = "ArrivalCity is required")]
        public string ArrivalCity { get; set; }

        [Required(ErrorMessage = "DepartureDate is required")]
        public DateTime DepartureDate { get; set; }

        //we can use also TimeSpan as type for DepartureTime
        [Required(ErrorMessage = "DepartureTime is required")]
        public DateTime DepartureTime { get; set; }
        [Required(ErrorMessage = "NumberPlaces is required")]
        public int NumberPlaces { get; set; }
        [Required(ErrorMessage = "IsAutomaticAcceptance is required")]
        public bool IsAutomaticAcceptance { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [Phone]
       public string PhoneNumber { get; set; }
    }
}
