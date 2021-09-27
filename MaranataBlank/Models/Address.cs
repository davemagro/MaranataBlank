using System;
using System.Collections.Generic;
using System.Text;

namespace MaranataBlank.Models
{
    class AddressInfo
    {
        public string Address { get; set; }
        
        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}
