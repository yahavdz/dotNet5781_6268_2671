using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6268_4032
{
    class InvalidLongitudeException : Exception
    {
        public InvalidLongitudeException() { }
        public InvalidLongitudeException(double longitude) : base(String.Format("Invalid longitude :{0}, the longitude must be between -90 to 90", longitude)) { }
    }
}
