using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DalXml;
using DO;
using DS;

namespace Dal
{
    sealed class DalXml : IDal    //singletone
    {
        #region singelton
        static readonly DalXml instance = new DalXml();
        static DalXml() { }// static ctor to ensure instance init is done just before first usage
        private DalXml() 
        {
            List<Line> lines = GetAllLines().ToList();
            DO.Counts.setBusLineCount(lines.Count() + 1);
            List<Station> station = GetAllStations().ToList();
            DO.Counts.setStationsCount(station.Count() + 1);
            List<BusOnTrip> busOT = GetAllBusOnTrip().ToList();
            DO.Counts.setBusOnTripCount(busOT.Count() + 1); 
            List<Trip> trip = GetAllTrip().ToList();
            DO.Counts.setTripCount(trip.Count() + 1);
        }
        public static DalXml Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files

        string busPath = @"BusXml.xml"; //XMLSerializer
        string adjacentStationPath = @"AdjacentStationXml.xml"; //XElement
        string lineStationPath = @"LineStationXml.xml"; //XMLSerializer
        string lineTripPath = @"LineTripXml.xml"; //XElement
        string linePath = @"LineXml.xml"; //XMLSerializer
        string stationPath = @"StationXml.xml"; //XMLSerializer
        string userPath = @"UserXml.xml"; //XMLSerializer
        string busOnTripPath = @"BusOnTripXml.xml"; //XElement
        string tripPath = @"TripXml.xml"; //XElement


        #endregion

        #region Bus
        public Bus GetBus(int busId)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);

            Bus bus = ListBuses.Find(b => b.LicenseNum == busId && b.Active);

