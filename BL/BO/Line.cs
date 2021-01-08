using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Line
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public Areas Area { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
        public bool Active { get; set; }


        //public IEnumerable<Station> stations { get; set; }
        //public IEnumerable<Station> GetLineList()
        //{
        //    return from st in stations
        //           select st;
        //}
        //public void SetLineList(List<Station> value) => stations = value;

    }
}
