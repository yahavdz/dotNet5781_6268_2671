using System;
using System.Collections.Generic;
//using System.Windows.Media;
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
                    Address= "Tel-Aviv 122",
                    Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                    Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                    Name = " Dolev st. - Habrosh st.",
                    Accessibility = true,
                    Active = true
                },
                new Station//2
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Tel-Aviv 13",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Haermon st. - Hayarden st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//3
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Tel-Aviv 172",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Haermon st. - Hamor st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//4
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Tel-Aviv 72",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Hamor st. - Hayarden st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//5
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Tel-Aviv 73",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Yaalom st. - Hayarden st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//6
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Tel-Aviv 134",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Carmel st. - Yaalom st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//7
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Tel-Aviv 14",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Hagoren st. - Carmel st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//8
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Tel-Aviv 125",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Hashita st. - Hagoren st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//9
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Tel-Aviv 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Lakish st. - Hashita st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//10
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Tel-Aviv 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Haharouv st. - Lakish st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//11
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Haifa 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Dolev st. - Habrosh st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//12
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Haifa 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Haermon st. - Hayarden st.",
                      Accessibility = true,
                        Active = true
                  },
                new Station//13
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Haifa 122",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Haermon st. - Hamor st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//14
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Haifa 72",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Hamor st. - Hayarden st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//15
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Haifa 73",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Yaalom st. - Hayarden st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//16
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Haifa 134",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Carmel st. - Yaalom st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//17
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Haifa 14",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Hagoren st. - Carmel st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//18
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Haifa 125",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Hashita st. - Hagoren st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//19
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Haifa 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Lakish st. - Hashita st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//20
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Haifa 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Haharouv st. - Lakish st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//21
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Ashkelon 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Dolev st. - Habrosh st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//22
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Ashkelon 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Haermon st. - Hayarden st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//23
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Ashkelon 122",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Haermon st. - Hamor st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//24
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Ashkelon 72",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Hamor st. - Hayarden st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//25
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Ashkelon 73",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Yaalom st. - Hayarden st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//26
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Ashkelon 134",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Carmel st. - Yaalom st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//27
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Ashkelon 14",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Hagoren st. - Carmel st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//28
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Ashkelon 125",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Hashita st. - Hagoren st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//29
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Ashkelon 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Lakish st. - Hashita st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//30
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Ashkelon 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Haharouv st. - Lakish st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//31
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Moodiin 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Dolev st. - Habrosh st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//32
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Moodiin 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Haermon st. - Hayarden st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//33
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Moodiin 122",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Haermon st. - Hamor st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//34
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Moodiin 72",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Hamor st. - Hayarden st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//35
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Moodiin 73",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Yaalom st. - Hayarden st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//36
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Moodiin 134",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Carmel st. - Yaalom st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//37
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Moodiin 14",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Hagoren st. - Carmel st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//38
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Moodiin 125",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Hashita st. - Hagoren st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//39
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Moodiin 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Lakish st. - Hashita st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//40
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Moodiin 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Haharouv st. - Lakish st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//41
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Beit-Shemesh 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Dolev st. - Habrosh st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//42
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Beit-Shemes 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Haermon st. - Hayarden st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//43
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Beit-Shemes 122",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Haermon st. - Hamor st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//44
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Beit-Shemes 72",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Hamor st. - Hayarden st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//45
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Beit-Shemes 73",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Yaalom st. - Hayarden st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//46
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Beit-Shemes 134",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Carmel st. - Yaalom st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//47
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Beit-Shemes 14",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Hagoren st. - Carmel st.",
                      Accessibility = true,
                      Active = true
                  },
                new Station//48
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Beit-Shemes 125",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Hashita st. - Hagoren st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//49
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Beit-Shemes 15",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Lakish st. - Hashita st.",
                      Accessibility = false,
                      Active = true
                  },
                new Station//50
                  {
                      Code = Counts.getStationsCount(),
                      Address = "Beit-Shemes 155",
                      Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0,
                      Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3,
                      Name = " Haharouv st. - Lakish st.",
                      Accessibility = false,
                      Active = true
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
                     Active = true
                 },
                 new Line//2
                 {
                     Id= Counts.getBusLineCount(),
                     Code=2,
                     Area =Areas.North ,
                     FirstStation=ListStations[10].Code,
                     LastStation=ListStations[19].Code,
                     Active = true
                 },
                 new Line//3
                 {
                     Id= Counts.getBusLineCount(),
                     Code=3,
                     Area =Areas.South ,
                     FirstStation=ListStations[20].Code,
                     LastStation=ListStations[29].Code,
                     Active = true
                 },
                 new Line//4
                 {
                     Id= Counts.getBusLineCount(),
                     Code=4,
                     Area =Areas.Center ,
                     FirstStation=ListStations[30].Code,
                     LastStation=ListStations[39].Code,
                     Active = true
                 },
                 new Line//5
                 {
                     Id= Counts.getBusLineCount(),
                     Code=5,
                     Area =Areas.Jerusalem ,
                     FirstStation=ListStations[40].Code,
                     LastStation=ListStations[49].Code,
                     Active = true
                 },
                 new Line//6
                 {
                     Id= Counts.getBusLineCount(),
                     Code=6,
                     Area =Areas.General ,
                     FirstStation=ListStations[5].Code,
                     LastStation=ListStations[14].Code,
                     Active = true
                 },
                 new Line//7
                 {
                     Id= Counts.getBusLineCount(),
                     Code=7,
                     Area =Areas.General ,
                     FirstStation=ListStations[15].Code,
                     LastStation=ListStations[24].Code,
                     Active = true
                 },
                 new Line//8
                 {
                     Id= Counts.getBusLineCount(),
                     Code=8,
                     Area =Areas.South ,
                     FirstStation=ListStations[25].Code,
                     LastStation=ListStations[34].Code,
                     Active = true
                 },
                 new Line//9
                 {
                     Id= Counts.getBusLineCount(),
                     Code=9,
                     Area =Areas.General ,
                     FirstStation=ListStations[35].Code,
                     LastStation=ListStations[44].Code,
                     Active = true
                 },
                 new Line//10
                 {
                     Id= Counts.getBusLineCount(),
                     Code=10,
                     Area =Areas.Jerusalem ,
                     FirstStation=ListStations[20].Code,
                     LastStation=ListStations[39].Code,
                     Active = true
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
                    KilometersAtLastRefueling= 10000,
                    Active = true
                },
                new Bus//2
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate=(new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 11000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 11000,
                    Active = true
                },
                new Bus//3
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate= (new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 12000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 12000,
                    Active = true
                },
                new Bus//4
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate= (new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 13000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 13000,
                    Active = true
                },
                new Bus//5
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate=(new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 14000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 14000,
                    Active = true
                },
                new Bus//6
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate= (new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 15000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 15000,
                    Active = true
                },
                new Bus//7
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate=(new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 16000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 16000,
                    Active = true
                },
                new Bus//8
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate= (new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 17000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 17000,
                    Active = true
                },
                new Bus//9
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate= (new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 18000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 18000,
                    Active = true
                },
                new Bus//10
                {
                    LicenseNum = Counts.getLicenseNumFrom2018(),
                    FromDate= (new DateTime(random.Next(2018, 2021), random.Next(1, 12), 1)).AddDays(random.Next(30)),
                    TotalTrip = 19000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =0,
                    KilometersAtLastRefueling= 19000,
                    Active = true
                },
                new Bus//11
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate= (new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 100000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =100000-random.Next(1,15000),
                    KilometersAtLastRefueling= 100000,
                    Active = true
                },
                new Bus//12
                {
                   LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate=(new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 110000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =110000-random.Next(1,15000),
                    KilometersAtLastRefueling= 110000,
                    Active = true
                },
                new Bus//13
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate=(new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 120000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =120000-random.Next(1,15000),
                    KilometersAtLastRefueling= 120000,
                    Active = true
                },
                new Bus//14
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate=(new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 130000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =130000-random.Next(1,15000),
                    KilometersAtLastRefueling= 130000,
                    Active = true
                },
                new Bus//15
                {
                   LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate= (new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 140000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =140000-random.Next(1,15000),
                    KilometersAtLastRefueling= 140000,
                    Active = true
                },
                new Bus//16
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate= (new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 150000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =150000-random.Next(1,15000),
                    KilometersAtLastRefueling= 150000,
                    Active = true
                },
                new Bus//17
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate= (new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 160000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =160000-random.Next(1,15000),
                    KilometersAtLastRefueling= 160000,
                    Active = true
                },
                new Bus//18
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate= (new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 170000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =170000-random.Next(1,15000),
                    KilometersAtLastRefueling= 170000,
                    Active = true
                },
                new Bus//19
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate=(new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 180000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =180000-random.Next(1,15000),
                    KilometersAtLastRefueling= 180000,
                    Active = true
                },
                new Bus//20
                {
                    LicenseNum = Counts.getlicenseNumLess2018(),
                    FromDate= (new DateTime(random.Next(1985, 2018), random.Next(1, 13), 1)).AddDays(random.Next(30)),
                    TotalTrip = 190000,
                    FuelRemain = 1200,
                    BusStatus = BusStatus.readyToGo,
                    KilometersSinceLastTreatment =190000-random.Next(1,15000),
                    KilometersAtLastRefueling= 190000,
                    Active = true
                },
            };

            #region LineTrip
            ListLineTrips = new List<LineTrip>()
            {
                new LineTrip//1
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[0].Id,
                    StartAt = new TimeSpan(6,0,0),
                    Frequency = new TimeSpan(0,30,0),
                    FinishAt = new TimeSpan(23,0,0),
                    Active = true
                },
                new LineTrip//2
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[1].Id,
                    StartAt = new TimeSpan(6,0,0),
                    Frequency = new TimeSpan(0,15,0),
                    FinishAt = new TimeSpan(23,45,0),
                    Active = true
                },
                new LineTrip//3
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[2].Id,
                    StartAt = new TimeSpan(6,0,0),
                    Frequency = new TimeSpan(1,0,0),
                    FinishAt = new TimeSpan(23,0,0),
                    Active = true
                },
                new LineTrip//4
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[3].Id,
                    StartAt = new TimeSpan(6,0,0),
                    Frequency = new TimeSpan(0,30,0),
                    FinishAt = new TimeSpan(23,0,0),
                    Active = true
                },
                new LineTrip//5
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[4].Id,
                    StartAt = new TimeSpan(7,0,0),
                    Frequency = new TimeSpan(0,15,0),
                    FinishAt = new TimeSpan(21,45,0),
                    Active = true
                },
                new LineTrip//6
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[5].Id,
                    StartAt = new TimeSpan(8,0,0),
                    Frequency = new TimeSpan(1,0,0),
                    FinishAt = new TimeSpan(22,0,0),
                    Active = true
                },
                new LineTrip//7
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[6].Id,
                    StartAt = new TimeSpan(12,0,0),
                    Frequency = new TimeSpan(1,0,0),
                    FinishAt = new TimeSpan(23,0,0),
                    Active = true
                },
                new LineTrip//8
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[7].Id,
                    StartAt = new TimeSpan(10,0,0),
                    Frequency = new TimeSpan(0,15,0),
                    FinishAt = new TimeSpan(21,45,0),
                    Active = true
                },
                new LineTrip//9
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[8].Id,
                    StartAt = new TimeSpan(5,0,0),
                    Frequency = new TimeSpan(1,0,0),
                    FinishAt = new TimeSpan(22,0,0),
                    Active = true
                },
                new LineTrip//10
                {
                    Id = Counts.getTripLine(),
                    LineId = ListLines[9].Id,
                    StartAt = new TimeSpan(6,0,0),
                    Frequency = new TimeSpan(1,30,0),
                    FinishAt = new TimeSpan(12,0,0),
                    Active = true
                },
            };
            #endregion

            #region LineStation
            ListLineStations = new List<LineStation>();
            for (int i = 0; i < 5; i++)
            {
                ListLineStations.Add(new LineStation
                {
                    LineId = ListLines[i].Id,
                    Station = ListLines[i].FirstStation,
                    LineStationIndex = 0,
                    // PrevStation = ListStations[0].Code,
                    NextStation = ListLines[i].FirstStation + 1,
                    Active = true
                });

                for (int j = 1; j < 9; j++)
                {
                    ListLineStations.Add(new LineStation
                    {
                        LineId = ListLines[i].Id,
                        Station = ListLines[i].FirstStation +  j,
                        LineStationIndex = j,
                        PrevStation = ListLines[i].FirstStation + j - 1,
                        NextStation = ListLines[i].FirstStation + j + 1,
                        Active = true
                    });
                }

                ListLineStations.Add(new LineStation
                {
                    LineId = ListLines[i].Id,
                    Station = ListLines[i].LastStation,
                    LineStationIndex = 9,
                    PrevStation = ListLines[i].LastStation - 1,
                    // NextStation = ListStations[i * 10 + 1] .Code,
                    Active = true
                });
            }
            for (int i = 0; i < 4; i++)
            {
                ListLineStations.Add(new LineStation
                {
                    LineId = ListLines[i + 5].Id,
                    Station = ListLines[i + 5].FirstStation,
                    LineStationIndex = 0,
                    // PrevStation = ListStations[0].Code,
                    NextStation = ListLines[i + 5].FirstStation + 1,
                    Active = true
                }) ;

                for (int j = 1; j < 9; j++)
                {
                    ListLineStations.Add(new LineStation
                    {
                        LineId = ListLines[i + 5].Id,
                        Station = ListLines[i + 5].FirstStation + j,
                        LineStationIndex = j,
                        PrevStation = ListLines[i + 5].FirstStation + j - 1,
                        NextStation = ListLines[i + 5].FirstStation + j + 1,
                        Active = true
                    });
                }


                ListLineStations.Add(new LineStation
                {
                    LineId = ListLines[i + 5].Id,
                    Station = ListLines[i + 5].LastStation,
                    LineStationIndex = 9,
                    PrevStation = ListLines[i + 5].LastStation - 1,
                    // NextStation = ListStations[i * 10 + 1] .Code,
                    Active = true
                });
            }

            // for the last line with 20 stations:
            ListLineStations.Add(new LineStation
            {
                LineId = ListLines[9].Id,
                Station = ListLines[9].FirstStation,
                LineStationIndex = 0,
                // PrevStation = ListStations[0].Code,
                NextStation = ListLines[9].FirstStation + 1,
                Active = true
            });
            for (int j = 1; j < 19; j++)
            {
                ListLineStations.Add(new LineStation
                {
                    LineId = ListLines[9].Id,
                    Station = ListLines[9].FirstStation + j,
                    LineStationIndex = j,
                    PrevStation = ListLines[9].FirstStation + j - 1,
                    NextStation = ListLines[9].FirstStation + j + 1,
                    Active = true
                });
            }
            ListLineStations.Add(new LineStation
            {
                LineId = ListLines[9].Id,
                Station = ListLines[9].LastStation,
                LineStationIndex = 19,
                PrevStation = ListLines[9].LastStation - 1,
                // NextStation = ListStations[i * 10 + 1] .Code,
                Active = true
            });
            #endregion

            #region User
            ListUsers = new List<User>()
            {
                new User
                {
                    UserName = "zzz",
                    HashPassword = 15947562,
                    Admin = true,
                    Active = true
                },
                new User
                {
                    UserName = "admin",
                    HashPassword = 15947562,
                    Admin = true,
                    Active = true
                },
                new User
                {
                    UserName = "aaa",
                    HashPassword = -1152142086,
                    Admin = false,
                    Active = true
                }
                
            };
            #endregion

            ListAdjacentStations = new List<AdjacentStations>();
            foreach (LineStation lineStation in ListLineStations)
            {
                if (lineStation.NextStation != 0)
                {
                    ListAdjacentStations.Add(new AdjacentStations
                    {
                        Station1 = lineStation.Station,
                        Station2 = lineStation.NextStation,
                        Distance = random.NextDouble() * 20,
                        Time = new TimeSpan(random.Next(0, 2), random.Next(0, 59), random.Next(0, 59)),
                        Active = true
                    });
                }
            }
        }
    }
}
