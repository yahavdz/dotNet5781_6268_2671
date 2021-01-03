using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.BO
{
    public class Line
    {
        public IEnumerable<Station> lineList { get; set; }

        //public IEnumerable<Station> GetLineList()
        //{
        //    return from st in lineList
        //           select st;
        //}

        //public void SetLineList(List<Station> value) => lineList = value;

        public int LineId { get; set; }
        public Areas Area { get; set; }
       
    }
}
