using System;
using System.Collections.Generic;
using System.Text;

namespace BL.BO
{
    class Line
    {
        public IEnumerable<Station> LineList { get; set; }

        public int LineId { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
        public Areas Area { get; set; }
    }
}
