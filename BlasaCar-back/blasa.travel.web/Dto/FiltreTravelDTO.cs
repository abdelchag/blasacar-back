using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blasa.travel.web.Models
{
    public class FiltreTravelDTO
    {
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }

        public DateTime? DepartureDate { get; set; }

        public DateTime? DepartureTime { get; set; }
        public int? NumberPlaces { get; set; }
        public bool? IsAutomaticAcceptance { get; set; }
        public decimal? Price { get; set; }
        public string PhoneNumber { get; set; }

    }
}
