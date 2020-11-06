using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6268_4032
{
    public class BusStation
    {
        private int busStationKey; 
        public int BusStationKey { 
            set { 
                if (value < 0 || value > 999999)
                {
                    Console.WriteLine("ERROR - BusStationKey must be positive and with at most 6 digits");
                  
                }
                busStationKey = value;
            }
            get { return busStationKey;  }
        }
        private double latitude;
        public double Latitude { 
            set
            {
                if (value < -180 || value > 180)
                {
                    Console.WriteLine("ERROR - Latitude must a number between -180 to 180");

                }
                else
                    latitude = value;
            }
            get { return latitude;  }
        }

        private double longitude;
        public double Longitude {
            set
            {
                if (value < -90 || value > 90)
                {
                    Console.WriteLine("ERROR - Longitude must be a number between -90 to 90");

                }
                else
                    longitude = value;
            }
            get { return longitude; }
        }


        public string address { set; get; }
        //TODO


        // methods:
        public override string ToString() 
        {
            string s = "Bus Station Code: " + busStationKey.ToString() + ", " + latitude.ToString() + "°N " + longitude.ToString() + "°E";
            return s;
        }
    }
}
