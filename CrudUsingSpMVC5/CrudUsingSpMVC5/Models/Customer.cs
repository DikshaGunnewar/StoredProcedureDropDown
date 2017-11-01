using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudUsingSpMVC5.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Enter Your Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Your Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Your Address")]
        public int AddressId { get; set; }
      

    }
}