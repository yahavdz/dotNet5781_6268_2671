﻿using System;
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
        List<BusLineStation> Stations { get; set; }
        int busLine { get; set; }
        BusLineStation FirstStation { get; set; }
        BusLineStation LastStation { get; set; }
        Area BusArea { get; set; }

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
            //נוסחאת מרחק בין 2 נקודות
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
