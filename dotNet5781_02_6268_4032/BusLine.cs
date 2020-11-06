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
            return Math.Sqrt(Math.Pow(station2.Latitude - station1.Latitude, 2) + Math.Pow(station2.Longitude - station1.Longitude, 2) * 1.0);
        }

        public double getTimeBetweenStations(BusLineStation station1, BusLineStation station2)
        {

            int key = 0;
            int i = 0;
            double timeCounter = 0;
            for (; i < Stations.Count(); ++i)
            {
                if (Stations[i].BusStationKey == station1.BusStationKey)
                {
                    key = station2.BusStationKey;
                    break;
                }
                if (Stations[i].BusStationKey == station2.BusStationKey)
                {
                    key = station1.BusStationKey;
                    break;
                }
            }
            i += 1;
            for (; Stations[i-1].BusStationKey == key; ++i)
            {
                timeCounter += Stations[i].TimeFromPreviousStation;
            }
            return timeCounter;
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
