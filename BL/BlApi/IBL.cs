using System;
using System.Collections.Generic;
using System.Text;
using BL.BO;

namespace BL.BlApi
{
    public interface IBL
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
        Line GetLine(int busId);
        IEnumerable<Line> GetAllLines();
        IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate);
        void AddLine(Line line);
        void UpdateLine(Line line);
        void DeleteLine(int id);
        #endregion Line

        #region Station
        Station GetStations(int stationId);
        IEnumerable<Station> GetAllStations();
        IEnumerable<Station> GetAllStationsBy(Predicate<Station> predicate);
        void AddStation(Station station);
        void UpdateStation(Station station);
        void DeleteStation(int id);
        #endregion Station

    }
}
