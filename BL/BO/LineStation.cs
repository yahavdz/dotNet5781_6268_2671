using System;
using System.Collections.Generic;
using System.Text;

namespace BL.BO
{
    class LineStation : Station
    {
        public int LineId { get; set; }
        public int DistanceToNextStation { get; set; }
        public TimeSpan TimeFromPrevStation { get; set; }
    }
}
