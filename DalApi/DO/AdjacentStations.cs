﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
     public class AdjacentStations
    {
        public int Station1 { get; set; }
        public int Station2 { get; set; }
        public double Distance { get; set; }
        public TimeSpan Time { get; set; }
        public bool Active { get; set; }
    }
}
