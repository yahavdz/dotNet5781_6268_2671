using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BO;
using DalApi;
using BLApi;

namespace BL
{
    class TravelOperator
    {
        BackgroundWorker OperatorWorker;
        List<DO.LineTrip> allLT;
        IDal dl;
        IBL bl;

        #region singelton
        static readonly TravelOperator instance = new TravelOperator();
        static TravelOperator() { }// static ctor to ensure instance init is done just before first usage
        private TravelOperator()
        {
            OperatorWorker = new BackgroundWorker();
            OperatorWorker.DoWork += Worker_DoWork;
            OperatorWorker.ProgressChanged += doTravel;
            OperatorWorker.WorkerSupportsCancellation = true;
            OperatorWorker.WorkerReportsProgress = true;

            dl = DalFactory.GetDal();
            CodeStation = -1;
        }
        public static TravelOperator Instance { get => instance; }// The public Instance property to use
        #endregion

        private Action<List<LineTiming>> updateBus { get; set; }
        private int CodeStation { get; set; }
        private bool onTravel = false;

        public void AddObserver(Action<List<LineTiming>> _updateBus) => updateBus = _updateBus;
        public void RemoveObserver() => updateBus = null;

        public void Start(int _Station)
        {
            if (!onTravel && !OperatorWorker.IsBusy)
            {
                allLT = dl.GetAllLineTrip().OrderBy(lt => lt.StartAt).ToList();
                allLT.OrderBy(lt => lt.StartAt);
                CodeStation = _Station;
                onTravel = true;
                OperatorWorker.RunWorkerAsync();
            }
        }

        public void Stop()
        {
            if (onTravel)
                onTravel = false;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (SystemClock.isTimerRun && onTravel)
            {
                OperatorWorker.ReportProgress(1);
                Thread.Sleep(60000 / SystemClock.rate);
            }
            onTravel = false;
            if (OperatorWorker.CancellationPending)
                e.Cancel = true;
        }

        private void doTravel(object sender, ProgressChangedEventArgs e)
        {
            List<LineTiming> lineTimingInSta = GetLineTimingPerStation(CodeStation);
            if(updateBus != null)
                updateBus(lineTimingInSta);
        }

        private List<LineTiming> GetLineTimingPerStation(int curStation)
        {
            bl = BLFactory.GetBL("1");
            TimeSpan HalfHour = new TimeSpan(0, 30, 0);
            TimeSpan tenMin = new TimeSpan(0, 10, 0);
            TimeSpan tempNow = SystemClock.Now;
            List<LineTiming> allLineTiming = new List<LineTiming>();
            List<LineTiming> passLineTiming = new List<LineTiming>();
            foreach (DO.LineTrip ltr in allLT)
            {
                bool find = false;
                Line currentLine = bl.GetLine(ltr.LineId);
                TimeSpan isAriveAt = ltr.StartAt;
                TimeSpan isTake = new TimeSpan(0);
                if (currentLine.stations.FirstOrDefault(s => s.Code == curStation) != null)
                {
                    foreach (LineStation ls in currentLine.stations)
                    {
                        if (ls.Code == curStation)
                            break;
                        isTake += ls.TimeToNextStation;
                    }
                    find = true;
                }
                isAriveAt = isAriveAt.Add(isTake);

                if (find)
                {
                    find = false;
                    while (isAriveAt < ltr.FinishAt.Add(isTake))
                    {
                        if (!(isAriveAt.Add(tenMin) < tempNow || isAriveAt > tempNow.Add(HalfHour)))
                        {
                            find = true;
                            while (!(isAriveAt.Add(ltr.Frequency).Add(tenMin) < tempNow || isAriveAt.Add(ltr.Frequency) > tempNow.Add(HalfHour)))
                            {
                                if(isAriveAt > tempNow)
                                    break;
                                isAriveAt = isAriveAt.Add(ltr.Frequency);
                            }
                            break;
                        }
                        isAriveAt = isAriveAt.Add(ltr.Frequency);
                    }
                }

                if (find && !(tempNow.Add(HalfHour) < isAriveAt || isAriveAt < tempNow))
                {
                    LineTiming newLT = new LineTiming();
                    newLT.LineNum = currentLine.Code;
                    newLT.StationName = currentLine.stations.Last().Name;
                    newLT.StartTime = isAriveAt;
                    newLT.ArrivalTime = isAriveAt.Subtract(tempNow);
                    allLineTiming.Add(newLT);
                }

                if (find && !(isAriveAt.Add(tenMin) < tempNow || tempNow < isAriveAt))
                {
                    LineTiming newLT = new LineTiming();
                    newLT.LineNum = currentLine.Code;
                    newLT.StationName = currentLine.stations.Last().Name;
                    newLT.StartTime = isAriveAt;
                    newLT.ArrivalTime = (new TimeSpan(0, 60, 0)).Subtract(tempNow.Subtract(isAriveAt));
                    passLineTiming.Add(newLT);
                }
            }
            passLineTiming = passLineTiming.OrderBy(lt => lt.ArrivalTime).ToList();
            passLineTiming.AddRange(allLineTiming.OrderBy(lt => lt.ArrivalTime));
            return passLineTiming;
        }
    }
}