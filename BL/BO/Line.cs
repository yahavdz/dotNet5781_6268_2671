using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.BO
{
    public class Line
    {
        public IEnumerable<Station> stations { get; set; }

        public IEnumerable<Station> GetLineList()
        {
            return from st in stations
                   select st;
        }

        public void SetLineList(List<Station> value) => stations = value;

        public int LineId { get; set; }
        public Areas Area { get; set; }

    }
}
