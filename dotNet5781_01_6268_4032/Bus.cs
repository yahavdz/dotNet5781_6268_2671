using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6268_4032
{
    class Buss
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
        public int bussID { set; get; }
        public int km { set; get; }
        public int kmHandler { set; get; }
        public int kmFule { set; get; }
        public DateTime date { set; get; }
        private static int maxKmHandler = 20000;
        private static int maxKmFule = 1200;
    }
}
