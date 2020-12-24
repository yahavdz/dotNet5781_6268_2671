using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    class LineStation
    {
        int LineId { get; set; }
        int Station { get; set; }
        int LineStationIndex { get; set; }
        int PrevStation { get; set; }
        int NextStation { get; set; }
    }
}
