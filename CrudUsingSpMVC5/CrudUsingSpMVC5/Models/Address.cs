using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudUsingSpMVC5.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanantAddress { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }

    }
}