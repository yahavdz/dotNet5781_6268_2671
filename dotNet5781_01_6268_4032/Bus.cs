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
        public string bussID { set; get; }
        public int mileage { set; get; }
        public int mileageTreatment { set; get; }
        public int mileageFule { set; get; }
        public DateTime treatmentDate { set; get; }
        private static int maxMileageTreatment = 20000;
        private static int maxMileageFule = 1200;
    }
}
