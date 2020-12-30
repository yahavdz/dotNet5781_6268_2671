using System;
using System.Collections.Generic;
using System.Text;

namespace BL.BO
{
    class BusStation : Station
    {
        public IEnumerable<Line> LinesList { get; set; }

    }
}
