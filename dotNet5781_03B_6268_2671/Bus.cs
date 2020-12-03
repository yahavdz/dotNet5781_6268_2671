using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_6268_2671
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
            statusNow = status.treatmentNow;
            kilometersSinceLastTreatment = totalKilometers;
            LastTreatmentDate = DateTime.Today;
        }
        public void refueling()
        {
            statusNow = status.refuelingNow;
            KilometersAtLastRefueling = totalKilometers;
        }
        public override string ToString()
        {
            string s = "License plate: " + busID + ", Total Kilometers: " + totalKilometers + ", The Last Treatment Day: " + LastTreatmentDate + ", Fuel: " + Math.Abs(KilometersAtLastRefueling - totalKilometers) + " Left";
            return s;
        }
    }
}
