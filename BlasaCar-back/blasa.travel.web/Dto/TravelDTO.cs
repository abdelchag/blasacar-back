using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Tools.Constants;

namespace blasa.travel.web.Models
{
    public class TravelDTO
    {
        public int Id{ get; set; }
        [Required(ErrorMessage = ErrorConstants.BLASACAR_Travel_DepartureCity_is_required)]
        public string DepartureCity { get; set; }
        [Required(ErrorMessage = ErrorConstants.BLASACAR_Travel_ArrivalCity_is_required)]
        public string ArrivalCity { get; set; }

        [Required(ErrorMessage = ErrorConstants.BLASACAR_Travel_DepartureDate_is_required)]
        public DateTime DepartureDate { get; set; }

        //we can use also TimeSpan as type for DepartureTime
        [Required(ErrorMessage = ErrorConstants.BLASACAR_Travel_DepartureTime_is_required)]
        public DateTime DepartureTime { get; set; }
        [Required(ErrorMessage = ErrorConstants.BLASACAR_Travel_NumberPlaces_is_required)]
        public int NumberPlaces { get; set; }
        [Required(ErrorMessage = ErrorConstants.BLASACAR_Travel_IsAutomaticAcceptance_is_required)]
        public bool IsAutomaticAcceptance { get; set; }
        [Required(ErrorMessage = ErrorConstants.BLASACAR_Travel_Price_is_required)  ]
        public decimal Price { get; set; }

        [Required(ErrorMessage = ErrorConstants.BLASACAR_Travel_PhoneNumber_is_required)]
        [Phone(ErrorMessage = ErrorConstants.BLASACAR_Travel_invalid_PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
