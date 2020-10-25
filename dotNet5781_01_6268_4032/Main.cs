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
            List<Bus> busList = new List<Bus>();
            int readBusID, readKm, readKmHandler, readKmFule, readDate;
            userChoiceEnum choice;
            Console.WriteLine("Please enter your choise\n 1 for insert, 2 for choosing a buss");
            choice = (userChoiceEnum)Console.Read();
            switch (choice)
            {
                case userChoiceEnum.insertBuss:
                    busList.Add(new Bus
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
