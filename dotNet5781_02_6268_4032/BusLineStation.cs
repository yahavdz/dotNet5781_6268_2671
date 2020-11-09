using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6268_4032
{
    public class BusLineStation : BusStation
    {
        public BusLineStation(int busStationKey1, string address1) :base( busStationKey1,address1)
        {
        }
        public double DistanceFromPreviousStation { get; set; }
        public double TimeFromPreviousStation { get; set; } 

    }
}
