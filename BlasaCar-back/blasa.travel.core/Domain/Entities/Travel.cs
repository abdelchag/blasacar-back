using blasa.travel.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace blasa.travel.Core.Domain.Entities
{
   public class Travel : AuditableBaseEntity
    {

        public string DepartureCity { get; set; }

        public string ArrivalCity { get; set; }
        public DateTime DateOfDeparture { get; set; }

        //we can use also TimeSpan as type for DepartureTime
        public DateTime DepartureTime { get; set; }
        public int NumberOfPlaces { get; set; }
        public bool IsAutomaticAcceptance { get; set; }
        public decimal Price { get; set; }
        public string PhoneNumber { get; set; }

    
    }
}
