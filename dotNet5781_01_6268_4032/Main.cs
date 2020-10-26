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
            int readMileage, readMileageTreatment, readMileageFule; // these variables are for the user to insert and then for the program to use.

            string readBussID;
            bool successReadingBussID = true; // to use for the "do-while" loop

            DateTime readTreatmentDate;

            List<Buss> bussList = new List<Buss>();



            userChoiceEnum choice; // the definition is under the main
            choice = (userChoiceEnum)Console.Read(); // receiving a choise from the user for the enum
            Console.WriteLine( "Please enter your choise/n 1 for insert, 2 for choosing a buss");
            
            switch (choice)
            {
                case userChoiceEnum.insertBuss: // case 1

                    Console.WriteLine("Please enter activity start date/n");

                    readTreatmentDate = DateTime.Parse( Console.ReadLine()); // conversion from string to DateTime
                    
                    Console.WriteLine("Please enter buss ID/n");
                    
                    readBussID = Console.ReadLine(); // receiving the buss ID from the user
                    
                    string[] splitReadBussID = readBussID.Split('-'); // spliting the receiving string with '-' to check the format
                    
                    do
                    {
                        if (readTreatmentDate.Year < 2018) // format 2-3-2
                        {
                            if (!(splitReadBussID[0].Length == 2 && splitReadBussID[1].Length == 3 && splitReadBussID[2].Length == 2)) // the format should be 2-3-2
                            {
                                Console.WriteLine("Format is incorrect, please try again/n");
                                successReadingBussID = false;
                                readBussID = Console.ReadLine(); // receiving the buss ID from the user
                                splitReadBussID = readBussID.Split('-'); // spliting the receiving string with '-' to check the format
                            }
                        }
                        if (readTreatmentDate.Year >= 2018) // format 3-2-3
                        {
                            if (!(splitReadBussID[0].Length == 3 && splitReadBussID[1].Length == 2 && splitReadBussID[2].Length == 3)) // the format should be 3-2-3
                            {
                                Console.WriteLine("Format is incorrect, please try again/n");
                                successReadingBussID = false; 
                                readBussID = Console.ReadLine(); // receiving the buss ID from the user
                                splitReadBussID = readBussID.Split('-'); // spliting the receiving string with '-' to check the format
                            }
                        }
                        
                    } while (successReadingBussID==false); // if successReadingBussID==true it continues on

                    Console.WriteLine("Please enter current mileage/n");
                    readMileage = Console.Read();

                    Console.WriteLine("Please enter last mileage treatment/n");
                    readMileageTreatment = Console.Read();
                    
                    Console.WriteLine("Please enter since last refuling mileage/n");
                    readMileageFule = Console.Read();
                    
                    bussList.Add(new Buss // copying the reading variables to the original variables in the class Buss and putting them in a new Buss item
                    {
                        bussID = readBussID,
                        treatmentDate = readTreatmentDate,
                        mileage=readMileage,
                        mileageTreatment=readMileageTreatment,
                        mileageFule=readMileageFule
                    });
                    break;
                
                
                case userChoiceEnum.chooseBuss: // case 2
                    break;
                default:
                    break;
            }
        }
    }
    public enum userChoiceEnum
    {
        insertBuss=1, // case 1
        chooseBuss=2, // case 2
    }
  
}
