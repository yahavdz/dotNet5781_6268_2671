﻿using System;
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
    public class BLImp : IBL
    {
        #region singelton
        static readonly BLImp instance = new BLImp();
        static BLImp() { }// static ctor to ensure instance init is done just before first usage
        private BLImp() { }
        public static BLImp Instance { get => instance; }// The public Instance property to use
        #endregion

        IDal dl = DalFactory.GetDal();
        SystemClock sc = SystemClock.Instance;
        TravelOperator to = TravelOperator.Instance;

        #region Simulator
        public void SetStationPanel(int station, Action<List<LineTiming>> updateBus)
        {
            if (SystemClock.isTimerRun)
            {
                to.Stop();
                to.AddObserver(updateBus);
                to.Start(station);
            }
        }
        public void StartSimulator(TimeSpan startTime, int Rate, Action<TimeSpan> updateTime)
        {
            sc.AddObserver(updateTime);
            sc.Start(startTime, Rate);
        }
        public void StopSimulator()
        {
            to.Stop();
            sc.Stop();
            sc.RemoveObserver();
            to.RemoveObserver();
        }
        #endregion

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
        void ValidateBusFiledsCheck(BO.Bus bus)
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
            IEnumerable<DO.LineStation> orderedListOfLineStations = listOfLineStations.OrderBy(ls => ls.LineStationIndex);
            for (int i = 0; i < orderedListOfLineStations.Count(); i++)
            {
                BO.LineStation bols = new LineStation();
                DO.Station currentDoStation = dl.GetStation(orderedListOfLineStations.ElementAt(i).Station);
                if (i < orderedListOfLineStations.Count() - 1) // not the last
                {
                    DO.AdjacentStations adjacentStations = dl.GetAdjacentStations(orderedListOfLineStations.ElementAt(i).Station, orderedListOfLineStations.ElementAt(i + 1).Station);
                    bols.TimeToNextStation = adjacentStations.Time;
                    bols.DistanceToNextStation = adjacentStations.Distance;
                }
                currentDoStation.CopyPropertiesTo(bols);
                LineBO.stations = LineBO.stations.Append(bols).ToList();
            }

            // calculate the total time according to the stations list:
            TimeSpan lt = new TimeSpan();
            foreach (LineStation ls in LineBO.stations)
            {
                lt += ls.TimeToNextStation;
            }
            LineBO.TotalTime = lt;

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
                doLineStation.LineId = LineBO.Id;
                doLineStation.LineStationIndex = i;
                doLineStation.Station = LineBO.stations.ElementAt(i).Code;
                doLineStation.Active = LineBO.stations.ElementAt(i).Active;
                if (i > 0) doLineStation.PrevStation = LineBO.stations.ElementAt(i - 1).Code;
                if (i < LineBO.stations.Count() - 1 && LineBO.stations.Count() > 1) doLineStation.NextStation = LineBO.stations.ElementAt(i + 1).Code;
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
        public int AddLine(BO.Line line)
        {
            if (line.Code < 0)
                throw new BadIdException(line.Code, " Line Code must be bigger then 0");
            DO.Line lineDO = new DO.Line();
            line.CopyPropertiesTo(lineDO);
            lineDO.Active = true;
            try
            {
                return dl.AddLine(lineDO);
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
            /*
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
                        throw new BO.IsNotExistException(ex, $" {lineDO.Id} Not Exist", lineDO.Id);
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
                        throw new BO.IsNotExistException(ex, $" {lineDO.Id} Not Exist", lineDO.Id);
                    }
                }
            }*/


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
        public void AddStationToLine(Line line, LineStation station, int index)
        {
            // add the new stations in index position:
            IEnumerable<DO.LineStation> lineStationsList = dl.GetAllLineStationBy(ls => ls.LineId == line.Id); // LineBoLineStationDoAdapter(line);
            lineStationsList = lineStationsList.OrderBy(ls => ls.LineStationIndex);

            int nextId = lineStationsList.ElementAt(index).Station;
            int prevId = lineStationsList.ElementAt(index - 1).Station;

            DO.LineStation newLineStation = new DO.LineStation();
            newLineStation.Active = true;
            newLineStation.LineId = line.Id;
            newLineStation.LineStationIndex = index;
            newLineStation.Station = station.Code;
            if (index < line.stations.Count()) newLineStation.NextStation = nextId;
            if (index > 0) newLineStation.PrevStation = prevId;
            try
            {
                dl.AddLineStation(newLineStation);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.DuplicateException(ex, $"Duplicate station: {station.Code}", station.Code);
            }

            // add adjacment stations if needed:
            if (index < line.stations.Count())
            {
                AddAdjacentStationsBetweenStations(newLineStation.Station, newLineStation.NextStation);
            }

            if (index > 0)
            {
                AddAdjacentStationsBetweenStations(newLineStation.PrevStation, newLineStation.Station);
            }

            //update line if it first or last station:
            DO.Line lineDO = new DO.Line();
            if (index == 0)
            {
                //add the station as first:
                line.CopyPropertiesTo(lineDO);
                lineDO.FirstStation = station.Code;
                dl.UpdateLine(lineDO);
            }
            else if (index == line.stations.Count())
            {
                //add the station as last:
                line.CopyPropertiesTo(lineDO);
                lineDO.LastStation = station.Code;
                dl.UpdateLine(lineDO);
            }


            // update the index of the stations after the new station:
            for (int i = index; i < lineStationsList.Count(); i++)
            {
                DO.LineStation doLineStation = dl.GetAllLineStationBy(ls => ls.LineStationIndex == i && ls.LineId == line.Id && ls.Station != station.Code).ElementAt(0);
                doLineStation.LineStationIndex++;
                dl.UpdateLineStation(doLineStation); // update the index of the station
            }


            // update the prev and next stations to point to the new station:
            if (index > 0)
            {
                DO.LineStation prevStation = dl.GetLineStation(line.Id, prevId);
                prevStation.NextStation = station.Code;
                dl.UpdateLineStation(prevStation);
            }
            if (index < lineStationsList.Count())
            {
                DO.LineStation nextStation = dl.GetLineStation(line.Id, nextId);
                nextStation.PrevStation = station.Code;
                dl.UpdateLineStation(nextStation);
            }


        }
        private void AddAdjacentStationsBetweenStations(int station1, int station2)
        {
            try
            {
                DO.AdjacentStations adjacentStations = new DO.AdjacentStations();
                adjacentStations.Station1 = station1;
                adjacentStations.Station2 = station2;
                adjacentStations.Active = true;
                DO.Station newStation = dl.GetStation(station1);
                DO.Station nextStation = dl.GetStation(station2);
                adjacentStations.Distance = Math.Sqrt(Math.Pow(nextStation.Latitude * 110.574 - newStation.Latitude * 110.574, 2) + Math.Pow(nextStation.Longitude * 111.320 * Math.Cos(nextStation.Latitude) - newStation.Longitude * 111.320 * Math.Cos(nextStation.Latitude), 2) * 1.0);

                double calc = (adjacentStations.Distance / 60) * 3;
                int temp = Convert.ToInt32(calc);
                if (calc < 1 && calc > 0)
                    adjacentStations.Time = new TimeSpan(0, 0, temp * 2);
                if (calc < 60 && calc > 1)
                    adjacentStations.Time = new TimeSpan(0, temp * 2, 0);
                else if (calc > 60)
                    adjacentStations.Time = TimeSpan.FromHours(temp * 2);

                dl.AddAdjacentStations(adjacentStations);
            }
            catch (DO.BadAdjacentStationsException)
            {
                // not throw exception - if alreday exist its ok
            }
        }
        public void DeleteStationFromLine(Line line, LineStation station)
        {
            DO.LineStation stationToDelete = dl.GetLineStation(line.Id, station.Code);
            if (stationToDelete.PrevStation == 0 && stationToDelete.NextStation == 0)
            {
                // the only station in the line
            }
            else if (stationToDelete.PrevStation == 0)
            {
                //the first station
                DO.LineStation nextStation = dl.GetLineStation(line.Id, stationToDelete.NextStation);
                nextStation.PrevStation = 0;
                dl.UpdateLineStation(nextStation);
                line.FirstStation = stationToDelete.NextStation;
                UpdateLine(line); // update the first station field
                IEnumerable<DO.LineStation> lineStationsList = dl.GetAllLineStationBy(ls => ls.LineId == line.Id); // LineBoLineStationDoAdapter(line);
                lineStationsList = lineStationsList.OrderBy(ls => ls.LineStationIndex);
                for (int i = stationToDelete.LineStationIndex + 1; i < lineStationsList.Count(); i++)
                {
                    DO.LineStation doLineStation = dl.GetAllLineStationBy(ls => ls.LineStationIndex == i && ls.LineId == line.Id && ls.Station != station.Code).ElementAt(0);
                    doLineStation.LineStationIndex--;
                    dl.UpdateLineStation(doLineStation); // update the index of the station
                }
            }
            else if (stationToDelete.NextStation == 0)
            {
                // the last station
                DO.LineStation prevStation = dl.GetLineStation(line.Id, stationToDelete.PrevStation);
                prevStation.NextStation = 0;
                dl.UpdateLineStation(prevStation);
                line.LastStation = stationToDelete.PrevStation;
                UpdateLine(line); // update the last station field
            }
            else
            {
                DO.LineStation prevStation = dl.GetLineStation(line.Id, stationToDelete.PrevStation);
                DO.LineStation nextStation = dl.GetLineStation(line.Id, stationToDelete.NextStation);
                prevStation.NextStation = nextStation.Station;
                nextStation.PrevStation = prevStation.Station;
                dl.UpdateLineStation(prevStation);
                dl.UpdateLineStation(nextStation);
                IEnumerable<DO.LineStation> lineStationsList = dl.GetAllLineStationBy(ls => ls.LineId == line.Id); // LineBoLineStationDoAdapter(line);
                lineStationsList = lineStationsList.OrderBy(ls => ls.LineStationIndex);
                for (int i = stationToDelete.LineStationIndex + 1; i < lineStationsList.Count(); i++)
                {
                    DO.LineStation doLineStation = dl.GetAllLineStationBy(ls => ls.LineStationIndex == i && ls.LineId == line.Id && ls.Station != station.Code).ElementAt(0);
                    doLineStation.LineStationIndex--;
                    dl.UpdateLineStation(doLineStation); // update the index of the station
                }

                // add adjacent stations if needed:
                AddAdjacentStationsBetweenStations(prevStation.Station, nextStation.Station);
             
            }
            dl.DeleteLineStation(line.Id, station.Code);

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
            DO.Station sta;
            try
            {
                sta = dl.GetStation(id);
            }
            catch (DO.BadIdException ex)
            {
                throw new BO.BadIdException(ex, $"station code does not exist: {id}", id);
            }

            bool emptySta = true;
            foreach (Line l in GetAllLines())
                foreach (Station s in l.stations)
                    if (sta.Code == s.Code)
                        emptySta = false;
            if (emptySta)
                dl.DeleteStation(id);
            else
                throw new BO.BadIdException(id, $"station code does not exist: {id}");
        }
        #endregion

        #region User
        User userDoBoAdapter(DO.User userDO)
        {
            User userBO = new User();
            userDO.CopyPropertiesTo(userBO);
            return userBO;
        }
        public BO.User GetUser(string name)
        {
            DO.User userDO;
            try
            {
                userDO = dl.GetUser(name);
            }
            catch (DO.BadUserNameException)
            {
                throw new BO.BadUserNameException(name);
            }
            return userDoBoAdapter(userDO);
        }
        public IEnumerable<User> GetAllUsers()
        {
            return from user in dl.GetAllUsers()
                   select userDoBoAdapter(user);
        }
        public IEnumerable<User> GetAllUsersBy(Predicate<User> predicate)
        {
            IEnumerable<DO.User> doUsers = dl.GetAllUsersBy((u) => predicate(userDoBoAdapter(u)));
            return doUsers.Select((u) => userDoBoAdapter(u));
        }
        public void AddUser(User user)
        {
            DO.User userDO = new DO.User();
            user.CopyPropertiesTo(userDO);
            userDO.Active = true;
            try
            {
                dl.AddUser(userDO);
            }
            catch (DO.BadUserNameException)
            {
                throw new BadUserNameException(user.UserName, "Duplicate user name");
            }
        }
        public void UpdateUser(User user)
        {
            DO.User userDO = new DO.User();
            user.CopyPropertiesTo(userDO);
            try
            {
                dl.UpdateUser(userDO);
            }
            catch (DO.BadUserNameException)
            {
                throw new BO.BadUserNameException(userDO.UserName);
            }
        }
        public void DeleteUser(string name)
        {
            try
            {
                dl.DeleteUser(name);
            }
            catch (DO.BadUserNameException)
            {
                throw new BO.BadUserNameException(name);
            }
        }
        #endregion
    }
}
