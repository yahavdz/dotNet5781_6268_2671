using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    class Trip
    {
        int Id { get; set; }
        string UserName { get; set; }
        int LineId { get; set; }
        int InStation { get; set; }
        TimeSpan InAt { get; set; }
        int outStation { get; set; }
        TimeSpan OutAt { get; set; }
    }
}
