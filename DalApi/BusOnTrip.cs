using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    class BusOnTrip
    {
        int Id { get; set; }
        int LicenseNum { get; set; }
        int LineId { get; set; }
        TimeSpan PlannedTakeOff { get; set; }
        TimeSpan ActualTakeOff { get; set; }
        int PrevStation { get; set; }
        TimeSpan PrevStationAt { get; set; }
        TimeSpan NextStationAt { get; set; }



    }
}
