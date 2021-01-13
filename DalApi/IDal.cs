using System;
using System.Collections.Generic;
using System.Text;
using DO;

namespace DalApi
{
    //CRUD Logic:
    // Create - add new instance
    // Request - ask for an instance or for a collection
    // Update - update properties of an instance
    // Delete - delete an instance
    public interface IDal
    {
        #region Bus
        Bus GetBus(int busId);
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
        void AddBus(Bus bus);
        void UpdateBus(Bus bus);
        void DeleteBus(int id);
        #endregion Bus

        #region Line
        Line GetLine(int lineId);
        IEnumerable<Line> GetAllLines();
        IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate);
        int AddLine(Line line);
        void UpdateLine(Line line);
        void DeleteLine(int lineId);
        #endregion Line

        #region Station
        Station GetStation(int stationId);
        IEnumerable<Station> GetAllStations();
        IEnumerable<Station> GetAllStationBy(Predicate<Station> predicate);
        void AddStation(Station station);
        void UpdateStation(Station station);
        void DeleteStation(int stationId);
        #endregion Line

        #region BusOnTrip
        BusOnTrip GetBusOnTrip(int busOnTripId);
        IEnumerable<BusOnTrip> GetAllBusOnTrip();
        IEnumerable<BusOnTrip> GetAllBusOnTripBy(Predicate<BusOnTrip> predicate);
        void AddBusOnTrip(BusOnTrip busOnTrip);
        void UpdateBusOnTrip(BusOnTrip busOnTrip);
        void DeleteBusOnTrip(int busOnTripId);
        #endregion

        #region LineStation
        LineStation GetLineStation(int lineId, int station);
        IEnumerable<LineStation> GetAllLineStation();
        IEnumerable<LineStation> GetAllLineStationBy(Predicate<LineStation> predicate);
        void AddLineStation(LineStation lineStation);
        void UpdateLineStation(LineStation lineStation);
        void DeleteLineStation(int lineId, int station);
        bool isLineStationExistForLine(int lineId, int lineStationsId);
        #endregion

        #region LineTrip
        LineTrip GetLineTrip(int lineTripId);
        IEnumerable<LineTrip> GetAllLineTrip();
        IEnumerable<LineTrip> GetAllLineTripBy(Predicate<LineTrip> predicate);
        void AddLineTrip(LineTrip lineTrip);
        void UpdateLineTrip(LineTrip lineTrip);
        void DeleteLineTrip(int lineTripId);
        #endregion

        #region Trip
        Trip GetTrip(int tripId);
        IEnumerable<Trip> GetAllTrip();
        IEnumerable<Trip> GetAllTripBy(Predicate<Trip> predicate);
        void AddTrip(Trip trip);
        void UpdateTrip(Trip trip);
        void DeleteTrip(int tripId);
        #endregion

        #region User
        User GetUser(string userName);
        IEnumerable<User> GetAllUsers();
        IEnumerable<User> GetAllUsersBy(Predicate<User> predicate);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(string userName);
        #endregion

        #region AdjacentStations
        AdjacentStations GetAdjacentStations(int adjacentStationsId1, int adjacentStationsId2);
        IEnumerable<AdjacentStations> GetAllAdjacentStations();
        IEnumerable<AdjacentStations> GetAllAdjacentStationsBy(Predicate<AdjacentStations> predicate);
        void AddAdjacentStations(AdjacentStations adjacentStations);
        void UpdateAdjacentStations(AdjacentStations adjacentStations);
        void DeleteAdjacentStations(int adjacentStationsId1, int adjacentStationsId2);
        #endregion
    }
}
