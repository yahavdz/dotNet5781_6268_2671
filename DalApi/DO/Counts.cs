using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    public static class Counts
    {
        static int stationsCount = 123456;
        public static int getStationsCount() { return ++stationsCount; }


        static int busOnTripCount = 0;
        public static int getBusOnTripCount() { return ++busOnTripCount; }


        static int busLineCount = 0;
        public static int getBusLineCount() { return ++busLineCount; }


        static int tripCount = 0;
        public static int getTripCount() { return ++tripCount; }


        static int licenseNumFrom2018 = 13543798;
        public static int getLicenseNumFrom2018() { return ++licenseNumFrom2018; }


        static int licenseNumLess2018 = 9674521;
        public static int getlicenseNumLess2018() { return ++licenseNumLess2018; }


        static int tripLine = 0;
        public static int getTripLine() { return ++tripLine; }



    }
}
