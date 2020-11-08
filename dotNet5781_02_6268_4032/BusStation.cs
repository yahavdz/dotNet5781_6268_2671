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
                    //TODO exception
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
                    //TODO exception

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
                    //TODO exception

                }
                else
                    longitude = value;
            }
            get { return longitude; }
        }

        public string address { set; get; }
        //TODO

        public BusStation(int busStationKey1, double latitude1, double longitude1, string address1)
        {
            BusStationKey = busStationKey1;
            Latitude = latitude1;
            Longitude = longitude1;
            address = address1;
        }


        // methods:
        public override string ToString() 
        {
            string s = "Bus Station Code: " + busStationKey.ToString() + ", " + latitude.ToString() + "°N " + longitude.ToString() + "°E";
            return s;
        }
    }
}
