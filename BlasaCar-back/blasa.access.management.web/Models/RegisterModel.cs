using blasa.access.management.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blasa.access.management.web.Models
{
    public class RegisterModel 
    {
        [Required(ErrorMessage = "last Name is required")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "first Name is required")]
        public string firstName { get; set; }
 
         
        public string Telephone { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
       
       
        public string Address { get; set; }
        //public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Sex is required")]
        public string Sex { get; set; }
    }
}
