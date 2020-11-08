using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6268_4032
{
    public class BusCollection:IEnumerable
    {
        public List<BusLine> BusLines { set; get; }

        public IteratorBusLine Current;

        public void addBus(BusLine bus)
        {
            
        }
        public void deleteBus(int busID)
        {

        }
        public List<BusLine> getBusLinesOfStation(BusStation busStation)
        {
            
        }
        public List<BusLine> getBusListByLengthOfRide(BusCollection busCollection)
        {

        }

        public IEnumerator GetEnumerator()
        {
            return Current;
        }

        public BusLine indexer(int value)
        {
           return this.BusLines.FirstOrDefault(x => x.busLine == value);

        }

       
    }
    public class IteratorBusLine : IEnumerator
    {
        public BusCollection _busCollection { set; get; }
        public int counter=-1;

        public IteratorBusLine(BusCollection busCollection)
            {
            _busCollection = busCollection;
        }
        public Object Current { get {
                return _busCollection.BusLines[counter];
            } }

        public bool MoveNext()
        {
            return ++counter < _busCollection.BusLines.Count;
        }

        public void Reset()
        {
            counter = -1;
        }
    }
}
