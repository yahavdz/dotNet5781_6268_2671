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
        public BusLineStation(int busStationKey1, double latitude1, double longitude1, string address1,double DistanceFromPreviousStation1, double TimeFromPreviousStation1) :base( busStationKey1,latitude1,longitude1,address1)
        {
            DistanceFromPreviousStation = DistanceFromPreviousStation1;
            TimeFromPreviousStation = TimeFromPreviousStation1;
        }
        public double DistanceFromPreviousStation { get; set; }
        public double TimeFromPreviousStation { get; set; } 

    }
}
