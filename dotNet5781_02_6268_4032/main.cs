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
            //assumption: All stations must be entered manually so that the user has ready information and does not have to configure in the console
            int k = 0;
            Random random = new Random();
            BusCollection busCo = new BusCollection();
            for (int i = 0; i < 10; i++)
            {
                List<BusLineStation> stations = new List<BusLineStation>();
                for (int j = 0; j < 5; j++)
                {
                    BusLineStation bls = new BusLineStation(k, "even shmuel " + k.ToString());
                    bls.Latitude = random.NextDouble() * (33.3 - 31.0) + 31.0;
                    bls.Longitude = random.NextDouble() * (35.5 - 34.3) + 34.3;
                    bls.TimeFromPreviousStation = new Random().Next(86400);
                    if (j == 0)
                        bls.DistanceFromPreviousStation = 0;
                    else
                        bls.DistanceFromPreviousStation = Math.Sqrt(Math.Pow(bls.Latitude - stations[j - 1].Latitude, 2) + Math.Pow(bls.Longitude - stations[j - 1].Longitude, 2) * 1.0);

                    stations.Add(bls);
                    k++;                    
                }
                BusLine line = new BusLine(stations, i, stations[0], stations[4], Area.Center);
                busCo.addBus(line);

            }

            // another loop to put the same stations in another bus line: (add second line stations to the first line, and the forth line stations to the third line)
            for (int i = 0; i < 5; i++)
            {
                busCo[0].addStation(busCo[1].Stations[i], i);
                busCo[2].addStation(busCo[3].Stations[i], i);
            }

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
                                Console.WriteLine("please isert the Number of the Line: ");
                                int line = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("please isert the Area of the Line: General,North,South,Center,Jerusalem ");
                                Enum.TryParse(Console.ReadLine(), out Area myStatus);
                                BusLine newBus = new BusLine(line, myStatus);
                                busCo.addBus(newBus);
                            }
                            catch (Exception ex)
                            // assumption: the catch can get general Exception, bc all the catches will do the same
                            {
                                Console.WriteLine("Error: ${0}. please try again!", ex.Message);
                            }

                        }
                        else if (input == "2")
                        {
                            try
                            {
                                Console.WriteLine("To which line you want to enter the station? : ");
                                int line = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("please enter the number of the place you want to enter the station :");
                                int index = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("please enter Bus key Station: ");
                                int keyStation = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("please enter the address of the Station: ");
                                string address = Console.ReadLine();
                                BusLineStation newBusStation = new BusLineStation(keyStation, address);
                                busCo[line].addStation(newBusStation, index);
                            }
                            catch (Exception ex)
                            // assumption: the catch can get general Exception, bc all the catches will do the same
                            {
                                Console.WriteLine("Error: ${0}. please try again!", ex.Message);
                            }
                        }
                        else
                            Console.WriteLine("you can press only 1 or 2 please try again\n ");
                        break;

                    case userChoiceEnum.delete:
                        Console.WriteLine("Please enter your choise\n 1: To delete a new Bus Line\n 2:To add a new Bus Station");
                        string input1 = Console.ReadLine();
                        if (input1 == "1")
                        {
                            try
                            {
                                Console.WriteLine("please enter the bus number you want to delete: ");
                                int busDel = Int32.Parse(Console.ReadLine());
                                busCo.deleteBus(busDel);
                            }
                            catch (Exception ex)
                            // assumption: the catch can get general Exception, bc all the catches will do the same
                            {
                                Console.WriteLine("Error: ${0}. please try again!", ex.Message);
                            }
                        }
                        else if (input1 == "2")
                        {
                            try
                            {
                                Console.WriteLine("Please enter the bus number where the station is located for deletion: ");
                                int busDel = Int32.Parse(Console.ReadLine());
                                Console.WriteLine("please enter the station key you want to delete: ");
                                int stationdel = Int32.Parse(Console.ReadLine());
                                busCo[busDel].removeStation(stationdel);

                            }
                            catch (Exception ex)
                            // assumption: the catch can get general Exception, bc all the catches will do the same
                            {
                                Console.WriteLine("Error: ${0}. please try again!", ex.Message);
                            }
                        }

                        else
                            Console.WriteLine("you can press only 1 or 2 please try again\n ");
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
