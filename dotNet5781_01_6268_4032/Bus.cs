using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6268_4032
{
    class Bus
    {
        public int busID { set; get; }
        public int km { set; get; }
        public int treatmentKm { set; get; }
        public int kmFule { set; get; }
        public DateTime treatmentDate { set; get; }

        public void treatment(int fule)
        {
            treamentKm = km;
            treatmentDate = DateTime.Now;
        }
        public void fule()
        {
            kmFule = 1200;
        }
    }
}
