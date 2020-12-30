﻿using System;
using System.Collections.Generic;
using System.Windows.Media;
using DO;

namespace DS
{
    public static class DataSource
    {
        public static List<Bus> ListBuses;
        public static List<BusOnTrip> ListBusesOnTrip;
        public static List<Line> ListLines;
        public static List<LineStation> ListLineStations;
        public static List<LineTrip> ListLineTrips;
        public static List<Station> ListStations;
        public static List<Trip> ListTrips;
        public static List<User> ListUsers;
        public static List<AdjacentStations> ListAdjacentStations;

        static DataSource()
        {
            InitAllLists();
        }

        static void InitAllLists()
        {

            Random random = new Random();
            ListStations = new List<Station>()
            {

                new Station//1
                {
                    Code = Counts.getStationsCount(),
                    Name= "Tel-Aviv 122",
                    Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                    Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                    Address = " Dolev st. - Habrosh st.",
                    Accessibility= true
                },
                new Station//2
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Tel-Aviv 13",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Haermon st. - Hayarden st.",
                      Accessibility = true
                  },
                new Station//3
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Tel-Aviv 172",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Haermon st. - Hamor st.",
                      Accessibility = false
                  },
                new Station//4
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Tel-Aviv 72",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Hamor st. - Hayarden st.",
                      Accessibility = false
                  },
                new Station//5
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Tel-Aviv 73",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Yaalom st. - Hayarden st.",
                      Accessibility = false
                  },
                new Station//6
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Tel-Aviv 134",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Carmel st. - Yaalom st.",
                      Accessibility = true
                  },
                new Station//7
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Tel-Aviv 14",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Hagoren st. - Carmel st.",
                      Accessibility = true
                  },
                new Station//8
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Tel-Aviv 125",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Hashita st. - Hagoren st.",
                      Accessibility = false
                  },
                new Station//9
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Tel-Aviv 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Lakish st. - Hashita st.",
                      Accessibility = false
                  },
                new Station//10
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Tel-Aviv 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Haharouv st. - Lakish st.",
                      Accessibility = false
                  },
                new Station//11
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Haifa 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Dolev st. - Habrosh st.",
                      Accessibility = false
                  },
                new Station//12
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Haifa 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Haermon st. - Hayarden st.",
                      Accessibility = true
                  },
                new Station//13
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Haifa 122",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Haermon st. - Hamor st.",
                      Accessibility = true
                  },
                new Station//14
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Haifa 72",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Hamor st. - Hayarden st.",
                      Accessibility = false
                  },
                new Station//15
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Haifa 73",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Yaalom st. - Hayarden st.",
                      Accessibility = false
                  },
                new Station//16
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Haifa 134",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Carmel st. - Yaalom st.",
                      Accessibility = true
                  },
                new Station//17
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Haifa 14",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Hagoren st. - Carmel st.",
                      Accessibility = true
                  },
                new Station//18
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Haifa 125",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Hashita st. - Hagoren st.",
                      Accessibility = false
                  },
                new Station//19
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Haifa 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Lakish st. - Hashita st.",
                      Accessibility = false
                  },
                new Station//20
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Haifa 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Haharouv st. - Lakish st.",
                      Accessibility = false
                  },
                new Station//21
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Ashkelon 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Dolev st. - Habrosh st.",
                      Accessibility = false
                  },
                new Station//22
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Ashkelon 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Haermon st. - Hayarden st.",
                      Accessibility = true
                  },
                new Station//23
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Ashkelon 122",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Haermon st. - Hamor st.",
                      Accessibility = true
                  },
                new Station//24
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Ashkelon 72",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Hamor st. - Hayarden st.",
                      Accessibility = false
                  },
                new Station//25
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Ashkelon 73",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Yaalom st. - Hayarden st.",
                      Accessibility = false
                  },
                new Station//26
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Ashkelon 134",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Carmel st. - Yaalom st.",
                      Accessibility = true
                  },
                new Station//27
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Ashkelon 14",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Hagoren st. - Carmel st.",
                      Accessibility = true
                  },
                new Station//28
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Ashkelon 125",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Hashita st. - Hagoren st.",
                      Accessibility = false
                  },
                new Station//29
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Ashkelon 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Lakish st. - Hashita st.",
                      Accessibility = false
                  },
                new Station//30
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Ashkelon 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Haharouv st. - Lakish st.",
                      Accessibility = false
                  },
                new Station//31
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Moodiin 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Dolev st. - Habrosh st.",
                      Accessibility = false
                  },
                new Station//32
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Moodiin 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Haermon st. - Hayarden st.",
                      Accessibility = true
                  },
                new Station//33
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Moodiin 122",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Haermon st. - Hamor st.",
                      Accessibility = true
                  },
                new Station//34
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Moodiin 72",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Hamor st. - Hayarden st.",
                      Accessibility = false
                  },
                new Station//35
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Moodiin 73",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Yaalom st. - Hayarden st.",
                      Accessibility = false
                  },
                new Station//36
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Moodiin 134",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Carmel st. - Yaalom st.",
                      Accessibility = true
                  },
                new Station//37
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Moodiin 14",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Hagoren st. - Carmel st.",
                      Accessibility = true
                  },
                new Station//38
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Moodiin 125",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Hashita st. - Hagoren st.",
                      Accessibility = false
                  },
                new Station//39
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Moodiin 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Lakish st. - Hashita st.",
                      Accessibility = false
                  },
                new Station//40
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Moodiin 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Haharouv st. - Lakish st.",
                      Accessibility = false
                  },
                new Station//41
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Beit-Shemesh 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Dolev st. - Habrosh st.",
                      Accessibility = false
                  },
                new Station//42
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Beit-Shemes 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Haermon st. - Hayarden st.",
                      Accessibility = true
                  },
                new Station//43
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Beit-Shemes 122",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Haermon st. - Hamor st.",
                      Accessibility = true
                  },
                new Station//44
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Beit-Shemes 72",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Hamor st. - Hayarden st.",
                      Accessibility = false
                  },
                new Station//45
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Beit-Shemes 73",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Yaalom st. - Hayarden st.",
                      Accessibility = false
                  },
                new Station//46
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Beit-Shemes 134",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Carmel st. - Yaalom st.",
                      Accessibility = true
                  },
                new Station//47
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Beit-Shemes 14",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Hagoren st. - Carmel st.",
                      Accessibility = true
                  },
                new Station//48
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Beit-Shemes 125",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Hashita st. - Hagoren st.",
                      Accessibility = false
                  },
                new Station//49
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Beit-Shemes 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Lakish st. - Hashita st.",
                      Accessibility = false
                  },
                new Station//50
                  {
                      Code = Counts.getStationsCount(),
                      Name = "Beit-Shemes 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Address = " Haharouv st. - Lakish st.",
                      Accessibility = false
                  }
            };

            ListLines = new List<Line>()
            {
                 new Line//1
                 {
                     Id= Counts.getBusLineCount(),
                     Code=1,
                     Area =Areas.Center ,
                     FirstStation=ListStations[0].Code,
                     LastStation=ListStations[9].Code,
                 },
                 new Line//2
                 {
                     Id= Counts.getBusLineCount(),
                     Code=2,
                     Area =Areas.North ,
                     FirstStation=ListStations[10].Code,
                     LastStation=ListStations[19].Code,
                 },
                 new Line//3
                 {
                     Id= Counts.getBusLineCount(),
                     Code=3,
                     Area =Areas.South ,
                     FirstStation=ListStations[20].Code,
                     LastStation=ListStations[29].Code,
                 },
                 new Line//4
                 {
                     Id= Counts.getBusLineCount(),
                     Code=4,
                     Area =Areas.Center ,
                     FirstStation=ListStations[30].Code,
                     LastStation=ListStations[39].Code,
                 },
                 new Line//5
                 {
                     Id= Counts.getBusLineCount(),
                     Code=5,
                     Area =Areas.Jerusalem ,
                     FirstStation=ListStations[40].Code,
                     LastStation=ListStations[49].Code,
                 },
                 new Line//6
                 {
                     Id= Counts.getBusLineCount(),
                     Code=6,
                     Area =Areas.General ,
                     FirstStation=ListStations[5].Code,
                     LastStation=ListStations[15].Code,
                 },
                 new Line//7
                 {
                     Id= Counts.getBusLineCount(),
                     Code=7,
                     Area =Areas.General ,
                     FirstStation=ListStations[15].Code,
                     LastStation=ListStations[25].Code,
                 },
                 new Line//8
                 {
                     Id= Counts.getBusLineCount(),
                     Code=8,
                     Area =Areas.South ,
                     FirstStation=ListStations[25].Code,
                     LastStation=ListStations[35].Code,
                 },
                 new Line//9
                 {
                     Id= Counts.getBusLineCount(),
                     Code=9,
                     Area =Areas.General ,
                     FirstStation=ListStations[35].Code,
                     LastStation=ListStations[45].Code,
                 },
                 new Line//10
                 {
                     Id= Counts.getBusLineCount(),
                     Code=10,
                     Area =Areas.Jerusalem ,
                     FirstStation=ListStations[20].Code,
                     LastStation=ListStations[40].Code,
                 },

            };

            ListBuses = new List<Bus>()
            {
                new Bus//1
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate= (new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 10000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 10000
                },
                new Bus//2
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate=(new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 11000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 11000
                },
                new Bus//3
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate= (new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 12000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 12000
                },
                new Bus//4
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate= (new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 13000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 13000
                },
                new Bus//5
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate=(new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 14000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 14000
                },
                new Bus//6
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate= (new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 15000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 15000
                },
                new Bus//7
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate=(new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 16000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 16000
                },
                new Bus//8
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate= (new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 17000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 17000
                },
                new Bus//9
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate= (new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 18000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 18000
                },
                new Bus//10
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate= (new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 19000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 19000
                },
                new Bus//11
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate= (new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 100000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =100000-random.Next(1,15000),
                    KilometersAtLastRefueling= 100000
                },
                new Bus//12
                {
                   LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate=(new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 110000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =110000-random.Next(1,15000),
                    KilometersAtLastRefueling= 110000
                },
                new Bus//13
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate=(new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 120000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =120000-random.Next(1,15000),
                    KilometersAtLastRefueling= 120000
                },
                new Bus//14
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate=(new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 130000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =130000-random.Next(1,15000),
                    KilometersAtLastRefueling= 130000
                },
                new Bus//15
                {
                   LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate= (new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 140000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =140000-random.Next(1,15000),
                    KilometersAtLastRefueling= 140000
                },
                new Bus//16
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate= (new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 150000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =150000-random.Next(1,15000),
                    KilometersAtLastRefueling= 150000
                },
                new Bus//17
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate= (new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 160000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =160000-random.Next(1,15000),
                    KilometersAtLastRefueling= 160000
                },
                new Bus//18
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate= (new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 170000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =170000-random.Next(1,15000),
                    KilometersAtLastRefueling= 170000
                },
                new Bus//19
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate=(new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 180000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =180000-random.Next(1,15000),
                    KilometersAtLastRefueling= 180000
                },
                new Bus//20
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate= (new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 190000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =190000-random.Next(1,15000),
                    KilometersAtLastRefueling= 190000
                },
            };

            ListLineTrips = new List<LineTrip>()
            {
                new LineTrip//1
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[0].Code,
                    StartAt = new TimeSpan(6,0,0),
                    Frequency = new TimeSpan(0,0,30),
                    FinishAt = new TimeSpan(23,0,0)
                },
                new LineTrip//2
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[1].Code,
                    StartAt = new TimeSpan(6,0,0),
                    Frequency = new TimeSpan(0,15,0),
                    FinishAt = new TimeSpan(23,45,0)
                },
                new LineTrip//3
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[2].Code,
                    StartAt = new TimeSpan(6,0,0),
                    Frequency = new TimeSpan(1,0,0),
                    FinishAt = new TimeSpan(23,0,0)
                },
                new LineTrip//4
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[3].Code,
                    StartAt = new TimeSpan(6,0,0),
                    Frequency = new TimeSpan(0,0,30),
                    FinishAt = new TimeSpan(23,0,0)
                },
                new LineTrip//5
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[4].Code,
                    StartAt = new TimeSpan(7,0,0),
                    Frequency = new TimeSpan(0,15,0),
                    FinishAt = new TimeSpan(21,45,0)
                },
                new LineTrip//6
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[5].Code,
                    StartAt = new TimeSpan(8,0,0),
                    Frequency = new TimeSpan(1,0,0),
                    FinishAt = new TimeSpan(22,0,0)
                },
                new LineTrip//7
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[6].Code,
                    StartAt = new TimeSpan(12,0,0),
                    Frequency = new TimeSpan(0,1,0),
                    FinishAt = new TimeSpan(23,0,0)
                },
                new LineTrip//8
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[7].Code,
                    StartAt = new TimeSpan(10,0,0),
                    Frequency = new TimeSpan(0,15,0),
                    FinishAt = new TimeSpan(21,45,0)
                },
                new LineTrip//9
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[8].Code,
                    StartAt = new TimeSpan(5,0,0),
                    Frequency = new TimeSpan(1,0,0),
                    FinishAt = new TimeSpan(22,0,0)
                },
                new LineTrip//10
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[9].Code,
                    StartAt = new TimeSpan(6,0,0),
                    Frequency = new TimeSpan(1,30,0),
                    FinishAt = new TimeSpan(12,0,0)
                },
            };

            ListLineStations = new List<LineStation>();
            for (int i = 0; i < 10; i++)
            {

                ListLineStations.Add(new LineStation
                {
                    LineId = ListLines[i].Id,
                    Station = ListStations[0].Code,
                    LineStationIndex = 1,
                    PrevStation = ListStations[0].Code,
                    NextStation = ListStations[1].Code
                });

                for (int j = 1; j < 10; j++)
                {

                    ListLineStations.Add(new LineStation
                    {
                        LineId = ListLines[i].Id,
                        Station = ListStations[j - 1].Code,
                        LineStationIndex = j,
                        PrevStation = ListStations[j].Code,
                        NextStation = ListStations[j + 1].Code
                    });
                }

            }

            ListUsers = new List<User>()
            {
                new User
                {
                    UserName="Admin",
                    Password="Admin",
                    Admin=true
                },
                new User
                {
                    UserName="Guest",
                    Password="Guest",
                    Admin=false
                }
            };

            ListAdjacentStations = new List<AdjacentStations>();
            for (int i = 0; i < 48; i++)
            {
                ListAdjacentStations.Add(new AdjacentStations
                {
                    Station1= ListStations[i].Code,
                    Station2 = ListStations[i+1].Code,
                    Distance = random.Next(1,5)*0.9,
                    Time=new TimeSpan(0, random.Next(2, 10), 0)
                });
            }



        }
    }
}
