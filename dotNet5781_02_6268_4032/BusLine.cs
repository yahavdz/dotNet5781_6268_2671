using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_02_6268_4032
{
    public class BusLine : IComparable<BusLine>
    {
        public List<BusLineStation> Stations { get; set; }
        public int busLine { get; set; }
        public BusLineStation firstStation;
        public BusLineStation lastStation;
        public Area BusArea { get; set; }

        // methods:
        public BusLine() { }
        public BusLine(List<BusLineStation> Stations1, int busLine1, BusLineStation firstStation1, BusLineStation lastStation1, Area BusArea1)
        {
            Stations = new List<BusLineStation>();
            foreach (BusLineStation currentBus in Stations1)
            {
                Stations.Add(currentBus);
            }
            busLine = busLine1;
            firstStation = firstStation1;
            lastStation = lastStation1;
            BusArea = BusArea1;
        }
        public override string ToString()
        {
            string busLIneStations = "";
            foreach (BusLineStation currentLine in Stations)
            {
                busLIneStations += currentLine.BusStationKey.ToString();
                busLIneStations += " , ";
            }
            return "The number of the Line is: " + busLine + " , The Area of the Line is: " + BusArea + " , The bus station key is: " + busLIneStations;
        }

        public void addStation(BusLineStation station, int index)
        {
            if (index < 0 || index > Stations.Count())
                throw new InvalidIndexException(index);
            if (isStationExist(station))
                throw new StationAlreadyExistException(station.BusStationKey);
            if (index == Stations.Count())
                lastStation = station;
            if (index == 0)
                firstStation = station;
            Stations.Insert(index, station);

        }

        public void removeStation(BusLineStation station)//remove the station from the list
        {
            if (station.BusStationKey == firstStation.BusStationKey)
                firstStation = Stations[1];
            if (station.BusStationKey == lastStation.BusStationKey)
                lastStation = Stations[Stations.Count() - 2];
            if (!isStationExist(station))
            {
                throw new StationIsNotExistException(station.BusStationKey);
            }
            Stations.Remove(station);
        }

        public bool isStationExist(BusLineStation station)
        {
            return Stations.Any(x => x.BusStationKey == station.BusStationKey);
        }

        public double getDistanceBetweenStations(BusLineStation station1, BusLineStation station2)
        {
            //return Math.Sqrt(Math.Pow(station2.Latitude - station1.Latitude, 2) + Math.Pow(station2.Longitude - station1.Longitude, 2) * 1.0);

            if (!isStationExist(station1))
            {
                throw new StationIsNotExistException(station1.BusStationKey);
            }
            if (!isStationExist(station2))
            {
                throw new StationIsNotExistException(station2.BusStationKey);
            }
            int key = 0;
            int i = 0;
            double distanceCounter = 0;
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
            for (; Stations[i - 1].BusStationKey == key; ++i)
            {
                distanceCounter += Stations[i].TimeFromPreviousStation;
            }
            return distanceCounter;
        }


        public double getTimeBetweenStations(BusLineStation station1, BusLineStation station2)
        {
            if (!isStationExist(station1))
            {
                throw new StationIsNotExistException(station1.BusStationKey);
            }
            if (!isStationExist(station2))
            {
                throw new StationIsNotExistException(station2.BusStationKey);
            }
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
            for (; Stations[i - 1].BusStationKey == key; ++i)
            {
                timeCounter += Stations[i].TimeFromPreviousStation;
            }
            return timeCounter;
        }

        public BusLine getSubLineBetweenStations(BusLineStation station1, BusLineStation station2)
        {
            if (!isStationExist(station1))
            {
                throw new StationIsNotExistException(station1.BusStationKey);
            }
            if (!isStationExist(station2))
            {
                throw new StationIsNotExistException(station2.BusStationKey);
            }
            BusLine subBus = new BusLine();
            int key = 0;
            int i = 0;
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
            for (; Stations[i - 1].BusStationKey == key; ++i)
            {
                subBus.Stations.Add(Stations[i]);
            }
            return subBus;
        }

        public int CompareTo(BusLine busLine)
        {
            double timeCounter1 = 0;
            double timeCounter2 = 0;
            foreach (BusLineStation currentLine in Stations)
            {
                timeCounter1 += currentLine.TimeFromPreviousStation;
            }
            foreach (BusLineStation currentLine in busLine.Stations)
            {
                timeCounter2 += currentLine.TimeFromPreviousStation;
            }
            return timeCounter1.CompareTo(timeCounter2);
        }
    }
    public enum Area
    {
        General,
        North,
        South,
        Center,
        Jerusalem

    }


}
