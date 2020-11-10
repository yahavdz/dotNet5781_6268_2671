using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6268_4032
{

    public class BusCollection : IEnumerable
    {
        public BusCollection()
        {
            busLines = new List<BusLine>();
        }

        private List<BusLine> busLines;
        public List<BusLine> BusLines
        {
            set
            {
                busLines = value;
            }
            get { return busLines; }
        }

        public BusLine this[int i]//Indexer that receives a line number and returns the instance. If there is no such line an exception will be thrown.
        {
            get { return this.BusLines.FirstOrDefault(x => x.busLine == i); }
            set
            {
                foreach (BusLine b in BusLines)
                {
                    if (b.busLine == i)
                    {
                        BusLines.Remove(b);
                        BusLines.Add(value);
                    }
                }
            }
        }
        public void addBus(BusLine bus)//add bus to collection
        {
            var tooMany = 0;
            foreach (BusLine b in busLines)
            {
                if (b.busLine == bus.busLine)
                {
                    tooMany++;
                    if (b.firstStation == bus.lastStation && b.lastStation == bus.firstStation)
                    {
                        if (tooMany == 1)
                            BusLines.Add(bus);
                    }
                }
            }
            foreach (BusLine b in busLines)
            {
                if (b.busLine == bus.busLine)
                {
                    tooMany++;
                    if (b.firstStation == bus.lastStation && b.lastStation == bus.firstStation)
                    {
                        if (tooMany == 1)
                            BusLines.Add(bus);
                    }
                    else if (b.firstStation == bus.firstStation && b.lastStation == bus.lastStation)
                    {
                        throw new BusLineAlreadyExistsException(bus.busLine);
                    }
                }
            }
            if (tooMany == 0)
            { 
                BusLines.Add(bus);
            }

            
        }
        public void deleteBus(int busID)//delete bus fron collection
        {
            BusLines.Remove(BusLines[busID]);
        }
        public BusCollection getBusLinesOfStation(int busStation)
        {
            var busesToReturn = new List<BusLine>();
            foreach (BusLine b in busLines)
            {
                if ((b.Stations.FirstOrDefault(x => x.BusStationKey == busStation))!=null)
                {
                    busesToReturn.Add(b);
                }
            }

            return new BusCollection { busLines = busesToReturn };
        }
        public List<BusLine> getBusListByLengthOfRide()//A method that returns a list of all lines sorted by total travel time, from the shortest To lengthen
        {
            var busses = BusLines;
            return busses.OrderBy(x => x.getTimeBetweenStations(x.firstStation, x.lastStation)).ToList();
        }

        public IEnumerator GetEnumerator()
        {
            return BusLines.GetEnumerator();
        }

        public override string ToString()
        {
            string busCollection1 = "";
            foreach (BusLine busLine1 in BusLines)
            {
                busCollection1 += busLine1.busLine.ToString();
                busCollection1 += " , ";
            }
            return "The number of Lines is: " + busCollection1;
        }

    }
}