            if (bus != null)
                return bus;
            else
                throw new BadIdException(busId, $"bad bus id: {busId}");
        }
        public IEnumerable<Bus> GetAllBuses()
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);

            return from bus in ListBuses.FindAll(b => b.Active)
                   select bus;
        }
        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);

            return from bus in ListBuses.FindAll(b => b.Active).FindAll(predicate)
                   select bus;
        }
        public void AddBus(Bus bus)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);

            Bus findBus = ListBuses.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum);
            if (findBus != null && findBus.Active)
                throw new BadIdException(bus.LicenseNum, "Duplicate bus ID");
            if (findBus != null && !findBus.Active)
                ListBuses.Remove(findBus);
           ListBuses.Add(bus);
            XMLTools.SaveListToXMLSerializer(ListBuses, busPath);
        }
        public void DeleteBus(int busId)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);

            Bus bus = ListBuses.FirstOrDefault(b => b.LicenseNum == busId);
            if (bus != null)
            {
                ListBuses.Remove(bus);
                bus.Active = false;
                ListBuses.Add(bus);
            }

            else
                throw new BadIdException(busId, $"bad bus id: {busId}");
            XMLTools.SaveListToXMLSerializer(ListBuses, busPath);

        }
        public void UpdateBus(Bus bus)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);

            Bus findBus = ListBuses.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum && b.Active);

            if (findBus != null)
            {
                ListBuses.Remove(findBus);
                ListBuses.Add(bus);
            }
            else
                throw new BadIdException(bus.LicenseNum, $"bad bus id: {bus.LicenseNum}");
            XMLTools.SaveListToXMLSerializer(ListBuses, busPath);

        }
        #endregion Bus

        #region Line
        public Line GetLine(int lineId)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            Line line = ListLines.Find(l => l.Id == lineId && l.Active);

            if (line != null)
                return line;
            else
                throw new BadIdException(lineId, $"bad line id: {lineId}");
        }
        public IEnumerable<Line> GetAllLines()
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            return from line in ListLines.FindAll(l => l.Active)
                   select line;
        }
        public IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            return from line in ListLines.FindAll(l => l.Active).FindAll(predicate)
                   select line;
        }
        public int AddLine(Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            Line findLine = ListLines.FirstOrDefault(l => l.Id == line.Id);
            if (findLine != null && findLine.Active)
                throw new BadIdException(line.Id, "Duplicate line ID");
            if (findLine != null && !findLine.Active)
                ListLines.Remove(findLine);
            Line newLine = line;
            newLine.Id = Counts.getBusLineCount();
            ListLines.Add(newLine);
            XMLTools.SaveListToXMLSerializer(ListLines, linePath);
            return newLine.Id;
        }
        public void DeleteLine(int lineId)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            Line line = ListLines.Find(l => l.Id == lineId);
            if (line != null)
            {
                ListLines.Remove(line);
                line.Active = false;
                ListLines.Add(line);
            }
            else
                throw new BadIdException(lineId, $"bad line id: {lineId}");
            foreach (LineStation ls in GetAllLineStationBy(_ls => _ls.LineId == line.Id))
                ls.Active = false;
            XMLTools.SaveListToXMLSerializer(ListLines, linePath);


        }
        public void UpdateLine(Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            Line findLine = ListLines.FirstOrDefault(l => l.Id == line.Id && l.Active);

            if (findLine != null)
            {
                ListLines.Remove(findLine);
                ListLines.Add(line);
            }
            else
                throw new BadIdException(line.Id, $"bad line id: {line.Id}");
            XMLTools.SaveListToXMLSerializer(ListLines, linePath);

        }
        #endregion Line

        #region Station
        public Station GetStation(int stationId)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            Station station = ListStations.Find(s => s.Code == stationId && s.Active);

            if (station != null)
                return station;
            else
                throw new BadIdException(stationId, $"bad station code: {stationId}");
        }
        public IEnumerable<Station> GetAllStations()
        {

            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            return from station in ListStations.FindAll(s => s.Active)
                   select station;
        }
        public IEnumerable<Station> GetAllStationBy(Predicate<Station> predicate)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            return from station in ListStations.FindAll(s => s.Active).FindAll(predicate)
                   select station;
        }
        public void AddStation(Station station)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            Station findStation = ListStations.FirstOrDefault(s => s.Code == station.Code);
            if (findStation != null && findStation.Active)
                throw new BadIdException(station.Code, "Duplicate station Code");
            if (findStation != null && !findStation.Active)
                ListStations.Remove(findStation);
            ListStations.Add(station);
            XMLTools.SaveListToXMLSerializer(ListStations, stationPath);


        }
        public void DeleteStation(int stationId)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            Station station =ListStations.Find(s => s.Code == stationId);
            if (station != null)
            {
                ListStations.Remove(station);
                station.Active = false;
                ListStations.Add(station);
            }

            else
                throw new BadIdException(stationId, $"bad station code: {stationId}");
            XMLTools.SaveListToXMLSerializer(ListStations, stationPath);

        }
        public void UpdateStation(Station station)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            Station findStation = ListStations.FirstOrDefault(s => s.Code == station.Code && s.Active);

            if (findStation != null)
            {
                ListStations.Remove(findStation);
                ListStations.Add(station);
            }
            else
                throw new BadIdException(station.Code, $"bad station code: {station.Code}");
            XMLTools.SaveListToXMLSerializer(ListStations, stationPath);

        }
        #endregion Station

        #region LineStation
        public LineStation GetLineStation(int lineId, int station)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            LineStation lineStation = ListLineStations.Find(ls => ls.LineId == lineId && ls.Active && ls.Station == station);

            if (lineStation != null)
                return lineStation;
            else
                throw new BadIdException(lineId, $"The is no station {station} in line: {lineId}");
        }
        public IEnumerable<LineStation> GetAllLineStation()
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);
            return from lineStation in ListLineStations.FindAll(ls => ls.Active)
                   select lineStation;
        }
        public IEnumerable<LineStation> GetAllLineStationBy(Predicate<LineStation> predicate)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            return from lineStation in ListLineStations.FindAll(ls => ls.Active).FindAll(predicate)
                   select lineStation;
        }
        public void AddLineStation(LineStation lineStation)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            LineStation findLineStation = ListLineStations.FirstOrDefault(ls => ls.LineId == lineStation.LineId && ls.Station == lineStation.Station);
            if (findLineStation != null && findLineStation.Active)
                throw new BadIdException(lineStation.LineId, "Duplicate line station ID");
            if (findLineStation != null && !findLineStation.Active)
                ListLineStations.Remove(findLineStation);
            ListLineStations.Add(lineStation);
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationPath);


        }
        public void DeleteLineStation(int lineId, int station)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            LineStation lineStation = ListLineStations.Find(ls => ls.LineId == lineId && ls.Station == station);
            if (lineStation != null)
            {
                ListLineStations.Remove(lineStation);
                lineStation.Active = false;
                ListLineStations.Add(lineStation);
            }
            else
                throw new BadIdException(lineId, $"bad line station with line id: {lineId} and station code: {station}");
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationPath);
        }
        public void UpdateLineStation(LineStation lineStation)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            LineStation findLineStation = ListLineStations.FirstOrDefault(ls => ls.LineId == lineStation.LineId && ls.Station == lineStation.Station && ls.Active);
            if (findLineStation != null)
            {
                ListLineStations.Remove(findLineStation);
                ListLineStations.Add(lineStation);
            }
            else
                throw new BadIdException(lineStation.LineId, $"bad line station with line id: {lineStation.LineId} and station code: {lineStation.Station}");
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationPath);
        }
        public bool isLineStationExistForLine(int lineId, int lineStationsId)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            return (ListLineStations.Any(ls => ls.LineId == lineId && ls.Station == lineStationsId));
        }

        #endregion LineStation

        #region User
        public User GetUser(string userName)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(userPath);

            User user = ListUsers.Find(u => u.UserName == userName && u.Active);
            if (user != null)
                return user;
            else
                throw new BadUserNameException(userName, $"bad user name: {userName}");
        }
        public IEnumerable<User> GetAllUsers()
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(userPath);

            return from user in ListUsers.FindAll(u => u.Active)
                   select user;
        }
        public IEnumerable<User> GetAllUsersBy(Predicate<User> predicate)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(userPath);

            return from user in ListUsers.FindAll(u => u.Active).FindAll(predicate)
                   select user;
        }
        public void AddUser(User user)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(userPath);

            User findUser = ListUsers.FirstOrDefault(u => u.UserName == user.UserName);
            if (findUser != null && findUser.Active)
                throw new BadUserNameException(user.UserName, "Duplicate user name");
            if (findUser != null && !findUser.Active)
                ListUsers.Remove(findUser);
            ListUsers.Add(user);
            XMLTools.SaveListToXMLSerializer(ListUsers, userPath);

        }
        public void DeleteUser(string userName)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(userPath);

            User user = ListUsers.Find(u => u.UserName == userName);
            if (user != null)
            {
                ListUsers.Remove(user);
                user.Active = false;
                ListUsers.Add(user);
            }
            else
                throw new BadUserNameException(userName, $"bad user name: {userName}");
            XMLTools.SaveListToXMLSerializer(ListUsers, userPath);

        }
        public void UpdateUser(User user)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(userPath);

            User findUser = ListUsers.FirstOrDefault(u => u.UserName == user.UserName && u.Active);
            if (findUser != null)
            {
                ListUsers.Remove(findUser);
                ListUsers.Add(user);
            }
            else
                throw new BadUserNameException(user.UserName, $"bad user id: {user.UserName}");
            XMLTools.SaveListToXMLSerializer(ListUsers, userPath);

        }
        #endregion User

        #region AdjacentStations
        public AdjacentStations GetAdjacentStations(int adjacentStationsId1, int adjacentStationsId2)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationPath);

            AdjacentStations adjacentStations = (from adj in adjacentStationsRootElem.Elements()
                                                 where (int.Parse(adj.Element("Station1").Value) == adjacentStationsId1 && int.Parse(adj.Element("Station2").Value) == adjacentStationsId2 && adj.Element("Active").Value == "true")
                                                 select new AdjacentStations()
                                                 {
                                                     Station1 = int.Parse(adj.Element("Station1").Value),
                                                     Station2 = int.Parse(adj.Element("Station2").Value),
                                                     Distance = Convert.ToDouble(adj.Element("Distance").Value),
                                                     Time =  TimeSpan.Parse(adj.Element("Time").Value),
                                                     Active = Convert.ToBoolean(adj.Element("Active").Value)
                                                 }
                                                 ).FirstOrDefault();
            if (adjacentStations != null)
                return adjacentStations;
            else
                throw new BadAdjacentStationsException(adjacentStationsId1, adjacentStationsId2, $"bad adjacent stations id: {adjacentStationsId1}, {adjacentStationsId2}");
        }
        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationPath);

            return (from adj in adjacentStationsRootElem.Elements()
                    where(adj.Element("Active").Value == "true")
                    select new AdjacentStations()
                    {
                        Station1 = int.Parse(adj.Element("Station1").Value),
                        Station2 = int.Parse(adj.Element("Station2").Value),
                        Distance = Convert.ToDouble(adj.Element("Distance").Value),
                        Time = TimeSpan.Parse(adj.Element("Time").Value),
                        Active = Convert.ToBoolean(adj.Element("Active").Value)
                    }
                                                 );
        }
        public IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> predicate)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationPath);

            return from adj in adjacentStationsRootElem.Elements()
                    let adj1 = new AdjacentStations()
                    {
                        Station1 = int.Parse(adj.Element("Station1").Value),
                        Station2 = int.Parse(adj.Element("Station2").Value),
                        Distance = Convert.ToDouble(adj.Element("Distance").Value),
                        Time = TimeSpan.Parse(adj.Element("Time").Value),
                        Active = Convert.ToBoolean(adj.Element("Active").Value)
                    }
                    where predicate(adj1)
                    select adj1;
        }
        public void AddAdjacentStations(AdjacentStations adjacentStations)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationPath);

            XElement findAdjacentStations = (from adj in adjacentStationsRootElem.Elements()
                                                     where (int.Parse(adj.Element("Station1").Value) == adjacentStations.Station1 && int.Parse(adj.Element("Station2").Value) == adjacentStations.Station2)
                                                     select adj).FirstOrDefault();
            if (findAdjacentStations != null && findAdjacentStations.Element("Active").Value=="true")
                throw new BadAdjacentStationsException(adjacentStations.Station1, adjacentStations.Station2, "Duplicate adjacentStations");
            if (findAdjacentStations != null && !(findAdjacentStations.Element("Active").Value == "true"))
            {
                XElement adjElem = new XElement("adjacentStations", 
                                   new XElement("Station1", adjacentStations.Station1),
                                   new XElement("Station2", adjacentStations.Station2),
                                   new XElement("Distance", adjacentStations.Distance),
                                   new XElement("Time", adjacentStations.Time),
                                   new XElement("Active", adjacentStations.Active));
                adjacentStationsRootElem.Add(adjElem);
            }
            XMLTools.SaveListToXMLElement(adjacentStationsRootElem, adjacentStationPath);
        }
        public void DeleteAdjacentStations(int adjacentStationsId1, int adjacentStationsId2)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationPath);

            XElement findAdjacentStations = (from adj in adjacentStationsRootElem.Elements()
                                             where (int.Parse(adj.Element("Station1").Value) == adjacentStationsId1 && int.Parse(adj.Element("Station2").Value) == adjacentStationsId2)
                                             select adj).FirstOrDefault();
            if (findAdjacentStations != null)
            {
                findAdjacentStations.Element("Active").Value = "true";//not sure
                XMLTools.SaveListToXMLElement(adjacentStationsRootElem, adjacentStationPath);
            }

            else
                throw new BadAdjacentStationsException(adjacentStationsId1, adjacentStationsId2, $"bad adjacent stations id: {adjacentStationsId1}, {adjacentStationsId2}");
        }
        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            XElement adjacentStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationPath);

            XElement findAdjacentStations = (from adj in adjacentStationsRootElem.Elements()
                                             where (int.Parse(adj.Element("Station1").Value) == adjacentStations.Station1 && int.Parse(adj.Element("Station2").Value) == adjacentStations.Station2 && adj.Element("Active").Value == "true")
                                             select adj).FirstOrDefault();

            if (findAdjacentStations != null)
            {
                findAdjacentStations.Element("Station1").Value = adjacentStations.Station1.ToString();
                findAdjacentStations.Element("Station2").Value = adjacentStations.Station2.ToString();
                findAdjacentStations.Element("Distance").Value = adjacentStations.Distance.ToString();
                findAdjacentStations.Element("Time").Value = adjacentStations.Time.ToString();
                findAdjacentStations.Element("Active").Value = adjacentStations.Active.ToString();
                XMLTools.SaveListToXMLElement(adjacentStationsRootElem, adjacentStationPath);
            }
            else
                throw new BadAdjacentStationsException(adjacentStations.Station1, adjacentStations.Station2, $"bad adjacent stations id: {adjacentStations.Station1}, {adjacentStations.Station2}");
        }
        #endregion AdjacentStations

        #region LineTrip
        public LineTrip GetLineTrip(int lineTripId)
        {
            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);

            LineTrip lineTrip = (from line in LineTripsRootElem.Elements()
                                                 where (int.Parse(line.Element("Id").Value) == lineTripId && line.Element("Active").Value == "true")
                                                 select new LineTrip()
                                                 {
                                                     Id = int.Parse(line.Element("Id").Value),
                                                     LineId = int.Parse(line.Element("LineId").Value),
                                                     StartAt = TimeSpan.Parse(line.Element("StartAt").Value),
                                                     Frequency = TimeSpan.Parse(line.Element("Frequency").Value),
                                                     FinishAt = TimeSpan.Parse(line.Element("FinishAt").Value),
                                                     Active = Convert.ToBoolean(line.Element("Active").Value)
                                                 }
                                                ).FirstOrDefault();
            if (lineTrip != null)
                return lineTrip;
            else
                throw new BadIdException(lineTripId, $"bad line trip id: {lineTripId}");
        }
        public IEnumerable<LineTrip> GetAllLineTrip()
        {
            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);

            return (from line in LineTripsRootElem.Elements()
                    where(line.Element("Active").Value == "true")
                    select new LineTrip()
                    {
                        Id = int.Parse(line.Element("Id").Value),
                        LineId = int.Parse(line.Element("LineId").Value),
                        StartAt = TimeSpan.Parse(line.Element("StartAt").Value),
                        Frequency = TimeSpan.Parse(line.Element("Frequency").Value),
                        FinishAt = TimeSpan.Parse(line.Element("FinishAt").Value),
                        Active = Convert.ToBoolean(line.Element("Active").Value)
                    });
        }
        public IEnumerable<LineTrip> GetAllLineTripBy(Predicate<LineTrip> predicate)
        {
            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);

            return from line in LineTripsRootElem.Elements()
                   let line1 = new LineTrip()
                   {
                       Id = int.Parse(line.Element("Id").Value),
                       LineId = int.Parse(line.Element("LineId").Value),
                       StartAt = TimeSpan.Parse(line.Element("StartAt").Value),
                       Frequency = TimeSpan.Parse(line.Element("Frequency").Value),
                       FinishAt = TimeSpan.Parse(line.Element("FinishAt").Value),
                       Active = Convert.ToBoolean(line.Element("Active").Value)
                   }
                   where predicate(line1)
                   select line1;
        }
        public void AddLineTrip(LineTrip lineTrip)
        {
            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);
            XElement findLineTrip = (from line in LineTripsRootElem.Elements()
                                             where (int.Parse(line.Element("Id").Value) == lineTrip.Id) 
                                             select line).FirstOrDefault();
            if (findLineTrip != null && findLineTrip.Element("Active").Value == "true")
                throw new BadIdException(lineTrip.Id, "Duplicate line trip ID");
            if (findLineTrip != null && !(findLineTrip.Element("Active").Value == "true"))
            {
                XElement lTElem = new XElement("LineTrip",
                                      new XElement("Id", lineTrip.Id),
                                      new XElement("LineId", lineTrip.LineId),
                                      new XElement("StartAt", lineTrip.StartAt),
                                      new XElement("Frequency", lineTrip.Frequency),
                                      new XElement("FinishAt", lineTrip.FinishAt),
                                      new XElement("Active", lineTrip.Active));
                LineTripsRootElem.Add(lTElem);
            }
            XMLTools.SaveListToXMLElement(LineTripsRootElem, lineTripPath);
        }
        public void DeleteLineTrip(int lineTripId)
        {
            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);

            XElement findLineTrip = (from line in LineTripsRootElem.Elements()
                                     where (int.Parse(line.Element("Id").Value) == lineTripId)
                                     select line).FirstOrDefault();
            if (findLineTrip != null)
            {
                findLineTrip.Element("Active").Value = "true";//not sure
                XMLTools.SaveListToXMLElement(LineTripsRootElem, lineTripPath);
            }

            else
                throw new BadIdException(lineTripId, $"bad line trip id: {lineTripId}");
        }
        public void UpdateLineTrip(LineTrip lineTrip)
        {
            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripPath);
            XElement findLineTrip = (from line in LineTripsRootElem.Elements()
                                     where (int.Parse(line.Element("Id").Value) == lineTrip.Id && line.Element("Active").Value == "true")
                                     select line).FirstOrDefault();
            if (findLineTrip != null)
            {
                findLineTrip.Element("Id").Value = lineTrip.Id.ToString();
                findLineTrip.Element("LineId").Value = lineTrip.LineId.ToString();
                findLineTrip.Element("StartAt").Value = lineTrip.StartAt.ToString();
                findLineTrip.Element("Frequency").Value = lineTrip.Frequency.ToString();
                findLineTrip.Element("FinishAt").Value = lineTrip.FinishAt.ToString();
                findLineTrip.Element("Active").Value = lineTrip.Active.ToString();
                XMLTools.SaveListToXMLElement(LineTripsRootElem, lineTripPath);
            }
            else
                throw new BadIdException(lineTrip.Id, $"bad line trip id: {lineTrip.Id}");
        }
        #endregion LineTrip

        #region BusOnTrip
        public BusOnTrip GetBusOnTrip(int busOnTripId)
        {
            XElement BusOnTripsRootElem = XMLTools.LoadListFromXMLElement(busOnTripPath);
            BusOnTrip busOnTrip = (from busOT in BusOnTripsRootElem.Elements()
                                 where (int.Parse(busOT.Element("Id").Value) == busOnTripId && busOT.Element("Active").Value == "true")
                                 select new BusOnTrip()
                                 {
                                     Id = int.Parse(busOT.Element("Id").Value),
                                     LineId = int.Parse(busOT.Element("LineId").Value),
                                     LicenseNum = int.Parse(busOT.Element("LicenseNum").Value),
                                     PlannedTakeOff = TimeSpan.Parse(busOT.Element("PlannedTakeOff").Value),
                                     ActualTakeOff = TimeSpan.Parse(busOT.Element("ActualTakeOff").Value),
                                     PrevStation = int.Parse(busOT.Element("PrevStation").Value),
                                     PrevStationAt = TimeSpan.Parse(busOT.Element("PrevStationAt").Value),
                                     NextStationAt = TimeSpan.Parse(busOT.Element("NextStationAt").Value),
                                     Active = Convert.ToBoolean(busOT.Element("Active").Value)
                                 }
                                                ).FirstOrDefault();
            if (busOnTrip != null)
                return busOnTrip;
            else
                throw new BadIdException(busOnTripId, $"bad busOnTrip id: {busOnTripId}");
        }
        public IEnumerable<BusOnTrip> GetAllBusOnTrip()
        {
            XElement BusOnTripsRootElem = XMLTools.LoadListFromXMLElement(busOnTripPath);

            return (from busOT in BusOnTripsRootElem.Elements()
                    where (busOT.Element("Active").Value == "true")
                    select new BusOnTrip()
                    {
                        Id = int.Parse(busOT.Element("Id").Value),
                        LineId = int.Parse(busOT.Element("LineId").Value),
                        LicenseNum = int.Parse(busOT.Element("LicenseNum").Value),
                        PlannedTakeOff = TimeSpan.Parse(busOT.Element("PlannedTakeOff").Value),
                        ActualTakeOff = TimeSpan.Parse(busOT.Element("ActualTakeOff").Value),
                        PrevStation = int.Parse(busOT.Element("PrevStation").Value),
                        PrevStationAt = TimeSpan.Parse(busOT.Element("PrevStationAt").Value),
                        NextStationAt = TimeSpan.Parse(busOT.Element("NextStationAt").Value),
                        Active = Convert.ToBoolean(busOT.Element("Active").Value)
                    });
        }
        public IEnumerable<BusOnTrip> GetAllBusOnTripBy(Predicate<BusOnTrip> predicate)
        {
            XElement BusOnTripsRootElem = XMLTools.LoadListFromXMLElement(busOnTripPath);

            return from busOT in BusOnTripsRootElem.Elements()
                   let busOT1 = new BusOnTrip()
                   {
                       Id = int.Parse(busOT.Element("Id").Value),
                       LineId = int.Parse(busOT.Element("LineId").Value),
                       LicenseNum = int.Parse(busOT.Element("LicenseNum").Value),
                       PlannedTakeOff = TimeSpan.Parse(busOT.Element("PlannedTakeOff").Value),
                       ActualTakeOff = TimeSpan.Parse(busOT.Element("ActualTakeOff").Value),
                       PrevStation = int.Parse(busOT.Element("PrevStation").Value),
                       PrevStationAt = TimeSpan.Parse(busOT.Element("PrevStationAt").Value),
                       NextStationAt = TimeSpan.Parse(busOT.Element("NextStationAt").Value),
                       Active = Convert.ToBoolean(busOT.Element("Active").Value)
                   }
                   where predicate(busOT1)
                   select busOT1;
        }
        public void AddBusOnTrip(BusOnTrip busOnTrip)
        {
            XElement BusOnTripsRootElem = XMLTools.LoadListFromXMLElement(busOnTripPath);
            XElement findBusOnTrip = (from busOT in BusOnTripsRootElem.Elements()
                                     where (int.Parse(busOT.Element("Id").Value) == busOnTrip.Id)
                                     select busOT).FirstOrDefault();
            if (findBusOnTrip != null && findBusOnTrip.Element("Active").Value == "true")
                throw new BadIdException(busOnTrip.Id, "Duplicate busOnTrip ID");
            if (findBusOnTrip != null && !(findBusOnTrip.Element("Active").Value == "true"))
            {
                XElement busOTElem = new XElement("BusOnTrip",
                                      new XElement("Id", busOnTrip.Id),
                                      new XElement("LineId", busOnTrip.LineId),
                                      new XElement("LicenseNum", busOnTrip.LicenseNum),
                                      new XElement("PlannedTakeOff", busOnTrip.PlannedTakeOff),
                                      new XElement("ActualTakeOff", busOnTrip.ActualTakeOff),
                                      new XElement("PrevStation", busOnTrip.PrevStation),
                                      new XElement("PrevStationAt", busOnTrip.PrevStationAt),
                                      new XElement("NextStationAt", busOnTrip.NextStationAt),
                                      new XElement("Active", busOnTrip.Active));
                BusOnTripsRootElem.Add(busOTElem);
            }
            XMLTools.SaveListToXMLElement(BusOnTripsRootElem, busOnTripPath);

        }
        public void DeleteBusOnTrip(int busOnTripId)
        {
            XElement BusOnTripsRootElem = XMLTools.LoadListFromXMLElement(busOnTripPath);

            XElement busOnTrip = (from busOT in BusOnTripsRootElem.Elements()
                                     where (int.Parse(busOT.Element("Id").Value) == busOnTripId)
                                     select busOT).FirstOrDefault();
            if (busOnTrip != null)
            {
                busOnTrip.Element("Active").Value = "true";//not sure
                XMLTools.SaveListToXMLElement(BusOnTripsRootElem, busOnTripPath);
            }

            else
                throw new BadIdException(busOnTripId, $"bad busOnTrip id: {busOnTripId}");
        }
        public void UpdateBusOnTrip(BusOnTrip busOnTrip)
        {
            XElement BusOnTripsRootElem = XMLTools.LoadListFromXMLElement(busOnTripPath);
            XElement findBusOnTrip = (from busOT in BusOnTripsRootElem.Elements()
                                  where (int.Parse(busOT.Element("Id").Value) == busOnTrip.Id && busOT.Element("Active").Value == "true")
                                  select busOT).FirstOrDefault();
            if (findBusOnTrip != null)
            {
                findBusOnTrip.Element("Id").Value = busOnTrip.Id.ToString();
                findBusOnTrip.Element("LineId").Value = busOnTrip.LineId.ToString();
                findBusOnTrip.Element("LicenseNum").Value = busOnTrip.LicenseNum.ToString();
                findBusOnTrip.Element("PlannedTakeOff").Value = busOnTrip.PlannedTakeOff.ToString();
                findBusOnTrip.Element("ActualTakeOff").Value = busOnTrip.ActualTakeOff.ToString();
                findBusOnTrip.Element("PrevStation").Value = busOnTrip.PrevStation.ToString();
                findBusOnTrip.Element("PrevStationAt").Value = busOnTrip.PrevStationAt.ToString();
                findBusOnTrip.Element("NextStationAt").Value = busOnTrip.NextStationAt.ToString();
                findBusOnTrip.Element("Active").Value = busOnTrip.Active.ToString();
                XMLTools.SaveListToXMLElement(BusOnTripsRootElem, busOnTripPath);
            }
            else
                throw new BadIdException(busOnTrip.Id, $"bad busOnTrip id: {busOnTrip.Id}");
        }
        #endregion BusOnTrip 

        #region Trip
        public Trip GetTrip(int tripId)
        {
            XElement TripsRootElem = XMLTools.LoadListFromXMLElement(tripPath);

            Trip trip = (from t in TripsRootElem.Elements()
                                   where (int.Parse(t.Element("Id").Value) == tripId && t.Element("Active").Value == "true")
                                   select new Trip()
                                   {
                                       Id = int.Parse(t.Element("Id").Value),
                                       LineId = int.Parse(t.Element("LineId").Value),
                                       UserName = t.Element("UserName").Value,
                                       InAt = TimeSpan.Parse(t.Element("PlannedTakeOff").Value),
                                       OutAt = TimeSpan.Parse(t.Element("ActualTakeOff").Value),
                                       InStation = int.Parse(t.Element("PrevStation").Value),
                                       outStation = int.Parse(t.Element("PrevStation").Value),
                                       Active = Convert.ToBoolean(t.Element("Active").Value)
                                   }
                                               ).FirstOrDefault();
            if (trip != null)
                return trip;
            else
                throw new BadIdException(tripId, $"bad trip id: {tripId}");
        }
        public IEnumerable<Trip> GetAllTrip()
        {
            XElement TripsRootElem = XMLTools.LoadListFromXMLElement(tripPath);

            return (from t in TripsRootElem.Elements()
                    where (t.Element("Active").Value == "true")
                    select new Trip()
                    {
                        Id = int.Parse(t.Element("Id").Value),
                        LineId = int.Parse(t.Element("LineId").Value),
                        UserName = t.Element("UserName").Value,
                        InAt = TimeSpan.Parse(t.Element("PlannedTakeOff").Value),
                        OutAt = TimeSpan.Parse(t.Element("ActualTakeOff").Value),
                        InStation = int.Parse(t.Element("PrevStation").Value),
                        outStation = int.Parse(t.Element("PrevStation").Value),
                        Active = Convert.ToBoolean(t.Element("Active").Value)
                    });
        }
        public IEnumerable<Trip> GetAllTripBy(Predicate<Trip> predicate)
        {
            XElement TripsRootElem = XMLTools.LoadListFromXMLElement(tripPath);

            return from t in TripsRootElem.Elements()
                   let t1 = new  Trip()
                   {
                       Id = int.Parse(t.Element("Id").Value),
                       LineId = int.Parse(t.Element("LineId").Value),
                       UserName = t.Element("UserName").Value,
                       InAt = TimeSpan.Parse(t.Element("InAt").Value),
                       OutAt = TimeSpan.Parse(t.Element("OutAt").Value),
                       InStation = int.Parse(t.Element("InStation").Value),
                       outStation = int.Parse(t.Element("outStation").Value),
                       Active = Convert.ToBoolean(t.Element("Active").Value)
                   }
                   where predicate(t1)
                   select t1;
        }
        public void AddTrip(Trip trip)
        {
            XElement TripsRootElem = XMLTools.LoadListFromXMLElement(tripPath);
            XElement findTrip = (from t in TripsRootElem.Elements()
                                      where (int.Parse(t.Element("Id").Value) == trip.Id)
                                      select t).FirstOrDefault();
            if (findTrip != null && findTrip.Element("Active").Value == "true")
                throw new BadIdException(trip.Id, "Duplicate trip ID");
            if (findTrip != null && !(findTrip.Element("Active").Value == "true"))
            {
                XElement tripElem = new XElement("Trip",
                                      new XElement("Id", trip.Id),
                                      new XElement("LineId", trip.LineId),
                                      new XElement("UserName", trip.UserName),
                                      new XElement("InAt", trip.InAt),
                                      new XElement("OutAt", trip.OutAt),
                                      new XElement("InStation", trip.InStation),
                                      new XElement("outStation", trip.outStation),
                                      new XElement("Active", trip.Active));
                TripsRootElem.Add(tripElem);
            }
            XMLTools.SaveListToXMLElement(TripsRootElem, tripPath);

        }
        public void DeleteTrip(int tripId)
        {
            XElement TripsRootElem = XMLTools.LoadListFromXMLElement(tripPath);
            XElement trip = (from t in TripsRootElem.Elements()
                                  where (int.Parse(t.Element("Id").Value) == tripId)
                                  select t).FirstOrDefault();
            if (trip != null)
            {
                trip.Element("Active").Value = "true";//not sure
                XMLTools.SaveListToXMLElement(TripsRootElem, tripPath);
            }

            else
                throw new BadIdException(tripId, $"bad trip id: {tripId}");
        }
        public void UpdateTrip(Trip trip)
        {
            XElement TripsRootElem = XMLTools.LoadListFromXMLElement(tripPath);
            XElement findTrip = (from t in TripsRootElem.Elements()
                                      where (int.Parse(t.Element("Id").Value) == trip.Id && t.Element("Active").Value == "true")
                                      select t).FirstOrDefault();
            if (findTrip != null)
            {
                findTrip.Element("Id").Value = trip.Id.ToString();
                findTrip.Element("LineId").Value = trip.LineId.ToString();
                findTrip.Element("UserName").Value = trip.UserName.ToString();
                findTrip.Element("InAt").Value = trip.InAt.ToString();
                findTrip.Element("OutAt").Value = trip.OutAt.ToString();
                findTrip.Element("InStation").Value = trip.InStation.ToString();
                findTrip.Element("outStation").Value = trip.outStation.ToString();
                findTrip.Element("Active").Value = trip.Active.ToString();
                XMLTools.SaveListToXMLElement(TripsRootElem, tripPath);
            }
            else
                throw new BadIdException(trip.Id, $"bad trip id: {trip.Id}");
        }
        #endregion Trip


    }
}
