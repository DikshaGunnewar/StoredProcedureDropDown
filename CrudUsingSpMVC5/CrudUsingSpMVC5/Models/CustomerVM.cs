using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudUsingSpMVC5.Models
{
    public class CustomerVM
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanantAddress { get; set; }
        public int State { get; set; }
        public int City { get; set; }
    }
}