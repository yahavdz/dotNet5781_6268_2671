using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class LineStation : Station
    {
        public int LineId { get; set; }
        public double DistanceToNextStation { get; set; }
        public TimeSpan TimeFromPrevStation { get; set; }
    }
}
