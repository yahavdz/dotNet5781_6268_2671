using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6268_4032
{
    public enum status { readyToGo, midRide, refuelingNow, treatmentNow }
    public class Bus
    {
        public static int maxMileageTreatment = 20000;
        public static int maxMileageFuel = 1200;

        public string busID { set; get; }
        public int totalKilometers { set; get; }
        public DateTime LastTreatmentDate { set; get; }
        public int kilometersSinceLastTreatment { set; get; }
        public int KilometersAtLastRefueling { set; get; }
        public status statusNow { set; get; }
        public DateTime ActivityStartDate { set; get; }

        public void treatment()
        {
            kilometersSinceLastTreatment = totalKilometers;
            LastTreatmentDate = DateTime.Now;
        }
        public void refueling()
        {
            KilometersAtLastRefueling = totalKilometers;
        }
        public override string ToString()
        {
            string s = "License plate: " + busID + ", Total Kilometers: " + totalKilometers + ", The Last Treatment Day: " + LastTreatmentDate + ", Fuel: " + Math.Abs(KilometersAtLastRefueling - totalKilometers) + " Left";
            return s;
        }
    }
}
