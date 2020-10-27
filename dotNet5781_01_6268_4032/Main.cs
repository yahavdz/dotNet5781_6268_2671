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
                        busID = readBusID,
                        date = readDate
                    });
                    break;

                case userChoiceEnum.chooseBus:
                    Console.WriteLine("please ente the bus id: ");
                    string busid = Console.ReadLine();
                    Random r = new Random(DateTime.Now.Millisecond);
                    int randomKm = r.Next(Bus.maxKmHandler);
                    Bus currentBus1 = busList.Find(b => b.busID.Equals(busid));
                    if (currentBus1 == null)
                    {
                        Console.WriteLine("Error: you entered invalid id, bus not found");
                        return;
                    }

                    if (currentBus1.km + randomKm >= Bus.maxKmHandler)
                    {
                        Console.WriteLine("Error: the bus cant take the ride, too many km");
                        return;
                    }
                    else if ((currentBus1.kmFule - randomKm) < 0)
                    {
                        Console.WriteLine("Error: the bus cant take the ride, the fule is too low");
                        return;
                    }
                    else
                    {
                        currentBus1.km += randomKm;
                        currentBus1.kmFule -= randomKm;
                    }
                    break;

                case userChoiceEnum.treatmentOrRefueling:
                    Console.WriteLine("please enter bus id: ");
                    string id = Console.ReadLine();
                    Bus currentBus = busList.Find(b => b.busID.Equals(id));
                    if (currentBus == null)
                    {
                        Console.WriteLine("bus not found");
                        return;
                    }
                    Console.WriteLine("Please enter your choise\n 1 for treatment, 2 for refuling");
                    string input = Console.ReadLine();
                    if (input == "1")
                    {
                        currentBus.treatment();
                    }
                    else
                    {
                        currentBus.refueling();
                    }
                    break;

                case userChoiceEnum.printKm:
                    foreach (Bus bus in busList)
                    {
                        Console.WriteLine(bus.busID + "the current km is: " + bus.km + ". the km from the last treatment until now is: " + (bus.km - bus.treatmentKm));
                    }
                    break;

                default:
                    break;
            }
        }
    }
    public enum userChoiceEnum
    {
        insertBus = 1,
        chooseBus = 2,
        treatmentOrRefueling = 3,
        printKm = 4

    }

}
