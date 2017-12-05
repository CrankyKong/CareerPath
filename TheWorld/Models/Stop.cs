using System;
using System.Collections.Generic;

namespace TheWorld.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Order { get; set; }
        public DateTime Arrival { get; set; }
        public bool Wishlist { get; set; }
        public double Salary { get; set; }
                
        public Organization Organization { get; set; }
    }
}