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
        public static int maxMileageTreatment = 20000;
        public static int maxmileageFuel = 1200;

        public string busID { set; get; }
        public int mileage { set; get; }
        public int mileageTreatment { set; get; }
        public int mileageFuel { set; get; }
        public DateTime treatmentDate { set; get; }

        public void treatment()
        {
            mileageTreatment = mileage;
            treatmentDate = DateTime.Now;
        }
        public void refueling()
        {
            mileageFuel = mileage;
        }

        

    }
}
