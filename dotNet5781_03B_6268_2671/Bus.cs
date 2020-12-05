//nevo cohen 207962671
//yahav davidzada 318356268
//exercise 3B
//In this exercise we created a graphical interface for handling a bus.
//We created windows to send the vehicle for refueling, treatment, and travel. And another window to insert a new bus into the system.
//The system announced the times when the bus was sent for each of the above missions.

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
