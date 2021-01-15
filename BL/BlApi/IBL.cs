using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLApi
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
        int AddLine(Line line);
        void UpdateLine(Line line);
        void DeleteLine(int id);
        void AddStationToLine(Line line, LineStation station, int index);
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
