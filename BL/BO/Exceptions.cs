using System;
using System.Collections.Generic;
using System.Text;

namespace BL.BO
{


    [Serializable]
    public class BadIdException : Exception
    {
        public int ID;
        public BadIdException(int id) : base() => ID = id;
        public BadIdException(int id, string message) :
            base(message) => ID = id;
        public BadIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;
        public BadIdException(DO.BadIdException ex, string message, int id) :
            base(message) => ID = id;
        public override string ToString() => base.ToString() + $", bad id: {ID}";
    }
    public class BadTimeException : Exception
    {

        public DateTime Date;
        public BadTimeException(DateTime date, string message) :
           base(message) => Date = date;
        public override string ToString() => base.ToString() + $", Wrong Time: {Date}";
    }
    public class BadTotalTripException : Exception
    {
        public double TotalTrip;
        public BadTotalTripException(double total, string message) :
       base(message) => TotalTrip = total;
        public override string ToString() => base.ToString() + $", Wrong Total trip: {TotalTrip}";
    }
    public class BadFuelException : Exception
    {
        public double Fuel;
        public BadFuelException(double fuel, string message) :
       base(message) => Fuel = fuel;
        public override string ToString() => base.ToString() + $", Wrong Fuel remain: {Fuel}";
    }
    public class BadKilometersException : Exception
    {
        public double Kilometers;
        public BadKilometersException(double kilometers, string message) :
       base(message) => Kilometers = kilometers;
        public override string ToString() => base.ToString() + $", Wrong Fuel remain: {Kilometers}";
    }

    public class BadUserNameException : Exception
    {
        public string userName;
        public BadUserNameException(string un) : base() => userName = un;
        public BadUserNameException(string un, string message) :
            base(message) => userName = un;
        public BadUserNameException(string un, string message, Exception innerException) :
            base(message, innerException) => userName = un;
        public override string ToString() => base.ToString() + $", bad user name: {userName}";
    }

    public class BadAdjacentStationsException : Exception
    {
        public int station1;
        public int station2;
        public BadAdjacentStationsException(int s1, int s2) : base() { station1 = s1; station2 = s2; }
        public BadAdjacentStationsException(int s1, int s2, string message) :
            base(message)
        { station1 = s1; station2 = s2; }
        public BadAdjacentStationsException(int s1, int s2, string message, Exception innerException) :
            base(message, innerException)
        { station1 = s1; station2 = s2; }
        public override string ToString() => base.ToString() + $", bad adjacent stations: {station1}, {station2}";
    }
    public class BadAddressException : Exception
    {
        public string address;
        public BadAddressException(string a, string message) :
       base(message) => address = a;
        public override string ToString() => base.ToString() + $", Wrong address: {address}";
    }

    public class BadLongitudeLatitudeException : Exception
    {
        public double longitudeLatitude;
        public BadLongitudeLatitudeException(double l, string message) :
       base(message) => longitudeLatitude = l;
        public override string ToString() => base.ToString() + $", Wrong cordinates: {longitudeLatitude}";
    }
}
