using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Tools.Constants;

namespace blasa.travel.web.Models
{
    public class BookDTO
    {
        public int Id{ get; set; }
        [Required(ErrorMessage = ErrorConstants.BLASACAR_Book_Travel_is_required)]
        public TravelDTO Travel { get; set; }
        [Required(ErrorMessage = ErrorConstants.BLASACAR_Book_Userid_is_required)]
        public string Userid { get; set; }
        public bool? IsAccepted { get; set; }
    }
}
