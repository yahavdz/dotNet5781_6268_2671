using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public static class Counts
    {
        static int stationsCount = 123456;
        public static int getStationsCount() { return ++stationsCount; }
        public static void setStationsCount(int _stationsCount) { stationsCount = _stationsCount; }



        static int busOnTripCount = 0;
        public static int getBusOnTripCount() { return ++busOnTripCount; }
        public static void setBusOnTripCount(int _busOnTripCount) { busOnTripCount = _busOnTripCount; }



        static int busLineCount = 0;
        public static int getBusLineCount() { return ++busLineCount; }
        public static void setBusLineCount(int _busLineCount) { busLineCount = _busLineCount; }

        static int tripCount = 0;
        public static int getTripCount() { return ++tripCount; }
        public static void setTripCount(int _tripCount) { tripCount = _tripCount; }



        static int licenseNumFrom2018 = 13543798;
        public static int getLicenseNumFrom2018() { return ++licenseNumFrom2018; }
        public static void setLicenseNumFrom2018(int _licenseNumFrom2018) { licenseNumFrom2018 = _licenseNumFrom2018; }



        static int licenseNumLess2018 = 9674521;
        public static int getlicenseNumLess2018() { return ++licenseNumLess2018; }
        public static void setlicenseNumLess2018(int _licenseNumLess2018) { licenseNumLess2018 = _licenseNumLess2018; }



        static int tripLine = 0;
        public static int getTripLine() { return ++tripLine; }
        public static void setTripLine(int _tripLine) { tripLine = _tripLine; }




    }
}
