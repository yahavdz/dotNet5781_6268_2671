using System;

namespace DO
{
    public class Bus
    {
        public enum BusStatus { readyToGo, midRide, refuelingNow, treatmentNow }

        int LicenseNum { get; set; }
        DateTime FromDate { get; set; }
        double TotalTrip { get; set; }
        double FuelRemain { get; set; }
        BusStatus status { get; set; }


    }
}
