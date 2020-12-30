using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using DO;
using DS;

namespace Dal
{
    sealed class DALObject : IDal // singletone
    {
        #region singelton
        static readonly DALObject instance = new DALObject();
        static DALObject() { }// static ctor to ensure instance init is done just before first usage
        private DALObject() { } 
        public static DALObject Instance { get => instance; }// The public Instance property to use
        #endregion

        //Implement IDal methods, CRUD

        #region Bus
        public Bus GetBus(int busId)
        {
            Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == busId && b.Active);

            if (bus != null)
                return bus.Clone();
            else
                throw new BadIdException(busId, $"bad bus id: {busId}");
        }
        public IEnumerable<Bus> GetAllBuses()
        {
            return from bus in DataSource.ListBuses.FindAll(b => b.Active)
                   select bus.Clone();
        }
        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            return from bus in DataSource.ListBuses.FindAll(b => b.Active).FindAll(predicate)
                   select bus.Clone();
        }
        public void AddBus(Bus bus)
        {
            Bus findBus = DataSource.ListBuses.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum);
            if (findBus != null && findBus.Active)
                throw new BadIdException(bus.LicenseNum, "Duplicate bus ID");
            if (findBus != null && !findBus.Active)
                DataSource.ListBuses.Remove(findBus);
            DataSource.ListBuses.Add(bus.Clone());
        }
        public void DeleteBus(int busId)
        {
            Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == busId);
            if (bus != null)
                bus.Active = false;
            else
                throw new BadIdException(busId, $"bad bus id: {busId}");
        }
        public void UpdateBus(Bus bus)
        {
            Bus findBus = DataSource.ListBuses.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum && b.Active);

            if (findBus != null)
            {
                DataSource.ListBuses.Remove(findBus);
                DataSource.ListBuses.Add(bus.Clone());
            }
            else
                throw new BadIdException(bus.LicenseNum, $"bad bus id: {bus.LicenseNum}");
        }
        #endregion Bus

        #region Line
        public Line GetLine(int lineId)
        {
            Line line = DataSource.ListLines.Find(l => l.Id == lineId && l.Active);

            if (line != null)
                return line.Clone();
            else
                throw new BadIdException(lineId, $"bad line id: {lineId}");
        }
        public IEnumerable<Line> GetAllLines()
        {
            return from line in DataSource.ListLines.FindAll(l => l.Active)
                   select line.Clone();
        }
        public IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate)
        {
            return from line in DataSource.ListLines.FindAll(l => l.Active).FindAll(predicate)
                   select line.Clone();
        }
        public void AddLine(Line line)
        {
            Line findLine = DataSource.ListLines.FirstOrDefault(l => l.Id == line.Id);
            if (findLine != null && findLine.Active)
                throw new BadIdException(line.Id, "Duplicate line ID");
            if (findLine != null && !findLine.Active)
                DataSource.ListLines.Remove(findLine);
            DataSource.ListLines.Add(line.Clone());
        }
        public void DeleteLine(int lineId)
        {
            Line line = DataSource.ListLines.Find(l => l.Id == lineId);
            if (line != null)
                line.Active = false;
            else
                throw new BadIdException(lineId, $"bad line id: {lineId}");
        }
        public void UpdateLine(Line line)
        {
            Line findLine = DataSource.ListLines.FirstOrDefault(l => l.Id == line.Id && l.Active);

            if (findLine != null)
            {
                DataSource.ListLines.Remove(findLine);
                DataSource.ListLines.Add(line.Clone());
            }
            else
                throw new BadIdException(line.Id, $"bad line id: {line.Id}");
        }
        #endregion Line

        #region Station
        public Station GetStation(int stationId)
        {
            Station station = DataSource.ListStations.Find(s => s.Code == stationId && s.Active);

            if (station != null)
                return station.Clone();
            else
                throw new BadIdException(stationId, $"bad station code: {stationId}");
        }
        public IEnumerable<Station> GetAllStations()
        {
            return from station in DataSource.ListStations.FindAll(s => s.Active)
                   select station.Clone();
        }
        public IEnumerable<Station> GetAllStationBy(Predicate<Station> predicate)
        {
            return from station in DataSource.ListStations.FindAll(s => s.Active).FindAll(predicate)
                   select station.Clone();
        }
        public void AddStation(Station station)
        {
            Station findStation = DataSource.ListStations.FirstOrDefault(s => s.Code == station.Code);
            if (findStation != null && findStation.Active)
                throw new BadIdException(station.Code, "Duplicate station Code");
            if (findStation != null && !findStation.Active)
                DataSource.ListStations.Remove(findStation);
            DataSource.ListStations.Add(station.Clone());
        }
        public void DeleteStation(int stationId)
        {
            Station station = DataSource.ListStations.Find(s => s.Code == stationId);
            if (station != null)
                station.Active = false;
            else
                throw new BadIdException(stationId, $"bad station code: {stationId}");
        }
        public void UpdateStation(Station station)
        {
            Station findStation = DataSource.ListStations.FirstOrDefault(s => s.Code == station.Code && s.Active);

            if (findStation != null)
            {
                DataSource.ListStations.Remove(findStation);
                DataSource.ListStations.Add(station.Clone());
            }
            else
                throw new BadIdException(station.Code, $"bad station code: {station.Code}");
        }
        #endregion Station

        #region BusOnTrip
        public BusOnTrip GetBusOnTrip(int busOnTripId)
        {
            BusOnTrip busOnTrip = DataSource.ListBusesOnTrip.Find(bt => bt.Id == busOnTripId && bt.Active);

            if (busOnTrip != null)
                return busOnTrip.Clone();
            else
                throw new BadIdException(busOnTripId, $"bad busOnTrip id: {busOnTripId}");
        }
        public IEnumerable<BusOnTrip> GetAllBusOnTrip()
        {
            return from busOnTrip in DataSource.ListBusesOnTrip.FindAll(bt => bt.Active)
                   select busOnTrip.Clone();
        }
        public IEnumerable<BusOnTrip> GetAllBusOnTripBy(Predicate<BusOnTrip> predicate)
        {
            return from busOnTrip in DataSource.ListBusesOnTrip.FindAll(bt => bt.Active).FindAll(predicate)
                   select busOnTrip.Clone();
        }
        public void AddBusOnTrip(BusOnTrip busOnTrip)
        {
            BusOnTrip findBusOnTrip = DataSource.ListBusesOnTrip.FirstOrDefault(bt => bt.Id == busOnTrip.Id);
            if (findBusOnTrip != null && findBusOnTrip.Active)
                throw new BadIdException(busOnTrip.Id, "Duplicate busOnTrip ID");
            if (findBusOnTrip != null && !findBusOnTrip.Active)
                DataSource.ListBusesOnTrip.Remove(findBusOnTrip);
            DataSource.ListBusesOnTrip.Add(busOnTrip.Clone());
        }
        public void DeleteBusOnTrip(int busOnTripId)
        {
            BusOnTrip busOnTrip = DataSource.ListBusesOnTrip.Find(bt => bt.Id == busOnTripId);
            if (busOnTrip != null)
                busOnTrip.Active = false;
            else
                throw new BadIdException(busOnTripId, $"bad busOnTrip id: {busOnTripId}");
        }
        public void UpdateBusOnTrip(BusOnTrip busOnTrip)
        {
            BusOnTrip findBusOnTrip = DataSource.ListBusesOnTrip.FirstOrDefault(bt => bt.Id == busOnTrip.Id && bt.Active);

            if (findBusOnTrip != null)
            {
                DataSource.ListBusesOnTrip.Remove(findBusOnTrip);
                DataSource.ListBusesOnTrip.Add(busOnTrip.Clone());
            }
            else
                throw new BadIdException(busOnTrip.Id, $"bad busOnTrip id: {busOnTrip.Id}");
        }
        #endregion BusOnTrip

        #region LineStation
        public LineStation GetLineStation(int lineStationId)
        {
            LineStation lineStation = DataSource.ListLineStations.Find(ls => ls.LineId == lineStationId && ls.Active);

            if (lineStation != null)
                return lineStation.Clone();
            else
                throw new BadIdException(lineStationId, $"bad line station id: {lineStationId}");
        }
        public IEnumerable<LineStation> GetAllLineStation()
        {
            return from lineStation in DataSource.ListLineStations.FindAll(ls => ls.Active)
                   select lineStation.Clone();
        }
        public IEnumerable<LineStation> GetAllLineStationBy(Predicate<LineStation> predicate)
        {
            return from lineStation in DataSource.ListLineStations.FindAll(ls => ls.Active).FindAll(predicate)
                   select lineStation.Clone();
        }
        public void AddLineStation(LineStation lineStation)
        {
            LineStation findLineStation = DataSource.ListLineStations.FirstOrDefault(ls => ls.LineId == lineStation.LineId);
            if (findLineStation != null && findLineStation.Active)
                throw new BadIdException(lineStation.LineId, "Duplicate line station ID");
            if (findLineStation != null && !findLineStation.Active)
                DataSource.ListLineStations.Remove(findLineStation);
            DataSource.ListLineStations.Add(lineStation.Clone());
        }
        public void DeleteLineStation(int lineStationId)
        {
            LineStation lineStation = DataSource.ListLineStations.Find(ls => ls.LineId == lineStationId);
            if (lineStation != null)
                lineStation.Active = false;
            else
                throw new BadIdException(lineStationId, $"bad line station id: {lineStationId}");
        }
        public void UpdateLineStation(LineStation lineStation)
        {
            LineStation findLineStation = DataSource.ListLineStations.FirstOrDefault(ls => ls.LineId == lineStation.LineId && ls.Active);

            if (findLineStation != null)
            {
                DataSource.ListLineStations.Remove(findLineStation);
                DataSource.ListLineStations.Add(lineStation.Clone());
            }
            else
                throw new BadIdException(lineStation.LineId, $"bad line station id: {lineStation.LineId}");
        }
        #endregion LineStation

        #region LineTrip
        public LineTrip GetLineTrip(int lineTripId)
        {
            LineTrip lineTrip = DataSource.ListLineTrips.Find(lt => lt.Id == lineTripId && lt.Active);

            if (lineTrip != null)
                return lineTrip.Clone();
            else
                throw new BadIdException(lineTripId, $"bad line trip id: {lineTripId}");
        }
        public IEnumerable<LineTrip> GetAllLineTrip()
        {
            return from lineTrip in DataSource.ListLineTrips.FindAll(lt => lt.Active)
                   select lineTrip.Clone();
        }
        public IEnumerable<LineTrip> GetAllLineTripBy(Predicate<LineTrip> predicate)
        {
            return from lineTrip in DataSource.ListLineTrips.FindAll(lt => lt.Active).FindAll(predicate)
                   select lineTrip.Clone();
        }
        public void AddLineTrip(LineTrip lineTrip)
        {
            LineTrip findLineTrip = DataSource.ListLineTrips.FirstOrDefault(lt => lt.Id == lineTrip.Id);
            if (findLineTrip != null && findLineTrip.Active)
                throw new BadIdException(lineTrip.Id, "Duplicate line trip ID");
            if (findLineTrip != null && !findLineTrip.Active)
                DataSource.ListLineTrips.Remove(findLineTrip);
            DataSource.ListLineTrips.Add(lineTrip.Clone());
        }
        public void DeleteLineTrip(int lineTripId)
        {
            LineTrip lineTrip = DataSource.ListLineTrips.Find(lt => lt.Id == lineTripId);
            if (lineTrip != null)
                lineTrip.Active = false;
            else
                throw new BadIdException(lineTripId, $"bad line trip id: {lineTripId}");
        }
        public void UpdateLineTrip(LineTrip lineTrip)
        {
            LineTrip findLineTrip = DataSource.ListLineTrips.FirstOrDefault(lt => lt.Id == lineTrip.Id && lt.Active);

            if (findLineTrip != null)
            {
                DataSource.ListLineTrips.Remove(findLineTrip);
                DataSource.ListLineTrips.Add(lineTrip.Clone());
            }
            else
                throw new BadIdException(lineTrip.Id, $"bad line trip id: {lineTrip.Id}");
        }
        #endregion LineTrip

        #region Trip
        public Trip GetTrip(int tripId)
        {
            Trip trip = DataSource.ListTrips.Find(t => t.Id == tripId && t.Active);

            if (trip != null)
                return trip.Clone();
            else
                throw new BadIdException(tripId, $"bad trip id: {tripId}");
        }
        public IEnumerable<Trip> GetAllTrip()
        {
            return from trip in DataSource.ListTrips.FindAll(t => t.Active)
                   select trip.Clone();
        }
        public IEnumerable<Trip> GetAllTripBy(Predicate<Trip> predicate)
        {
            return from trip in DataSource.ListTrips.FindAll(t => t.Active).FindAll(predicate)
                   select trip.Clone();
        }
        public void AddTrip(Trip trip)
        {
            Trip findTrip = DataSource.ListTrips.FirstOrDefault(t => t.Id == trip.Id);
            if (findTrip != null && findTrip.Active)
                throw new BadIdException(trip.Id, "Duplicate trip ID");
            if (findTrip != null && !findTrip.Active)
                DataSource.ListTrips.Remove(findTrip);
            DataSource.ListTrips.Add(trip.Clone());
        }
        public void DeleteTrip(int tripId)
        {
            Trip trip = DataSource.ListTrips.Find(t => t.Id == tripId);
            if (trip != null)
                trip.Active = false;
            else
                throw new BadIdException(tripId, $"bad trip id: {tripId}");
        }
        public void UpdateTrip(Trip trip)
        {
            Trip findTrip = DataSource.ListTrips.FirstOrDefault(t => t.Id == trip.Id && t.Active);

            if (findTrip != null)
            {
                DataSource.ListTrips.Remove(findTrip);
                DataSource.ListTrips.Add(trip.Clone());
            }
            else
                throw new BadIdException(trip.Id, $"bad trip id: {trip.Id}");
        }
        #endregion Trip

        #region User
        public User GetUser(string userName)
        {
            User user = DataSource.ListUsers.Find(u => u.UserName == userName && u.Active);

            if (user != null)
                return user.Clone();
            else
                throw new BadUserNameException(userName, $"bad user name: {userName}");
        }
        public IEnumerable<User> GetAllUsers()
        {
            return from user in DataSource.ListUsers.FindAll(u => u.Active)
                   select user.Clone();
        }
        public IEnumerable<User> GetAllUsersBy(Predicate<User> predicate)
        {
            return from user in DataSource.ListUsers.FindAll(u => u.Active).FindAll(predicate)
                   select user.Clone();
        }
        public void AddUser(User user)
        {
            User findUser = DataSource.ListUsers.FirstOrDefault(u => u.UserName == user.UserName);
            if (findUser != null && findUser.Active)
                throw new BadUserNameException(user.UserName, "Duplicate user name");
            if (findUser != null && !findUser.Active)
                DataSource.ListUsers.Remove(findUser);
            DataSource.ListUsers.Add(user.Clone());
        }
        public void DeleteUser(string userName)
        {
            User user = DataSource.ListUsers.Find(u => u.UserName == userName);
            if (user != null)
                user.Active = false;
            else
                throw new BadUserNameException(userName, $"bad user name: {userName}");
        }
        public void UpdateUser(User user)
        {
            User findUser = DataSource.ListUsers.FirstOrDefault(u => u.UserName == user.UserName && u.Active);

            if (findUser != null)
            {
                DataSource.ListUsers.Remove(findUser);
                DataSource.ListUsers.Add(user.Clone());
            }
            else
                throw new BadUserNameException(user.UserName, $"bad user id: {user.UserName}");
        }
        #endregion User

        #region AdjacentStations
        public AdjacentStations GetAdjacentStations(int adjacentStationsId1, int adjacentStationsId2)
        {
            AdjacentStations adjacentStations = DataSource.ListAdjacentStations.Find(ads => ads.Station1 == adjacentStationsId1 && ads.Station2 == adjacentStationsId2 && ads.Active);

            if (adjacentStations != null)
                return adjacentStations.Clone();
            else
                throw new BadAdjacentStationsException(adjacentStationsId1, adjacentStationsId2, $"bad adjacent stations id: {adjacentStationsId1}, {adjacentStationsId2}");
        }
        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            return from adjacentStations in DataSource.ListAdjacentStations.FindAll(ads => ads.Active)
                   select adjacentStations.Clone();
        }
        public IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> predicate)
        {
            return from adjacentStations in DataSource.ListAdjacentStations.FindAll(ads => ads.Active).FindAll(predicate)
                   select adjacentStations.Clone();
        }
        public void AddAdjacentStations(AdjacentStations adjacentStations)
        {
            AdjacentStations findAdjacentStations = DataSource.ListAdjacentStations.FirstOrDefault(ads => ads.Station1 == adjacentStations.Station1 && ads.Station2 == adjacentStations.Station2);
            if (findAdjacentStations != null && findAdjacentStations.Active)
                throw new BadAdjacentStationsException(adjacentStations.Station1, adjacentStations.Station2, "Duplicate adjacentStations");
            if (findAdjacentStations != null && !findAdjacentStations.Active)
                DataSource.ListAdjacentStations.Remove(findAdjacentStations);
            DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
        }
        public void DeleteAdjacentStations(int adjacentStationsId1, int adjacentStationsId2)
        {
            AdjacentStations adjacentStations = DataSource.ListAdjacentStations.Find(ads => ads.Station1 == adjacentStationsId1 && ads.Station2 == adjacentStationsId2);
            if (adjacentStations != null)
                adjacentStations.Active = false;
            else
                throw new BadAdjacentStationsException(adjacentStationsId1, adjacentStationsId2, $"bad adjacent stations id: {adjacentStationsId1}, {adjacentStationsId2}");
        }
        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            AdjacentStations findAdjacentStations = DataSource.ListAdjacentStations.FirstOrDefault(ads => ads.Station1 == adjacentStations.Station1 && ads.Station1 == adjacentStations.Station1 && ads.Active);

            if (findAdjacentStations != null)
            {
                DataSource.ListAdjacentStations.Remove(findAdjacentStations);
                DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
            }
            else
                throw new BadAdjacentStationsException(adjacentStations.Station1, adjacentStations.Station2, $"bad adjacent stations id: {adjacentStations.Station1}, {adjacentStations.Station2}");
        }
        #endregion AdjacentStations
    }
}
