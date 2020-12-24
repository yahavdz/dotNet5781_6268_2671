using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    class LineTrip
    {
        int Id { get; set; }
        int LineId { get; set; }
        TimeSpan StartAt { get; set; }
        TimeSpan Frequency { get; set; }
        TimeSpan FinishAt { get; set; }

    }
}
