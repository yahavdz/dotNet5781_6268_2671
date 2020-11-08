using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6268_4032
{
    class StationIsNotExistException:Exception
    {
        public StationIsNotExistException(){}

        public StationIsNotExistException(int stationKey) : base(String.Format("The Station is not exist ,the key you entered is: {0}", stationKey)) { }


    }
}
