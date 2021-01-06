using BL.BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BL//
{
    public class BLImp : BlApi.IBL
    {
        #region singelton
        static readonly BLImp instance = new BLImp();
        static BLImp() { }// static ctor to ensure instance init is done just before first usage
        private BLImp() { }
        public static BLImp Instance { get => instance; }// The public Instance property to use
        #endregion

        #region Bus
        IDal dl = DalFactory.GetDal();
        Bus busDoBoAdapter(DO.Bus busDO)
        {
            Bus busBO = new Bus();

            busDO.CopyPropertiesTo(busBO);

            return busBO;
        }

        public BO.Bus GetBus(int id)
        {
            DO.Bus busDO;
            try
            {
                busDO = dl.GetBus(id);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.BadIdException(ex, $"License number does not exist: {id}", id);
            }
            return busDoBoAdapter(busDO);
        }

        public IEnumerable<BO.Bus> GetAllBuses()
        {

            return from bus in dl.GetAllBuses()
                   select busDoBoAdapter(bus);
        }
        public IEnumerable<BO.Bus> GetAllBusesBy(Predicate<BO.Bus> predicate)
        {
            IEnumerable<DO.Bus> doBuses = dl.GetAllBusesBy((b) => predicate(busDoBoAdapter(b)));
            return doBuses.Select((b) => busDoBoAdapter(b));
        }
        public void AddBus(BO.Bus bus)
        {
            ValidateBusFiledsCheck(bus);
            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO);
            try
            {
                dl.AddBus(busDO);
            }
            catch (DO.BadIdException ex)
            {
                throw new BadIdException(ex, "Duplicate bus ID", bus.LicenseNum);
            }

        }
        public void ValidateBusFiledsCheck(BO.Bus bus)
        {
            DateTime tempDate2018 = new DateTime(1, 1, 2018);
            DateTime tempDate2021 = DateTime.Now;
            DateTime tempDate1985 = new DateTime(1, 1, 1985);
            if (bus.LicenseNum > 100000000 || bus.LicenseNum < 1000000)
                throw new BO.BadIdException(bus.LicenseNum, $"bad bus License number: {bus.LicenseNum}");
            if (bus.LicenseNum < 10000000 || bus.LicenseNum > 1000000)
                if (bus.FromDate > tempDate2018 || bus.FromDate < tempDate1985)
                    throw new BO.BadTimeException(bus.FromDate, $"The time does not match the license plate: {bus.FromDate}");
            if (bus.LicenseNum < 100000000 || bus.LicenseNum > 10000000)
                if (bus.FromDate < tempDate2018 || bus.FromDate > tempDate2021)
                    throw new BO.BadTimeException(bus.FromDate, $"The time does not match the license plate: {bus.FromDate}");
            if (bus.TotalTrip < 0)
                throw new BO.BadTotalTripException(bus.TotalTrip, $"The Total Trip is lower then 0: {bus.TotalTrip}");
            if (bus.FuelRemain < 0 || bus.FuelRemain > 1200)
                throw new BO.BadFuelException(bus.FuelRemain, $"Wrong input for Fuel: {bus.FuelRemain}");
            if (bus.KilometersSinceLastTreatment > bus.TotalTrip || bus.KilometersSinceLastTreatment < (bus.TotalTrip - 20000))
                throw new BO.BadKilometersException(bus.KilometersSinceLastTreatment, $"Wrong input for Fuel: {bus.KilometersSinceLastTreatment}");
            if (bus.KilometersAtLastRefueling > bus.TotalTrip || bus.KilometersAtLastRefueling < (bus.TotalTrip - 1200))
                throw new BO.BadKilometersException(bus.KilometersAtLastRefueling, $"Wrong input for Fuel: {bus.KilometersAtLastRefueling}");
        }
        public void UpdateBus(BO.Bus bus)//?
        {
            ValidateBusFiledsCheck(bus);
            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO);
            try
            {
                dl.UpdateBus(busDO);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.BadIdException(ex, $"bad bus id: {busDO.LicenseNum}", busDO.LicenseNum);
            }

        }

        public void DeleteBus(int id)
        {

            try
            {
                dl.GetBus(id);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.BadIdException(ex, $"License number does not exist: {id}", id);
            }

        }
        #endregion
        #region Line

        Line LineDoBoAdapter(DO.Line LineDO)
        {
            Line LineBO = new Line();

            LineDO.CopyPropertiesTo(LineBO);

            return LineBO;
        }
        public BO.Line GetLine(int lineId)
        {
            DO.Line LineDO;
            try
            {
                LineDO = dl.GetLine(lineId);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.BadIdException(ex, $"Line Number does not exist: {lineId}", lineId);
            }
            return LineDoBoAdapter(LineDO);
        }
        public IEnumerable<BO.Line> GetAllLines()
        {

            return from line in dl.GetAllLines()
                   select LineDoBoAdapter(line);
        }
        public IEnumerable<BO.Line> GetAllLinesBy(Predicate<BO.Line> predicate)
        {
            IEnumerable<DO.Line> doLines = dl.GetAllLinesBy((l) => predicate(LineDoBoAdapter(l)));
            return doLines.Select((l) => LineDoBoAdapter(l));
        }
        public void AddLine(BO.Line line)
        {
            if (line.LineId < 0)
                throw new BadIdException(line.LineId, " Line ID must be bigger then 0");
            DO.Line lineDO = new DO.Line();
            line.CopyPropertiesTo(lineDO);
            try
            {
                dl.AddLine(lineDO);
            }
            catch (DO.BadIdException ex)
            {
                throw new BadIdException(ex, "Duplicate Line ID", line.LineId);
            }

        }
        public void UpdateLine(BO.Line line)
        {
            if (line.LineId < 0)
                throw new BadIdException(line.LineId, " Line ID must be bigger then 0");
            DO.Line lineDO = new DO.Line();
            line.CopyPropertiesTo(lineDO);
            try
            {
                dl.UpdateLine(lineDO);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.BadIdException(ex, $"bad bus id: {lineDO.Id}", lineDO.Id);
            }

        }
        public void DeleteLine(int id)
        {

            try
            {
                dl.DeleteLine(id);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.BadIdException(ex, $"Line does not exist: {id}", id);
            }

        }

        #endregion
        #region Station
        Station StationDoBoAdapter(DO.Station stationDO)
        {
            Station stationBO = new Station();

            stationBO.CopyPropertiesTo(stationBO);

            return stationBO;
        }
        public BO.Station GetStations(int id)
        {
            DO.Station stationDO;
            try
            {
                stationDO = dl.GetStation(id);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.BadIdException(ex, $"Station number does not exist: {id}", id);
            }
            return StationDoBoAdapter(stationDO);
        }
        public IEnumerable<BO.Station> GetAllStations()
        {

            return from station in dl.GetAllStations()
                   select StationDoBoAdapter(station);
        }
        public IEnumerable<BO.Station> GetAllStationsBy(Predicate<BO.Station> predicate)
        {
            IEnumerable<DO.Station> doStations = dl.GetAllStationBy((b) => predicate(StationDoBoAdapter(b)));
            return doStations.Select((b) => StationDoBoAdapter(b));
        }
        public void ValidateStationFiledsCheck(BO.Station station)
        {
            int findLetters = 0;
            int findLetters1 = 0;
            findLetters = Regex.Matches(station.Address, @"[a-zA-Z]").Count;
            findLetters1 = Regex.Matches(station.Name, @"[a-zA-Z]").Count;
            if (findLetters == 0)
                throw new BO.BadAddressException(station.Address, $"Wrong Address: {station.Address}");
            if (station.Longitude < 31.0 || station.Longitude > 33.3)
                throw new BO.BadLongitudeLatitudeException(station.Longitude, $"Wrong cordinates: {station.Longitude}");
            if (station.Latitude < 34.3 || station.Latitude > 35.5)
                throw new BO.BadLongitudeLatitudeException(station.Latitude, $"Wrong cordinates: {station.Latitude}");
            if (findLetters1 == 0)
                throw new BO.BadAddressException(station.Address, $"Wrong Address: {station.Address}");
        }
        public void AddStation(BO.Station station)
        {
            ValidateStationFiledsCheck(station);
            DO.Station stationDO = new DO.Station();
            station.CopyPropertiesTo(stationDO);
            try
            {
                dl.AddStation(stationDO);
            }
            catch (DO.BadIdException ex)
            {
                throw new BadIdException(ex, "Duplicate station Code", station.Code);
            }

        }
        public void UpdateStation(BO.Station station)//?
        {
            ValidateStationFiledsCheck(station);
            DO.Station stationDO = new DO.Station();
            station.CopyPropertiesTo(stationDO);
            try
            {
                dl.UpdateStation(stationDO);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.BadIdException(ex, $"bad station Code: {stationDO.Code}", stationDO.Code);
            }
        }
        public void DeleteStation(int id)
        {
            try
            {
                dl.GetStation(id);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.BadIdException(ex, $"station code does not exist: {id}", id);
            }

        }
        #endregion

    }
}

