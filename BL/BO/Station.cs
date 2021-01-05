using System;
using System.Collections.Generic;
using System.Text;

namespace BL.BO
{
    public class Station
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Accessibility { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool Active { get; set; }
    }
}
