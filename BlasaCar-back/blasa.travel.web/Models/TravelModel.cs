﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blasa.travel.web.Models
{
    public class TravelModel
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
