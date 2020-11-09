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

        public BusLine this[int i]
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

        // public IteratorBusLine iterator;

        public void addBus(BusLine bus)
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
                }
            }
            if (tooMany == 0)
                BusLines.Add(bus);
            //TODO exception when line allready exist
        }
        public void deleteBus(int busID)
        {
            BusLines.Remove(BusLines[busID]);
        }
        public List<BusLine> getBusLinesOfStation(BusStation busStation)
        {
            var busesToReturn = new List<BusLine>();
            foreach (BusLine b in busLines)
            {
                if (b.Stations.Contains(busStation))
                {
                    busesToReturn.Add(b);
                }
            }
            return busesToReturn;
        }
        public List<BusLine> getBusListByLengthOfRide()
        {
            var busses = BusLines;
            return busses.OrderBy(x => x.getTimeBetweenStations(x.firstStation, x.lastStation)).ToList();
        }

        public IEnumerator GetEnumerator()
        {
            return BusLines.GetEnumerator();
        }
    }
}
