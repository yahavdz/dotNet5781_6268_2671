using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6268_4032
{
    class StationAlreadyExistException:Exception
    {
        public StationAlreadyExistException() { }
        public StationAlreadyExistException(int stationKey) :base(String.Format("The Station is already exist, the key is: {0}", stationKey)) { }
    }
}
