using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    class Line
    {
        public enum Areas { General, North, South, Center, Jerusalem }
        int Id { get; set; }
        int Code { get; set; }
        Areas Area { get; set; }
        int FirstStation { get; set; }
        int LastStation { get; set; }
    }
}
