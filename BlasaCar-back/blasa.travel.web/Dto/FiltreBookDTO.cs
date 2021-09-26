using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blasa.travel.web.Models
{
    public class FiltreBookDTO
    {
        public TravelDTO travel { get; set; }
        public string Userid { get; set; }
        public bool ? IsAccepted { get; set; }


    }
}
