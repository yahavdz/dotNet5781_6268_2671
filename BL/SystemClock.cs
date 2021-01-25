﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BL
{
    class SystemClock
    {
        private static Stopwatch stopWatch;
        internal static volatile bool isTimerRun = false;
        BackgroundWorker timerworker;

        #region singelton
        static readonly SystemClock instance = new SystemClock();
        static SystemClock() { }// static ctor to ensure instance init is done just before first usage
        private SystemClock()
        {
            stopWatch = new Stopwatch();
            timerworker = new BackgroundWorker();
            timerworker.DoWork += Worker_DoWork;
            timerworker.ProgressChanged += Worker_ProgressChanged;
            timerworker.WorkerReportsProgress = true;
        }
        public static SystemClock Instance { get => instance; }// The public Instance property to use
        #endregion

        static TimeSpan startTime;
        internal static int rate;        
        private Action<TimeSpan> updateTime { get; set; }
        internal static TimeSpan Now { get { return new TimeSpan(stopWatch.ElapsedTicks * rate) + startTime; } }

        public void AddObserver(Action<TimeSpan> _updateTime) => updateTime = _updateTime;
        public void RemoveObserver() => updateTime = null;

        public void Start(TimeSpan _startTime, int _rate)
        {
            if (!isTimerRun)
            {
                startTime = _startTime;
                rate = _rate;
                stopWatch.Restart();
                isTimerRun = true;
                timerworker.RunWorkerAsync();
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TimeSpan newTS = new TimeSpan(stopWatch.ElapsedTicks * rate) + startTime;
            if(updateTime != null)
                updateTime(new TimeSpan(newTS.Hours, newTS.Minutes, newTS.Seconds));
        }

        public void Stop()
        {
            if (isTimerRun)
            {
                stopWatch.Stop();
                isTimerRun = false;
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timerworker.ReportProgress(1);
                Thread.Sleep(1000/rate);
            }
        }
    }
}