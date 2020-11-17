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
        public BusLineStation(int busStationKey1, string address1) :base( busStationKey1,address1)//constructor heir from BusStation
        {
        }
        public double DistanceFromPreviousStation { get; set; }
        public double TimeFromPreviousStation { get; set; }

        // methods:
        public override string ToString()
        {
            string Time="";
            if (TimeFromPreviousStation>60)
            {
               double min = TimeFromPreviousStation % 60;
                double hour = Math.Floor(TimeFromPreviousStation / 60);
                if(min<10)
                {
                    Time = hour + ":0" + min + ":00";
                }
                else
                    Time = hour + ":" + min + ":00";
                string s = " Addres: " + address + ",  time from previous stations: " + Time + ",  Code: " + BusStationKey.ToString() + "\n          Location:  " + Latitude.ToString() + "°N , " + Longitude.ToString() + "°E";
                return s;
            }
            else
            {
                string s = " Addres: " + address + ",  time from previous stations: " + TimeFromPreviousStation + " minute,  Code: " + BusStationKey.ToString() + "\n          Location:  " + Latitude.ToString() + "°N , " + Longitude.ToString() + "°E";
                return s;
            }
            
        }

    }
}
