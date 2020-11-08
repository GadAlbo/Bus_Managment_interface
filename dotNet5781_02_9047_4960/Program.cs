﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet5781_02_9047_4960
{
    class Program
    {
        public class busStation
        {
            Random r = new Random(DateTime.Now.Millisecond);
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
          
            private string address;
            private GeoCoordinate coordinates;
            public GeoCoordinate Coordinates
            {
                get
                {
                    return coordinates;
                }
            }


            public busStation()
            {
                BusStationKey = staticBusStationKey;
                staticBusStationKey++;
                Console.WriteLine("Please give us the latitude of the station");
                double latitude = r.NextDouble() * (33.3 - 31) + 31;
                Console.WriteLine("Please give us the longitude of the station");
                double longitude = r.NextDouble() * (35.5 - 34.3) + 34.3;
                coordinates.Latitude = latitude;
                coordinates.Longitude = longitude;
                Console.WriteLine("Please give us the address of the station or enter");
                address = Console.ReadLine();
                Console.WriteLine("the bus station that was created is");
                Console.WriteLine(this);
            }
            public override string ToString() //override the toString func
            {
                return "Bus Station Code: " + BusStationKey + ", " + coordinates.Latitude + "°N "+ coordinates.Longitude + "°E\n";
            }
            public double Distance(busStation bs)
            {
                return ((coordinates.GetDistanceTo(bs.coordinates)));
            }

        }

        public class BusStationList: IEnumerable, IEnumerator
        {
            public List<busStation> busStations;
            IEnumerator<busStation> IEnumeratorBusStation;
           /* public IEnumerator GetEnumerator()
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
            public void Add(busStation bs)
            {
                int count = 0;
                int index = 0;
                IEnumerator<busStation> IEnumeratorBusStation = busStations.GetEnumerator();
                double minDistance = IEnumeratorBusStation.Current.Distance(bs);
                while (IEnumeratorBusStation.MoveNext())
                {
                    if (IEnumeratorBusStation.Current.Distance(bs) < minDistance)
                    {
                        minDistance = IEnumeratorBusStation.Current.Distance(bs);
                        index += count;
                  
                    }
                }
                busStations.Insert(index + 1, bs);
            }
            public void Remove(busStation bs)
            {
                busStations.Remove(bs);
            }
            public double distance(busStation first, busStation last)
            {
                if(first.Distance(last)>-1)
                {
                    IEnumerator<busStation> IEnumeratorBusStation = busStations.GetEnumerator();
                    double Distance = 0;
                    while (IEnumeratorBusStation.MoveNext())
                    {
                        if (IEnumeratorBusStation.Current.BusStationKey == first.BusStationKey)
                        {
                            IEnumerator<busStation> help = IEnumeratorBusStation;
                            help.MoveNext();
                            while (help.Current.BusStationKey!=last.BusStationKey)
                            {
                                Distance += IEnumeratorBusStation.Current.Distance(help.Current);
                            }
                            Distance += IEnumeratorBusStation.Current.Distance(help.Current);
                            return Distance;
                        }
                    }
                }
                return -1;
            }
            public BusStationList creatNewBusStationList(busStation first, busStation last)
            {
                BusStationList bs = new BusStationList();
                IEnumerator<busStation> IEnumeratorBusStation = busStations.GetEnumerator();
                while (IEnumeratorBusStation.MoveNext())
                {
                    if (IEnumeratorBusStation.Current.BusStationKey == first.BusStationKey)
                    {
                        while (IEnumeratorBusStation.Current.BusStationKey != last.BusStationKey)
                        {
                            bs.Add(IEnumeratorBusStation.Current);
                            IEnumeratorBusStation.MoveNext();
                        }
                        return bs;
                    }
                    Console.WriteLine("one or more ot the stations are not exsist");
                }
                return null;
            }
            public double distanceWholeLine()
            {
                return distance(busStations.First<busStation>(), busStations.Last<busStation>());
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
        class BusLine :Bus, IComparable
        {
            public const double TimeForMeter = 0.01;
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
                private double distanceFromLastStation;
                private double timeFromLastStation;

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
            public BusLine(BusStationList bs, Areas a) : base()
            {
                stations = bs;
                area = a;
            }
            public bool isBusStation(busStation bs)
            {
                return stations.Equals(bs);
            }
            public override string ToString()
            {
                return "BusLine: " + busLineNumber + " Area:" + area+ "\n"+" stions:\n"+stations.ToString();
            }
            public void addStition(busStation bs)
            {
                stations.Add(bs);
            }
            public void deleteStition(busStation bs)
            {
                stations.Remove(bs);
            }
            public double distance(busStation first, busStation last)
            {
                if( isBusStation(first))
                {
                    if(isBusStation(last))
                    {
                        return first.Distance(last);
                    }
                    Console.WriteLine(last.BusStationKey + " is not exsist");
                }
                Console.WriteLine(first.BusStationKey+" is not exsist");
                return -1;
            }
            public double time(busStation first, busStation last)
            {
                double time = 0;
                if (isBusStation(first))
                {
                    if (isBusStation(last))
                    {
                        time += stations.distance(first, last) * TimeForMeter;
                    }
                    Console.WriteLine(last.BusStationKey + " is not exsist");
                }
                Console.WriteLine(first.BusStationKey + " is not exsist");
                if (time > -1)
                    return time;
                return -1;
            }
            public BusLine subBusLine(busStation first,busStation last)
            {
                if (isBusStation(first))
                {
                    if (isBusStation(last))
                    {
                        return (new BusLine(stations.creatNewBusStationList(first, last),area));
                    }
                    Console.WriteLine(last.BusStationKey + " is not exsist");
                }
                Console.WriteLine(first.BusStationKey + " is not exsist");
                return null;
            }
            public int CompareTo(object obj)
            {
                BusLine b = (BusLine)obj;
                if (this.stations.distanceWholeLine()==b.stations.distanceWholeLine())
                {
                    return 0;
                }
                if(this.stations.distanceWholeLine() > b.stations.distanceWholeLine())
                {
                    return 1;
                }
                return -1;
            }
        }
        class BusLineCollection : IEnumerable
        {
            private List<BusLine> busLines;
            IEnumerator<List<BusLine>> IEnumeratorBusStation;
            public List<BusLine> BusLines
            {
                get
                {
                    return busLines;
                }
            }
            public void add(BusLine bus)
            {
                IEnumerator<List<BusLine>> enumerator = busLines.GetEnumerator();
                busLines.Add(bus);
            }
            public IEnumerator GetEnumerator()
            {
                return IEnumeratorBusStation;
            }
        }
        static void Main(string[] args)
        {
        }
    }
}
