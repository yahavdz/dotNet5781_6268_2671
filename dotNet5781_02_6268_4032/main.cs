using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6268_4032
{
    class Program
    {
        static void Main(string[] args)
        {
            //assumption: There is no need to grill the latitude and longitude lines
            //assumption: All stations must be entered manually so that the user has ready information and does not have to configure in the console
            BusLineStation a = new BusLineStation(111222, "even shmuel 131");
            BusLineStation b = new BusLineStation(111333, "even shmuel 132");
            BusLineStation c = new BusLineStation(111444, "even shmuel 133");
            BusLineStation d = new BusLineStation(111555, "even shmuel 134");
            BusLineStation e = new BusLineStation(111666, "even shmuel 135");
            BusLineStation f = new BusLineStation(111777, "even shmuel 136");
            BusLineStation g = new BusLineStation(111888, "even shmuel 137");
            BusLineStation h = new BusLineStation(111999, "even shmuel 138");
            BusLineStation i = new BusLineStation(111000, "even shmuel 139");
            BusLineStation j = new BusLineStation(111111, "even shmuel 140");
            List<BusLineStation> stations1 = new List<BusLineStation>();
            List<BusLineStation> stations2 = new List<BusLineStation>();
            List<BusLineStation> stations3 = new List<BusLineStation>();
            stations1.Add(a);
            stations1.Add(b);
            stations1.Add(c);
            stations2.Add(d);
            stations2.Add(e);
            stations2.Add(f);
            stations3.Add(g);
            stations3.Add(h);
            stations3.Add(i);
            stations3.Add(j);
            Random random = new Random();
            for (int ii=0; ii < stations1.Count(); ++ii)
            {
                stations1[ii].Latitude  = random.NextDouble() * (33.3 - 31.0) + 31.0;
                stations1[ii].Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3;
                stations1[ii].TimeFromPreviousStation = new Random().Next(86400);
                stations1[ii].DistanceFromPreviousStation = Math.Sqrt(Math.Pow(stations1[ii].Latitude - stations1[ii - 1].Latitude, 2) + Math.Pow(stations1[ii].Longitude - stations1[ii - 1].Longitude, 2) * 1.0);
            }
            for (int ii = 0; ii < stations2.Count(); ++ii)
            {
                stations2[ii].Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0;
                stations2[ii].Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3;
                stations2[ii].TimeFromPreviousStation = new Random().Next(86400);
                stations2[ii].DistanceFromPreviousStation= Math.Sqrt(Math.Pow(stations2[ii].Latitude - stations2[ii-1].Latitude, 2) + Math.Pow(stations2[ii].Longitude - stations2[ii-1].Longitude, 2) * 1.0);
            }
            for (int ii = 0; ii < stations3.Count(); ++ii)
            {
                stations3[ii].Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0;
                stations3[ii].Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3;
                stations3[ii].TimeFromPreviousStation = new Random().Next(86400);
                stations3[ii].DistanceFromPreviousStation = Math.Sqrt(Math.Pow(stations3[ii].Latitude - stations3[ii - 1].Latitude, 2) + Math.Pow(stations3[ii].Longitude - stations3[ii - 1].Longitude, 2) * 1.0);
            }

            BusLine bus1 = new BusLine(stations1, 12, a, c, Area.Center);
            BusLine bus2 = new BusLine(stations2, 17, d, f, Area.General);
            BusLine bus3 = new BusLine(stations3, 1, g, j, Area.Jerusalem);
            BusCollection busCo = new BusCollection();
            busCo.addBus(bus1);
            busCo.addBus(bus2);
            busCo.addBus(bus3);


            Console.WriteLine("Please enter your choise:\n 1: To add\n 2: To delete\n 3: To search\n 4: To print\n 5: exit\n");
            userChoiceEnum choice; // the definition is under the main
            Enum.TryParse(Console.ReadLine(), true, out choice);
            while (choice != userChoiceEnum.exit)
            {
                switch (choice)
                {
                    case userChoiceEnum.add: // case 1
                        Console.WriteLine("Please enter your choise\n 1: To add a new Bus Line\n 2:To add a new Bus Station");
                        string input = Console.ReadLine();
                        if (input == "1")
                        {
                            try
                            {
                                //TODO add all the station?
                                BusLine newBus = new BusLine();
                                busCo.addBus(newBus);
                            }
                            catch
                            {

                            }

                        }
                        else if (input == "2")
                        {
                            try
                            {
                                Console.WriteLine("To which line you want to enter the station? : \n");
                                int line = Int32.Parse(Console.ReadLine());
                                busCo[line]

                                Console.WriteLine("please enter Bus key Station: \n");
                                int keyStation = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("please enter the address of the Station: \n");
                                string address = Console.ReadLine();
                                BusStation newBusStation = new BusStation(keyStation, address);


                            }
                            catch
                            {

                            }
                        }
                        else
                            Console.WriteLine("you can press only 1 or 2 please try again\n ");
                        break;

                    case userChoiceEnum.delete:
                        
                        break;

                    case userChoiceEnum.search:
                        ;
                        break;

                    case userChoiceEnum.print:
                        
                        break;

                    default:
                        break;

                }
                Console.WriteLine("Please enter your choise:\n 1: To add\n 2: To delete\n 3: To search\n 4: To print\n 5: exit\n");
                Enum.TryParse(Console.ReadLine(), true, out choice); // convert the input to the enum and enter it into choice
            }
        }
    }
}
public enum userChoiceEnum
{
    add = 1,
    delete = 2,
    search = 3,
    print = 4,
    exit = 5

}
