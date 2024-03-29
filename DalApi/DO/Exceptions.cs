﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DO
{
    [Serializable]
    public class BadIdException : Exception
    {
        public int ID;
        public BadIdException(int id) : base() => ID = id;
        public BadIdException(int id, string message) :
            base(message) => ID = id;
        public BadIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;
        public override string ToString() => base.ToString() + $", bad id: {ID}";
    }
    public class BadTimeException : Exception
    {

        public DateTime Date; 
        public BadTimeException(DateTime date, string message) :
           base(message) => Date = date;
        public override string ToString() => base.ToString() + $", Wrong Time: {Date}";
    }

        public class BadUserNameException : Exception
    {
        public string userName;
        public BadUserNameException(string un) : base() => userName = un;
        public BadUserNameException(string un, string message) :
            base(message) => userName = un;
        public BadUserNameException(string un, string message, Exception innerException) :
            base(message, innerException) => userName = un;
        public override string ToString() => base.ToString() + $", bad user name: {userName}";
    }

    public class BadAdjacentStationsException : Exception
    {
        public int station1;
        public int station2;
        public BadAdjacentStationsException(int s1, int s2) : base() { station1 = s1; station2 = s2; }
        public BadAdjacentStationsException(int s1, int s2, string message) :
            base(message) { station1 = s1; station2 = s2; }
        public BadAdjacentStationsException(int s1, int s2, string message, Exception innerException) :
            base(message, innerException) { station1 = s1; station2 = s2; }
        public override string ToString() => base.ToString() + $", bad adjacent stations: {station1}, {station2}";
    }
    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }

}

