using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6268_4032
{
    class Program
    {
        static void Main(string[] args)
        {
            int readBussID, readKm, readKmHandler, readKmFule, readDate;
            List<Buss> bussList = new List<Buss>();
            userChoiceEnum choice;
            Console.WriteLine( "Please enter your choise/n 1 for insert, 2 for choosing a buss");
            choice = (userChoiceEnum)Console.Read();
            switch (choice)
            {
                case userChoiceEnum.insertBuss:
                    bussList.Add(new Buss
                    {
                        bussID = readBussID,
                        date = readDate
                    }) ;
                    break;
                case userChoiceEnum.chooseBuss:
                    break;
                default:
                    break;
            }
        }
    }
    public enum userChoiceEnum
    {
        insertBuss=1, 
        chooseBuss=2,
    }
  
}
