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
            Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == busId);

            if (bus != null)
                return bus.Clone();
            else
                throw new BadIdException(busId, $"bad bus id: {busId}");
        }
        public IEnumerable<Bus> GetAllBuses()
        {
            return from bus in DataSource.ListBuses
                   select bus.Clone();
        }
        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
        {
            return from bus in DataSource.ListBuses.FindAll(predicate)
                   select bus.Clone();
        }
        public void AddBus(Bus bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum) != null)
                throw new BadIdException(bus.LicenseNum, "Duplicate bus ID");
            DataSource.ListBuses.Add(bus.Clone());
        }
        public void DeleteBus(int busId)
        {
            Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == busId);
            if (bus != null)
                DataSource.ListBuses.Remove(bus);
            else
                throw new BadIdException(busId, $"bad bus id: {busId}");
        }
        public void UpdateBus(Bus bus)
        {
            Bus findBus = DataSource.ListBuses.Find(b => b.LicenseNum == bus.LicenseNum);

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
            Line line = DataSource.ListLines.Find(l => l.Id == lineId);

            if (line != null)
                return line.Clone();
            else
                throw new BadIdException(lineId, $"bad line id: {lineId}");
        }
        public IEnumerable<Line> GetAllLines()
        {
            return from line in DataSource.ListLines
                   select line.Clone();
        }
        public IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate)
        {
            return from line in DataSource.ListLines.FindAll(predicate)
                   select line.Clone();
        }
        public void AddLine(Line line)
        {
            if (DataSource.ListLines.FirstOrDefault(l => l.Id == line.Id) != null)
                throw new BadIdException(line.Id, "Duplicate line ID");
            DataSource.ListLines.Add(line.Clone());
        }
        public void DeleteLine(int lineId)
        {
            Line line = DataSource.ListLines.Find(l => l.Id == lineId);
            if (line != null)
                DataSource.ListLines.Remove(line);
            else
                throw new BadIdException(lineId, $"bad line id: {lineId}");
        }
        public void UpdateLine(Line line)
        {
            Line findLine = DataSource.ListLines.Find(l => l.Id == line.Id);

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
            Station station = DataSource.ListStations.Find(s => s.Code == stationId);

            if (station != null)
                return station.Clone();
            else
                throw new BadIdException(stationId, $"bad station id: {stationId}");
        }
        public IEnumerable<Station> GetAllStations()
        {
            return from station in DataSource.ListStations
                   select station.Clone();
        }
        public IEnumerable<Station> GetAllStationBy(Predicate<Station> predicate)
        {
            return from station in DataSource.ListStations.FindAll(predicate)
                   select station.Clone();
        }
        public void AddStation(Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(s => s.Code == station.Code) != null)
                throw new BadIdException(station.Code, "Duplicate station ID");
            DataSource.ListStations.Add(station.Clone());
        }
        public void DeleteStation(int stationId)
        {
            Station station = DataSource.ListStations.Find(s => s.Code == stationId);
            if (station != null)
                DataSource.ListStations.Remove(station);
            else
                throw new BadIdException(stationId, $"bad station id: {stationId}");
        }
        public void UpdateStation(Station station)
        {
            Station findStation = DataSource.ListStations.Find(s => s.Code == station.Code);

            if (findStation != null)
            {
                DataSource.ListStations.Remove(findStation);
                DataSource.ListStations.Add(station.Clone());
            }
            else
                throw new BadIdException(station.Code, $"bad station id: {station.Code}");
        }
        #endregion Station

        #region BusOnTrip
        public BusOnTrip GetBusOnTrip(int busOnTripId)
        {
            BusOnTrip busOnTrip = DataSource.ListBusesOnTrip.Find(bt => bt.Id == busOnTripId);

            if (busOnTrip != null)
                return busOnTrip.Clone();
            else
                throw new BadIdException(busOnTripId, $"bad busOnTrip id: {busOnTripId}");
        }
        public IEnumerable<BusOnTrip> GetAllBusOnTrip()
        {
            return from busOnTrip in DataSource.ListBusesOnTrip
                   select busOnTrip.Clone();
        }
        public IEnumerable<BusOnTrip> GetAllBusOnTripBy(Predicate<BusOnTrip> predicate)
        {
            return from busOnTrip in DataSource.ListBusesOnTrip.FindAll(predicate)
                   select busOnTrip.Clone();
        }
        public void AddBusOnTrip(BusOnTrip busOnTrip)
        {
            if (DataSource.ListBusesOnTrip.FirstOrDefault(bt => bt.Id == busOnTrip.Id) != null)
                throw new BadIdException(busOnTrip.Id, "Duplicate busOnTrip ID");
            DataSource.ListBusesOnTrip.Add(busOnTrip.Clone());
        }
        public void DeleteBusOnTrip(int busOnTripId)
        {
            BusOnTrip busOnTrip = DataSource.ListBusesOnTrip.Find(bt => bt.Id == busOnTripId);
            if (busOnTrip != null)
                DataSource.ListBusesOnTrip.Remove(busOnTrip);
            else
                throw new BadIdException(busOnTripId, $"bad busOnTrip id: {busOnTripId}");
        }
        public void UpdateBusOnTrip(BusOnTrip busOnTrip)
        {
            BusOnTrip findBusOnTrip = DataSource.ListBusesOnTrip.Find(bt => bt.Id == busOnTrip.Id);

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
            LineStation lineStation = DataSource.ListLineStations.Find(ls => ls.LineId == lineStationId);

            if (lineStation != null)
                return lineStation.Clone();
            else
                throw new BadIdException(lineStationId, $"bad line station id: {lineStationId}");
        }
        public IEnumerable<LineStation> GetAllLineStation()
        {
            return from lineStation in DataSource.ListLineStations
                   select lineStation.Clone();
        }
        public IEnumerable<LineStation> GetAllLineStationBy(Predicate<LineStation> predicate)
        {
            return from lineStation in DataSource.ListLineStations.FindAll(predicate)
                   select lineStation.Clone();
        }
        public void AddLineStation(LineStation lineStation)
        {
            if (DataSource.ListLineStations.FirstOrDefault(ls => ls.LineId == lineStation.LineId) != null)
                throw new BadIdException(lineStation.LineId, "Duplicate line station ID");
            DataSource.ListLineStations.Add(lineStation.Clone());
        }
        public void DeleteLineStation(int lineStationId)
        {
            LineStation lineStation = DataSource.ListLineStations.Find(ls => ls.LineId == lineStationId);
            if (lineStation != null)
                DataSource.ListLineStations.Remove(lineStation);
            else
                throw new BadIdException(lineStationId, $"bad line station id: {lineStationId}");
        }
        public void UpdateLineStation(LineStation lineStation)
        {
            LineStation findLineStation = DataSource.ListLineStations.Find(ls => ls.LineId == lineStation.LineId);

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
            LineTrip lineTrip = DataSource.ListLineTrips.Find(lt => lt.Id == lineTripId);

            if (lineTrip != null)
                return lineTrip.Clone();
            else
                throw new BadIdException(lineTripId, $"bad line trip id: {lineTripId}");
        }
        public IEnumerable<LineTrip> GetAllLineTrip()
        {
            return from lineTrip in DataSource.ListLineTrips
                   select lineTrip.Clone();
        }
        public IEnumerable<LineTrip> GetAllLineTripBy(Predicate<LineTrip> predicate)
        {
            return from lineTrip in DataSource.ListLineTrips.FindAll(predicate)
                   select lineTrip.Clone();
        }
        public void AddLineTrip(LineTrip lineTrip)
        {
            if (DataSource.ListLineTrips.FirstOrDefault(lt => lt.Id == lineTrip.Id) != null)
                throw new BadIdException(lineTrip.Id, "Duplicate line trip ID");
            DataSource.ListLineTrips.Add(lineTrip.Clone());
        }
        public void DeleteLineTrip(int lineTripId)
        {
            LineTrip lineTrip = DataSource.ListLineTrips.Find(lt => lt.Id == lineTripId);
            if (lineTrip != null)
                DataSource.ListLineTrips.Remove(lineTrip);
            else
                throw new BadIdException(lineTripId, $"bad line trip id: {lineTripId}");
        }
        public void UpdateLineTrip(LineTrip lineTrip)
        {
            LineTrip findLineTrip = DataSource.ListLineTrips.Find(lt => lt.Id == lineTrip.Id);

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
            Trip trip = DataSource.ListRrips.Find(t => t.Id == tripId);

            if (trip != null)
                return trip.Clone();
            else
                throw new BadIdException(tripId, $"bad trip id: {tripId}");
        }
        public IEnumerable<Trip> GetAllTrip()
        {
            return from trip in DataSource.ListRrips
                   select trip.Clone();
        }
        public IEnumerable<Trip> GetAllTripBy(Predicate<Trip> predicate)
        {
            return from trip in DataSource.ListRrips.FindAll(predicate)
                   select trip.Clone();
        }
        public void AddTrip(Trip trip)
        {
            if (DataSource.ListRrips.FirstOrDefault(t => t.Id == trip.Id) != null)
                throw new BadIdException(trip.Id, "Duplicate trip ID");
            DataSource.ListRrips.Add(trip.Clone());
        }
        public void DeleteTrip(int tripId)
        {
            Trip trip = DataSource.ListRrips.Find(t => t.Id == tripId);
            if (trip != null)
                DataSource.ListRrips.Remove(trip);
            else
                throw new BadIdException(tripId, $"bad trip id: {tripId}");
        }
        public void UpdateTrip(Trip trip)
        {
            Trip findTrip = DataSource.ListRrips.Find(t => t.Id == trip.Id);

            if (findTrip != null)
            {
                DataSource.ListRrips.Remove(findTrip);
                DataSource.ListRrips.Add(trip.Clone());
            }
            else
                throw new BadIdException(trip.Id, $"bad trip id: {trip.Id}");
        }
        #endregion Trip

        #region User
        public User GetUser(string userName)
        {
            User user = DataSource.ListUsers.Find(u => u.UserName == userName);

            if (user != null)
                return user.Clone();
            else
                throw new BadUserNameException(userName, $"bad user name: {userName}");
        }
        public IEnumerable<User> GetAllUsers()
        {
            return from user in DataSource.ListUsers
                   select user.Clone();
        }
        public IEnumerable<User> GetAllUsersBy(Predicate<User> predicate)
        {
            return from user in DataSource.ListUsers.FindAll(predicate)
                   select user.Clone();
        }
        public void AddUser(User user)
        {
            if (DataSource.ListUsers.FirstOrDefault(u => u.UserName == user.UserName) != null)
                throw new BadUserNameException(user.UserName, "Duplicate user name");
            DataSource.ListUsers.Add(user.Clone());
        }
        public void DeleteUser(string userName)
        {
            User user = DataSource.ListUsers.Find(u => u.UserName == userName);
            if (user != null)
                DataSource.ListUsers.Remove(user);
            else
                throw new BadUserNameException(userName, $"bad user name: {userName}");
        }
        public void UpdateUser(User user)
        {
            User findUser = DataSource.ListUsers.Find(u => u.UserName == user.UserName);

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
            AdjacentStations adjacentStations = DataSource.ListAdjacentStations.Find(ads => ads.Station1 == adjacentStationsId1 && ads.Station2 == adjacentStationsId2);

            if (adjacentStations != null)
                return adjacentStations.Clone();
            else
                throw new BadAdjacentStationsException(adjacentStationsId1, adjacentStationsId2, $"bad adjacent stations id: {adjacentStationsId1}, {adjacentStationsId2}");
        }
        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            return from adjacentStations in DataSource.ListAdjacentStations
                   select adjacentStations.Clone();
        }
        public IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> predicate)
        {
            return from adjacentStations in DataSource.ListAdjacentStations.FindAll(predicate)
                   select adjacentStations.Clone();
        }
        public void AddAdjacentStations(AdjacentStations adjacentStations)
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(ads => ads.Station1 == adjacentStations.Station1 && ads.Station2 == adjacentStations.Station2) != null)
                throw new BadAdjacentStationsException(adjacentStations.Station1, adjacentStations.Station2, "Duplicate adjacentStations");
            DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
        }
        public void DeleteAdjacentStations(int adjacentStationsId1, int adjacentStationsId2)
        {
            AdjacentStations adjacentStations = DataSource.ListAdjacentStations.Find(ads => ads.Station1 == adjacentStationsId1 && ads.Station2 == adjacentStationsId2);
            if (adjacentStations != null)
                DataSource.ListAdjacentStations.Remove(adjacentStations);
            else
                throw new BadAdjacentStationsException(adjacentStationsId1, adjacentStationsId2, $"bad adjacent stations id: {adjacentStationsId1}, {adjacentStationsId2}");
        }
        public void UpdateAdjacentStations(AdjacentStations adjacentStations)
        {
            AdjacentStations findAdjacentStations = DataSource.ListAdjacentStations.Find(ads => ads.Station1 == adjacentStations.Station1 && ads.Station1 == adjacentStations.Station1);

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
