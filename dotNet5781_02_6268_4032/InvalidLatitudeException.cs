using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6268_4032
{
    class InvalidLatitudeException : Exception
    {
        public InvalidLatitudeException() { }
        public InvalidLatitudeException(double latitude) : base(String.Format("Invalid latitude :{0}, the Latitude must be between -180 to 180", latitude)) { }
    }
}
