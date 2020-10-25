using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6268_4032
{
    class Bus
    {
        public static int maxKmHandler = 20000;
        public static int maxKmFule = 1200;
        public string busID { set; get; }
        public int km { set; get; }
        public int treatmentKm { set; get; }
        public int kmFule { set; get; }
        public DateTime treatmentDate { set; get; }

        public void treatment()
        {
            treatmentKm = km;
            treatmentDate = DateTime.Now;
        }
        public void refueling()
        {
            kmFule = maxKmFule;
        }

    }
}
