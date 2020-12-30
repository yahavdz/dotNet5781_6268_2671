using System;


namespace DO
{
    public class Bus
    {


        public int LicenseNum { get; set; }
        public DateTime FromDate { get; set; }
        public double TotalTrip { get; set; }
        public double FuelRemain { get; set; }
        public BusStatus BusStatus { get; set; }
        public double KilometersSinceLastTreatment { get; set; }
        public double KilometersAtLastRefueling { get; set; }
        public bool Active { get; set; }



    }
}
