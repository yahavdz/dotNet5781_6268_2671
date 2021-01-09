using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class BusStation : Station
    {
        public IEnumerable<Line> LinesList { get; set; }

    }
}
