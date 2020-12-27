using System;
using System.Collections.Generic;
using System.Text;
using DO;

namespace DalApi
{
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
        Bus GetLine(int lineId);
        IEnumerable<Bus> GetAllLines();
        IEnumerable<Bus> GetAllLinesBy(Predicate<Bus> predicate);
        void AddLine(Line line);
        void UpdateLine(Line line);
        void DeleteLine(int id);
        #endregion Line

        #region Station
        Bus GetStation(int lineId);
        IEnumerable<Bus> GetAllStations();
        IEnumerable<Bus> GetAllStationBy(Predicate<Bus> predicate);
        void AddStation(Station station);
        void UpdateStation(Station station);
        void DeleteStation(int id);
        #endregion Line



    }
}
