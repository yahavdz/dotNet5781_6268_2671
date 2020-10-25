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
            int readMileage, readKmHandler, readKmFule;
            string readBussID;
            DateTime readTreatmentDate;
            List<Buss> bussList = new List<Buss>();
            
            userChoiceEnum choice;
            Console.WriteLine( "Please enter your choise/n 1 for insert, 2 for choosing a buss");
            choice = (userChoiceEnum)Console.Read();
            switch (choice)
            {
                case userChoiceEnum.insertBuss:
                    Console.WriteLine("Please enter activity start date/n");
                    readTreatmentDate =DateTime.Parse( Console.ReadLine());
                    
                    Console.WriteLine("Please enter buss ID/n");
                    readBussID = Console.ReadLine();
                    if (readTreatmentDate.Year < 2018)
                    {
                        if (true)
                        {

                        }
                    }
                    Console.WriteLine("Please enter current mileage/n");
                    readMileage = Console.Read();
                    Console.WriteLine("Please enter last mileage treatment/n");
                    readKmHandler = Console.Read();
                    Console.WriteLine("Please enter since last refuling mileage/n");
                    readKmFule = Console.Read();
                    bussList.Add(new Buss
                    {
                        bussID = readBussID,
                        treatmentDate = readTreatmentDate,
                        
                        mileage=readMileage,
                        treatmentKm=readKmHandler,
                        kmFule=readKmFule
                    });
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
