using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_9047_4960
{
    class Program
    {
        public class busStation
        {
            private static int staticBusStationKey=1;
            private int busStationKey;
            public int BusStationKey
            {
                get
                {
                    return busStationKey;
                }
                private set
                {
                    busStationKey = value;
                }
            }
            private double latitude;
            public double Latitude
            {
                get
                {
                    return latitude;
                }
                private set
                {
                    bool flag = true;
                    while (flag)
                    {
                        if ((31<=value)&&(value<=33.3))
                        {
                            latitude = value;
                                flag = false;
                        }
                        else
                        {
                            Console.WriteLine("latitude does not fit the criteria");
                            value = (double)Console.Read();
                        }
                    }
                    
                    
                }
            }
            private double longitude;
            public double Longitude
            {
                get
                {
                    return longitude;
                }
                private set
                {
                    bool flag = true;
                    while (flag)
                    {
                        if ((34.3 <= value) && (value <= 35.5))
                        {
                            longitude = value;
                            flag = false;
                        }
                        else
                        {
                            Console.WriteLine("longitude does not fit the criteria");
                            value = (double)Console.Read();
                        }
                    }


                }
            }
            private string address;

            public busStation()
            {
                BusStationKey = staticBusStationKey;
                staticBusStationKey++;
                Console.WriteLine("Please give us the latitude of the station");
                Latitude = (double)Console.Read();
                Console.WriteLine("Please give us the longitude of the station");
                Longitude = (double)Console.Read();
                Console.WriteLine("Please give us the address of the station or enter");
                address = Console.ReadLine();
                Console.WriteLine("the bus station that was created is");
                Console.WriteLine(this);
            }
            public override string ToString() //override the toString func
            {
                return "Bus Station Code: " + BusStationKey + ", " + Latitude+ "°N "+Longitude+ "°E\n";
            }

        }

        public class BusStationList: IEnumerable, IEnumerator
        {
            public List<busStation> busStations;
            IEnumerator<busStation> IEnumeratorBusStation;
            /*public IEnumerator GetEnumerator()
            {
                IEnumerator<busStation> IEnumeratorBusStation = busStations.GetEnumerator();
                while(IEnumeratorBusStation.MoveNext())
                {
                    yield return IEnumeratorBusStation;
                }
                
            }*/
            public BusStationList()
            {
                busStations = new List<busStation>();
                IEnumeratorBusStation = busStations.GetEnumerator();
            }
            public IEnumerator GetEnumerator()
            { return IEnumeratorBusStation; }
            public object Current
            { get { return IEnumeratorBusStation; } }

            public bool MoveNext()
            {
                return IEnumeratorBusStation.MoveNext();
            }

            public void Reset()
            {
                IEnumeratorBusStation= busStations.GetEnumerator();
            }

            public bool Equals(busStation bs)
            {
                IEnumerator<busStation> IEnumeratorBusStation = busStations.GetEnumerator();
                while (IEnumeratorBusStation.MoveNext())
                {
                    if(IEnumeratorBusStation.Current.BusStationKey==bs.BusStationKey)
                    {
                        return true;
                    }
                }
                return false;
            }

        }
        class Bus
        {
            private string licenseNumber;
            public string LicenseNumber //property
            {
                get
                {
                    return licenseNumber;
                }
                private set
                {
                    bool flag = true;
                    while (flag)                                //while the license Number is not corect
                    {
                        if (startOfActivity.Year < 2018)          //checks if the year is before 2018
                        {
                            if (value.Length == 7)              //checks if the Length is right
                            {
                                flag = false;                   //the while is false now 
                                licenseNumber = value[0] + "" + value[1] + "-" + value[2] + value[3] + value[4] + "-" + value[5] + value[6]; //enter the right order o nums and -
                            }
                            else//if the length is not right
                            {
                                Console.WriteLine("license number is not compatible with the year of departure");
                                Console.Write("Please Enter The License Number No Spaces Or - :");
                                value = Console.ReadLine();
                            }
                        }
                        else
                        {
                            if (value.Length == 8)//checks if the Length is right
                            {
                                flag = false; //the while is false now 
                                licenseNumber = value[0] + "" + value[1] + "" + value[2] + "-" + value[3] + value[4] + "-" + value[5] + value[6] + value[7]; //enter the right order o nums and -
                            }
                            else//if the length is not right
                            {
                                Console.WriteLine("license number is not compatible with the year of departure");
                                Console.Write("Please Enter The License Number No Spaces Or - :");
                                value = Console.ReadLine();
                            }
                        }

                    }
                }
            }
            private DateTime startOfActivity;
            public DateTime StartOfActivity//property
            {
                get
                {
                    return startOfActivity;
                }
                private set
                {
                    startOfActivity = value;
                }
            }
            private DateTime lastCheckup = new DateTime();
            public DateTime LastCheckup//property
            {
                get
                {
                    return lastCheckup;
                }
                set
                {
                    lastCheckup = value;
                }
            }
            private int allKilometrage = 0;
            public int AllKilometrage //property
            {
                get
                {
                    return allKilometrage;
                }
                set
                {
                    if (value > allKilometrage) // can not subtract
                        allKilometrage = value;
                    else
                        Console.WriteLine("kilometrage sould be positive and we can not subtract kilometrage");
                }
            }

            private int killFromLastCheckup = 0;
            public int KillFromLastCheckup//property
            {
                get
                {
                    return killFromLastCheckup;
                }
                set
                {
                    if (value >= 0)//shoud be positive
                    {
                        killFromLastCheckup = value;
                    }
                    else
                    {
                        Console.WriteLine("kilometrage sould be positive");
                    }
                }
            }
            private int killFromRefueling = 0;
            public int KillFromRefueling//property
            {
                get
                {
                    return killFromRefueling;
                }
                set
                {
                    if (value >= 0)//shoud be positive
                    {
                        killFromRefueling = value;
                    }
                    else
                    {
                        Console.WriteLine("kilometrage sould be positive");
                    }

                }
            }

            public Bus()
            {
                bool flag = true;
                while (flag)//true until we get a corect date 
                {
                    try
                    {
                        Console.WriteLine("Please Enter The Starting Date Of Activity");
                        Console.Write("year:");
                        int yearSA;
                        while (!Int32.TryParse(Console.ReadLine(), out yearSA))
                        {
                            Console.WriteLine("only enter numbers\n" + "year:");
                        }
                        Console.Write("mounth:");
                        int mounthSA;
                        while (!Int32.TryParse(Console.ReadLine(), out mounthSA))
                        {
                            Console.WriteLine("only enter numbers\n" + "mounth:");
                        }
                        Console.Write("day:");
                        int daySA;
                        while (!Int32.TryParse(Console.ReadLine(), out daySA))
                        {
                            Console.WriteLine("only enter numbers\n" + "day:");
                        }

                        startOfActivity = new DateTime(yearSA, mounthSA, daySA); // trys to cunstract a datetime, if it doesnt work sends an exsseption
                        flag = false;
                    }
                    catch (ArgumentOutOfRangeException)//catches exsseption
                    {
                        Console.WriteLine("ERROR:start activity date invalid, please try again");
                    }
                }
                flag = true;
                Console.Write("Please Enter The License Number No Spaces Or - :");
                LicenseNumber = Console.ReadLine();                                 //lisens number
                Console.WriteLine("If You Would Like To Tell Us More About The Vehicle Please Enter 1. else press any other number to continue");
                int decison;
                while (!Int32.TryParse(Console.ReadLine(), out decison))
                {
                    Console.WriteLine("only enter numbers");
                }
                if (decison == 1) //if the user wants to tell us more
                {
                    int op;

                    do
                    {
                        Console.WriteLine("enter 0 to add last checkup date\n"
                        + "enter 1 to add the amount of all the killometers the car has\n"
                        + "enter 2 to add the amount of all the killometers from last checkup\n"
                        + "enter 3 to add the amount of all the killometers the car has from last refuel\n"
                        + "press 4 to end");
                        while (!Int32.TryParse(Console.ReadLine(), out op))
                        {
                            Console.WriteLine("only enter numbers");
                        }
                        switch (op)
                        {
                            case 0: //adds last checkup date
                                while (flag) //true until we get a corect date 
                                {
                                    try
                                    {
                                        Console.WriteLine("Please Enter The Last Checkup Date");
                                        Console.Write("year:");
                                        int yearC;
                                        while (!Int32.TryParse(Console.ReadLine(), out yearC))
                                        {
                                            Console.WriteLine("only enter numbers\n" + "year:");
                                        }
                                        Console.Write("mounth:");
                                        int mounthC;
                                        while (!Int32.TryParse(Console.ReadLine(), out mounthC))
                                        {
                                            Console.WriteLine("only enter numbers\n" + "mounth:");
                                        }
                                        Console.Write("day:");
                                        int dayC;
                                        while (!Int32.TryParse(Console.ReadLine(), out dayC))
                                        {
                                            Console.WriteLine("only enter numbers\n" + "day:");
                                        }
                                        LastCheckup = new DateTime(yearC, mounthC, dayC);
                                        flag = false;
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        Console.WriteLine("Error: last checkup date invalid, please try again");
                                    }
                                }

                                break;
                            case 1: // adds the amount of all the killometers the car has
                                Console.WriteLine("Please Enter The total kl amount");
                                int allk;
                                while (!Int32.TryParse(Console.ReadLine(), out allk))
                                {
                                    Console.WriteLine("only enter numbers\n" + "total kl:");
                                }
                                AllKilometrage = allk;
                                break;
                            case 2: //adds the amount of all the killometers from last checkup
                                Console.WriteLine("Please Enter The  kl amount from last checkup");
                                int kilometersC;
                                while (!Int32.TryParse(Console.ReadLine(), out kilometersC))
                                {
                                    Console.WriteLine("only enter numbers\n" + "kl amount from last checkup:");
                                }
                                KillFromLastCheckup = kilometersC;
                                break;
                            case 3: //adds the amount of all the killometers the car has from last refuel
                                Console.WriteLine("Please Enter The  kl amount from last refuel");
                                int kilometersR;
                                while (!Int32.TryParse(Console.ReadLine(), out kilometersR))
                                {
                                    Console.WriteLine("only enter numbers\n" + "kl amount from last refuel:");
                                }
                                KillFromRefueling = kilometersR;
                                break;
                            case 4:
                                Console.WriteLine("thanks for the aditional information");
                                break;
                            default:
                                Console.WriteLine("please try again:)");
                                break;
                        }

                    } while (op != 4); //exit
                }
            }
            public void AddKilometrage(int addKill) //adds Kilometrage to all the necessary variables
            {
                if (addKill > 0) //needs to be positive
                {
                    allKilometrage += addKill;
                    killFromLastCheckup += addKill;
                    KillFromRefueling += addKill;
                }
                else
                {
                    Console.WriteLine("can not reduce the amount of kilometrage");
                }
            }
            public override string ToString() //override the toString func
            {
                return "License Number:" + licenseNumber + " total Kilometrage from last checkup:" + killFromLastCheckup;
            }
        }

        enum Areas {General, North, South, Center, Jerusalem };
        class BusLine :Bus
        {
            private int  busLineNumber;
            private static int staticBusLineNumber = 1;
            private BusStationList stations;
            private Areas area;
            public object firstbusStation
            {
                get
                {
                    return stations.Current;
                }
            }
            public object lastbusStation
            {
                get
                {
                    return stations.busStations.Last();
                }
            }
            public int BusLineNumber
            {
                get
                {
                    return busLineNumber;
                }
                private set
                {
                    busLineNumber =value;
                }
            }
            class busLineStation
            {
                private int busLineStationKey;
                
            }
            public BusLine(): base()
            {
                stations = new BusStationList();
                busLineNumber = staticBusLineNumber;
                staticBusLineNumber++;
                Console.WriteLine("In what area is the bus located?\n"+ "0-General, 1-North, 2-South, 3-Center, 4-Jerusalem, enter the coresponding number");
                int optionA;
                while (!Int32.TryParse(Console.ReadLine(), out optionA))        //trying to get the users chosen option
                {
                    Console.WriteLine("only enter numbers");
                }
                area = (Areas)optionA;
            }
            public bool isBusStation(busStation bs)
            {
                return stations.Equals(bs);
            }
            public override string ToString()
            {
                return "BusLine: " + busLineNumber + " Area:" + area+ "\n"+" stions:\n"+stations.ToString();
            }
        }
        static void Main(string[] args)
        {
        }
    }
}
