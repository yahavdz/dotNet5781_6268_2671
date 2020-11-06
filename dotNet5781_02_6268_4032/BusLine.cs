using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_02_6268_4032
{
    public class BusLine : IComparable<BusLine>
    {
        public List<BusLineStation> Stations { get; set; }
        public int busLine { get; set; }
        public BusLineStation FirstStation { get; set; }
        public BusLineStation LastStation { get; set; }
        public Area BusArea { get; set; }

        // methods:

        public override string ToString()
        {
            //TODO
            return "";
        }

        public void addStation(BusLineStation station)
        {
           
            //TODO
        }

        public void removeStation(BusLineStation station)//remove the station from the list
        {
            Stations.Remove(station);
        }

        public bool isStationExist(BusLineStation station)
        {
            return Stations.Any(x => x.BusStationKey == station.BusStationKey);
        }

        public double getDistanceBetweenStations(BusLineStation station1, BusLineStation station2)
        {
            (Math.Pow(station1.Latitude - station2.Latitude, 2) + Math.Pow(station1.Longitude - station2.Longitude, 2)) < (d * d);
        }

        public double getTimeBetweenStations(BusLineStation station1, BusLineStation station2)
        {
            //TODO
        }

        public BusLine getSubLineBetweenStations(BusLineStation station1, BusLineStation station2)
        {
            //TODO
        }

        public int CompareTo(BusLine busLine)
        {
            if (busLine == null) return 1;
            // TODO
        }


    }

    // TODO
    public enum Area { }


}
