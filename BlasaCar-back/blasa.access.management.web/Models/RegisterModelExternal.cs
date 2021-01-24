using blasa.access.management.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace blasa.access.management.web.Models
{
    public class RegisterModelExternal
    {
        [Required(ErrorMessage = "last Name is required")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "first Name is required")]
        public string firstName { get; set; }         
        public string Telephone { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string Address { get; set; }
        //public string Email { get; set; }
        [Required(ErrorMessage = "BirthDate is required")]
        public DateTime BirthDate { get; set; }
        //[Required(ErrorMessage = "Gender is required")]
        [Required(ErrorMessage = "Email is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Gender is required")]
       
        public string Provider { get; set; }
    }
    public class LoginModelExternal
    {
        [Required(ErrorMessage = "last Name is required")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "first Name is required")]
        public string firstName { get; set; }
        public string Telephone { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string Address { get; set; }
        //public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        //[Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Provider Label is required")]

        public string Provider { get; set; }
    }
}
