using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6268_4032
{
    class BusLineAlreadyExistsException: Exception
    {
        public BusLineAlreadyExistsException() { }
        public BusLineAlreadyExistsException(int busLine) : base(string.Format("Bus Line Already Exists ", busLine)) { }
    }
}
