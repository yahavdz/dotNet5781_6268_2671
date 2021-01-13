using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using BLApi;
using DalApi;
using BO;


namespace BL
{
    class BLImp : IBL
    {
        #region singelton
        static readonly BLImp instance = new BLImp();
        static BLImp() { }// static ctor to ensure instance init is done just before first usage
        private BLImp() { }
        public static BLImp Instance { get => instance; }// The public Instance property to use
        #endregion

        IDal dl = DalFactory.GetDal();

        #region Bus
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
            busDO.Active = true;
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
            DateTime tempDate2018 = new DateTime(2018, 1, 1);
            DateTime tempDate1985 = new DateTime(1985, 1, 1);
            if (bus.LicenseNum > 100000000 || bus.LicenseNum < 1000000)
                throw new BO.BadIdException(bus.LicenseNum, $"bad bus License number: {bus.LicenseNum}");
            if (bus.LicenseNum < 10000000 && bus.LicenseNum > 1000000)
                if (bus.FromDate > tempDate2018 || bus.FromDate < tempDate1985)
                    throw new BO.BadTimeException(bus.FromDate, $"The time does not match the license plate: {bus.FromDate}");
            if (bus.LicenseNum < 100000000 && bus.LicenseNum > 10000000)
                if (bus.FromDate < tempDate2018 || bus.FromDate > DateTime.Now)
                    throw new BO.BadTimeException(bus.FromDate, $"The time does not match the license plate: {bus.FromDate}");
            if (bus.TotalTrip < 0)
                throw new BO.BadTotalTripException(bus.TotalTrip, $"The Total Trip is lower then 0: {bus.TotalTrip}");
            if (bus.FuelRemain < 0 || bus.FuelRemain > 1200)
                throw new BO.BadFuelException(bus.FuelRemain, $"Wrong input for Fuel: {bus.FuelRemain}");
            if (bus.KilometersSinceLastTreatment > bus.TotalTrip || bus.KilometersSinceLastTreatment < (bus.TotalTrip - 20000))
                throw new BO.BadKilometersException(bus.KilometersSinceLastTreatment, $"Wrong input for Last Treatment: {bus.KilometersSinceLastTreatment}");
            if (bus.KilometersAtLastRefueling > bus.TotalTrip || bus.KilometersAtLastRefueling < (bus.TotalTrip - 1200))
                throw new BO.BadKilometersException(bus.KilometersAtLastRefueling, $"Wrong input for Last Refueling: {bus.KilometersAtLastRefueling}");
        }
        public void UpdateBus(BO.Bus bus)//?
        {
            ValidateBusFiledsCheck(bus);
            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO);
            busDO.Active = true;
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
                dl.DeleteBus(id);
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
            LineBO.stations = new List<LineStation>();

