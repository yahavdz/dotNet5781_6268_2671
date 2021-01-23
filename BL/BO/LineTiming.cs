using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineTiming
    {
        public int LineNum { get; set; }
        public TimeSpan StartTime { get; set; }
        public string StationName { get; set; }
        public TimeSpan ArrivalTime { get; set; }
    }
}
