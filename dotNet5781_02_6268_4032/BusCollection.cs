﻿using System;
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
        public List<BusLine> BusLines { set; get; }
        public BusLine this[int i]
        {
            get { return this.BusLines.FirstOrDefault(x => x.busLine == i); }
            set { 
                foreach (BusLine b in BusLines)
                {
                    if(b.busLine == i)
                    {
                        BusLines.Remove(b);
                        BusLines.Add(value);
                    }
                }
            }
        }

        public IteratorBusLine iterator;

        public void addBus(BusLine bus)
        {
            var tooMany = 0;
            while (iterator.MoveNext())
            {
                if (iterator.Current.busLine == bus.busLine)
                {
                    tooMany++;
                    if (iterator.Current.firstStation==bus.lastStation&&iterator.Current.lastStation==bus.firstStation)
                    {
                        if(tooMany==1)
                        BusLines.Add(bus);
                    }
                }
            }
            if (tooMany == 0)
                BusLines.Add(bus);
            //TODO exception when line lready exist
        }
        public void deleteBus(int busID)
        {
            BusLines.Remove(BusLines[busID]);
        }
        public List<BusLine> getBusLinesOfStation(BusStation busStation)
        {
            var busesToReturn = new List<BusLine>();
            while (iterator.MoveNext())
            {
                if (iterator.Current.Stations.Contains(busStation))
                {
                    busesToReturn.Add(iterator.Current);
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
            return iterator;
        }        
    }
    public class IteratorBusLine : IEnumerator<BusLine>
    {
        public BusCollection _busCollection { set; get; }
        public int counter = -1;
        public BusLine Current
        {
            get
            {
                return _busCollection.BusLines[counter];
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public IteratorBusLine(BusCollection busCollection)
        {
            _busCollection = busCollection;
        }

        public bool MoveNext()
        {
            return ++counter < _busCollection.BusLines.Count;
        }

        public void Reset()
        {
            counter = -1;
        }

        public void Dispose()
        { }
    }
}
