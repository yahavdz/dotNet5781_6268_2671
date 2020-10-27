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
        static Bus insertBus()
        {
            int readMileage, readMileageTreatment, readmileageFuel; // these variables are for the user to insert and then for the program to use.

            string readbusID;
            bool successReadingbusID = true; // to use for the "do-while" loop

            DateTime readTreatmentDate;
            Console.WriteLine("Please enter activity start date");

            readTreatmentDate = DateTime.Parse(Console.ReadLine()); // conversion from string to DateTime

            Console.WriteLine("Please enter bus ID");

            readbusID = Console.ReadLine(); // receiving the bus ID from the user

            string[] splitReadbusID = readbusID.Split('-'); // spliting the receiving string with '-' to check the format

            do
            {
                successReadingbusID = true;
                if (readTreatmentDate.Year < 2018) // format 2-3-2
                {
                    if (!(splitReadbusID[0].Length == 2 && splitReadbusID[1].Length == 3 && splitReadbusID[2].Length == 2)) // the format should be 2-3-2
                    {
                        Console.WriteLine("Format is incorrect, please try again\n");
                        successReadingbusID = false;
                        readbusID = Console.ReadLine(); // receiving the bus ID from the user
                        splitReadbusID = readbusID.Split('-'); // spliting the receiving string with '-' to check the format
                    }
                }
                if (readTreatmentDate.Year >= 2018) // format 3-2-3
                {
                    if (!(splitReadbusID[0].Length == 3 && splitReadbusID[1].Length == 2 && splitReadbusID[2].Length == 3)) // the format should be 3-2-3
                    {
                        Console.WriteLine("Format is incorrect, please try again\n");
                        successReadingbusID = false;
                        readbusID = Console.ReadLine(); // receiving the bus ID from the user
                        splitReadbusID = readbusID.Split('-'); // spliting the receiving string with '-' to check the format
                    }
                }

            } while (successReadingbusID == false); // if successReadingbusID==true it continues on

            Console.WriteLine("Please enter current mileage");
            Int32.TryParse(Console.ReadLine(), out readMileage);

            Console.WriteLine("Please enter the mileage from the last treatment");
            Int32.TryParse(Console.ReadLine(), out readMileageTreatment);

            Console.WriteLine("Please enter the mileage since the last refueling");
            Int32.TryParse(Console.ReadLine(), out readmileageFuel);

            Bus bus = new Bus // copying the reading variables to the original variables in the class bus and putting them in a new bus item
            {
                busID = readbusID,
                treatmentDate = readTreatmentDate,
                mileage = readMileage,
                mileageTreatment = readMileageTreatment,
                mileageFuel = readmileageFuel
            };
            Console.WriteLine();
            return bus;
            
        }
        static void chooseNewBus(List<Bus> busList)
        {
            Console.WriteLine("please enter the bus id: ");
            string busid = Console.ReadLine();
            Random r = new Random(DateTime.Now.Millisecond);
            int randomKm = r.Next(Bus.maxMileageTreatment);
            Console.WriteLine("the number of the miles is: " + randomKm);
            Bus currentBus1 = busList.Find(b => b.busID.Equals(busid));
            if (currentBus1 == null)
            {
                Console.WriteLine("Error: you entered invalid id, bus not found\n");
                return;
            }

            if ((currentBus1.mileage-currentBus1.mileageTreatment + randomKm) >= Bus.maxMileageTreatment)
            {
                Console.WriteLine("Error: the bus cant take the ride, too many km\n");
                return;
            }
            if ((currentBus1.mileageFuel - randomKm) < 0)
            {

                Console.WriteLine("Error: the bus cant take the ride, the fule is too low\n");
                return;
            }
            
            
                Console.WriteLine("Sucsess! the bus can take the drive\n");
                currentBus1.mileage += randomKm;
                currentBus1.mileageFuel -= randomKm;
            
            return;
        }
        static void treatmentOrRefuelingForBus(List<Bus> busList)
        {
            Console.WriteLine("please enter bus id: ");
            string id = Console.ReadLine();
            Bus currentBus = busList.Find(b => b.busID.Equals(id));
            if (currentBus == null)
            {
                Console.WriteLine("bus not found\n");
                return;
            }
            Console.WriteLine("Please enter your choise\n 1: for treatment\n 2: for refuling");
            string input = Console.ReadLine();
            if (input == "1")
            {
                currentBus.treatment();
            }
            else
            {
                currentBus.refueling();
            }
        }
        static void printList(List<Bus> busList)
        {
            foreach (Bus bus in busList)
            {
                Console.WriteLine(bus.busID + " \n the current km is: " + bus.mileage + "\n the km from the last treatment until now is: " + (bus.mileage - bus.mileageTreatment)+ "\n the last refueling: "+bus.mileageFuel);
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {

            List<Bus> busList = new List<Bus>();
            Console.WriteLine("Please enter your choise:\n 1: for insert\n 2: for choosing a bus\n 3: for treatment or refueling\n 4: for printing all the bus's mileage from the last treatment\n 5: exit\n");
            userChoiceEnum choice; // the definition is under the main
            Enum.TryParse(Console.ReadLine(), true, out choice); // convert the input to the enum and enter it into choice

            while (choice != userChoiceEnum.exit)
            {
                switch (choice)
                {
                    case userChoiceEnum.insertBus: // case 1
                        Bus newBus = insertBus();
                        busList.Add(newBus);
                        break;

                    case userChoiceEnum.chooseBus:
                        chooseNewBus(busList);
                        break;

                    case userChoiceEnum.treatmentOrRefueling:
                        treatmentOrRefuelingForBus(busList);
                        break;

                    case userChoiceEnum.printKm:
                        printList(busList);
                        break;

                    default:
                        break;
                     
                }
                Console.WriteLine("Please enter your choise:\n 1: for insert\n 2: for choosing a bus\n 3: for treatment or refueling\n 4: for printing all the bus's mileage from the last treatment\n 5: exit\n");
                Enum.TryParse(Console.ReadLine(), true, out choice); // convert the input to the enum and enter it into choice
            }
                
        }
    }
    public enum userChoiceEnum
    {
        insertBus = 1,
        chooseBus = 2,
        treatmentOrRefueling = 3,
        printKm = 4,
        exit = 5

    }

    
}