            LineDO.CopyPropertiesTo(LineBO);
            IEnumerable<DO.LineStation> listOfLineStations = dl.GetAllLineStationBy(ls => ls.LineId == LineDO.Id);
            listOfLineStations.OrderBy(ls => ls.LineStationIndex);
            for (int i = 0; i < listOfLineStations.Count(); i++)
            {
                BO.LineStation bols = new LineStation();
                DO.Station currentDoStation = dl.GetStation(listOfLineStations.ElementAt(i).Station);
                if (i < listOfLineStations.Count() - 1) // not the last
                {
                    DO.Station nextDoStation = dl.GetStation(listOfLineStations.ElementAt(i + 1).Station);
                    bols.DistanceToNextStation = Math.Sqrt(Math.Pow(nextDoStation.Latitude * 110.574 - currentDoStation.Latitude * 110.574, 2) + Math.Pow(nextDoStation.Longitude * 111.320 * Math.Cos(nextDoStation.Latitude) - currentDoStation.Longitude * 111.320 * Math.Cos(nextDoStation.Latitude), 2) * 1.0);
                    double calc = (bols.DistanceToNextStation / 50)*3;
                    int temp = Convert.ToInt32(calc);
                    if (calc < 1 && calc > 0) 
                        bols.TimeToNextStation =new TimeSpan (0,0,temp*2);
                    if (calc < 60 && calc > 1)
                        bols.TimeToNextStation = new TimeSpan(0, temp*2, 0);
                    else if (calc > 60)
                        bols.TimeToNextStation =  TimeSpan.FromHours(temp*2);
                }
                currentDoStation.CopyPropertiesTo(bols);
                LineBO.stations = LineBO.stations.Append(bols).ToList();
            }
            return LineBO;
        }
        DO.Line LineBoDoAdapter(BO.Line LineBO)
        {
            DO.Line LineDO = new DO.Line();
            LineBO.CopyPropertiesTo(LineDO);
            LineDO.FirstStation = LineBO.stations.First().Code;
            LineDO.LastStation = LineBO.stations.Last().Code;
            return LineDO;
        }

        IEnumerable<DO.LineStation> LineBoLineStationDoAdapter(BO.Line LineBO)
        {             
            List<DO.LineStation> stationsList = new List<DO.LineStation>();
            for (int i = 0; i < LineBO.stations.Count(); i++) // instaed of foraech for the station index
            {
                DO.LineStation doLineStation = new DO.LineStation();
                doLineStation.LineId = LineBO.Code;
                doLineStation.LineStationIndex = i;
                doLineStation.Station = LineBO.stations.ElementAt(i).Code;
                doLineStation.Active = LineBO.stations.ElementAt(i).Active;
                if (i > 0) doLineStation.PrevStation = LineBO.stations.ElementAt(i - 1).Code;
                if (i < LineBO.stations.Count() - 1) doLineStation.NextStation = LineBO.stations.ElementAt(i + 1).Code;
                stationsList.Add(doLineStation);
            }    
            return stationsList;
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
            if (line.Code < 0)
                throw new BadIdException(line.Code, " Line Code must be bigger then 0");
            DO.Line lineDO = new DO.Line();
            line.CopyPropertiesTo(lineDO);
            lineDO.Active = true;
            try
            {
                dl.AddLine(lineDO);
            }
            catch (DO.BadIdException ex)
            {
                throw new BadIdException(ex, "Duplicate Line ID", line.Id);
            }

        }
        public void UpdateLine(BO.Line line)
        {
            if (line.Code < 0)
                throw new BadIdException(line.Code, " Line Code must be bigger then 0");
            DO.Line lineDO = new DO.Line();

            // adapt the line - list of stations to first and last:
            lineDO = LineBoDoAdapter(line);
            try
            {
                dl.UpdateLine(lineDO);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.BadIdException(ex, $"bad line id: {lineDO.Id}", lineDO.Id);
            }

            // update the line stations - to save the stations order:
            IEnumerable<DO.LineStation> lineStationsList = LineBoLineStationDoAdapter(line);
            foreach (DO.LineStation doLineStation in lineStationsList)
            {
                if (dl.isLineStationExistForLine(doLineStation.LineId, doLineStation.Station))
                {
                    try
                    {
                        dl.UpdateLineStation(doLineStation);
                    }
                    catch (DO.BadIdException ex)
                    {
                        // TODO throw new BO.BadIdException(ex, $"bad bus id: {lineDO.Id}", lineDO.Id);
                    }
                }
                else
                {
                    try
                    {
                        dl.AddLineStation(doLineStation);
                    }
                    catch (DO.BadIdException ex)
                    {
                        // TODO throw new BO.BadIdException(ex, $"bad bus id: {lineDO.Id}", lineDO.Id);
                    }
                }
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
        public void AddStationToLine(Line line, LineStation station)
        {
            line.stations = line.stations.Append(station).ToList();
            UpdateLine(line);
        }

        #endregion

        #region Station
        Station StationDoBoAdapter(DO.Station stationDO)
        {
            Station stationBO = new Station();

            stationDO.CopyPropertiesTo(stationBO);

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
            if (station.Longitude < 34.3 || station.Longitude > 35.5)
                throw new BO.BadLongitudeLatitudeException(station.Longitude, $"Wrong cordinates: {station.Longitude}");
            if (station.Latitude < 31.0 || station.Latitude > 33.3)
                throw new BO.BadLongitudeLatitudeException(station.Latitude, $"Wrong cordinates: {station.Latitude}");
            if (findLetters1 == 0)
                throw new BO.BadAddressException(station.Address, $"Wrong Address: {station.Address}");
        }
        public void AddStation(BO.Station station)
        {
            ValidateStationFiledsCheck(station);
            DO.Station stationDO = new DO.Station();
            station.CopyPropertiesTo(stationDO);
            stationDO.Active = true;
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
                dl.DeleteStation(id);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.BadIdException(ex, $"station code does not exist: {id}", id);
            }

        }
        #endregion
    }
}
