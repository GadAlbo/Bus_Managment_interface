using System;
using System.Collections;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dotNet5781_02_9047_4960
{
    class Program
    {    
        const double TimeForMeter = 0.01;
        public class BusStation
        {
            readonly Random r = new Random(DateTime.Now.Millisecond);
            private static int staticBusStationKey = 1;
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

            private readonly string address;
            private readonly GeoCoordinate coordinates;
            public GeoCoordinate Coordinates
            {
                get
                {
                    return coordinates;
                }
            }


            public BusStation()
            {
                BusStationKey = staticBusStationKey;
                staticBusStationKey++;
                double latitude = r.NextDouble() * (33.3 - 31) + 31;
                double longitude = r.NextDouble() * (35.5 - 34.3) + 34.3;
                coordinates.Latitude = latitude;
                coordinates.Longitude = longitude;
                Console.WriteLine("Please give us the address of the station or enter");
                address = Console.ReadLine();
                Console.WriteLine("the bus station that was created is");
                Console.WriteLine(this);
            }
            public BusStation(int key)
            {
                BusStationKey = key;
            }
            public override string ToString() //override the toString func
            {
                return "Bus Station Code: " + BusStationKey + ", " + coordinates.Latitude + "°N " + coordinates.Longitude + "°E\n";
            }
            public double Distance(BusStation bs)
            {
                return ((coordinates.GetDistanceTo(bs.coordinates)));
            }

        }
        public class BusLineStation : BusStation
        {
            private double distanceFromLastStation;
            public double DistanceFromLastStation { get { return distanceFromLastStation; } set { distanceFromLastStation = value; } }
            public double TimeFromLastStation { get { return distanceFromLastStation*TimeForMeter; }}
            public BusLineStation()
            {
                DistanceFromLastStation = 0;
            }
            public BusLineStation(int key): base(key)
            {
                DistanceFromLastStation = 0;
            }
        }

        public class BusStationCollention : IEnumerable, IEnumerator<BusLineStation>
        {
            public List<BusLineStation> stations;
            IEnumerator<BusLineStation> IEnumeratorBusStation;
            public BusStationCollention()
            {
                stations = new List<BusLineStation>();
                IEnumeratorBusStation =stations.GetEnumerator();
            }
            public IEnumerator GetEnumerator()
            { return IEnumeratorBusStation; }
            public BusLineStation Current
            { get
                { return IEnumeratorBusStation.Current; } 
            }

            object IEnumerator.Current => IEnumeratorBusStation.Current;

            public bool MoveNext()
            {
                return IEnumeratorBusStation.MoveNext();
            }
            public void Reset()
            {
                IEnumeratorBusStation = stations.GetEnumerator();
            }
            public bool Equals(BusLineStation bs)
            {
                IEnumerator<BusLineStation> IEnumeratorBusStation = stations.GetEnumerator();
                while (IEnumeratorBusStation.MoveNext())
                {
                    if (IEnumeratorBusStation.Current.BusStationKey == bs.BusStationKey)
                    {
                        return true;
                    }
                }
                return false;
            }
            public void Add(BusLineStation bs)//adds the new station to the nearest station
            {
                int count = 0;
                int index = 0;
                IEnumerator<BusLineStation> IEnumeratorBusStation = stations.GetEnumerator();
                if (IEnumeratorBusStation.MoveNext())//moves to the first element
                {
                    double minDistance = IEnumeratorBusStation.Current.Distance(bs);    //set initial minimum distance to the distance
                    while (IEnumeratorBusStation.MoveNext())
                    {
                        if (IEnumeratorBusStation.Current.Distance(bs) < minDistance)
                        {
                            minDistance = IEnumeratorBusStation.Current.Distance(bs);
                            index = count;

                        }
                        count++;
                    }
                    bs.DistanceFromLastStation = minDistance;
                }
                stations.Insert(index, bs);
            }
            public void Remove(BusLineStation bs)
            {
                stations.Remove(bs);
            }
            public double Distance(BusLineStation first, BusLineStation last)
            {
                if (last.BusStationKey == first.BusStationKey)
                {
                    return 0;
                }
                IEnumerator<BusLineStation> IEnumeratorBusStation = stations.GetEnumerator();
                double Distance = 0;
                while (IEnumeratorBusStation.MoveNext())
                {
                    if (IEnumeratorBusStation.Current.BusStationKey == last.BusStationKey)
                    {
                        return -1;
                    }
                    if (IEnumeratorBusStation.Current.BusStationKey == first.BusStationKey)
                    {
                        IEnumerator<BusLineStation> help = IEnumeratorBusStation;
                        while ((help.MoveNext()) && help.Current.BusStationKey != last.BusStationKey)
                        {
                            Distance += IEnumeratorBusStation.Current.Distance(help.Current);
                            IEnumeratorBusStation.MoveNext();

                        }
                        if (help.Current == null)
                        {
                            return -1;
                        }
                        Distance += IEnumeratorBusStation.Current.Distance(help.Current);
                        return Distance;
                    }
                }

                return -1;
            }
            public BusStationCollention CreatNewBusStationCollention(BusLineStation first, BusLineStation last)
            {
                BusStationCollention bs = new BusStationCollention();
                IEnumerator<BusLineStation> IEnumeratorBusStation = stations.GetEnumerator();
                while (IEnumeratorBusStation.MoveNext())
                {
                    if (IEnumeratorBusStation.Current.BusStationKey == first.BusStationKey)
                    {
                        bs.Add(first);
                        while (IEnumeratorBusStation.MoveNext() && IEnumeratorBusStation.Current.BusStationKey != last.BusStationKey)
                        {
                            bs.Add(IEnumeratorBusStation.Current);
                        }
                        if (IEnumeratorBusStation.Current == null)
                        {
                            return null;
                        }
                        return bs;
                    }
                    Console.WriteLine("one or more ot the stations are not exsist");
                }
                return null;
            }
            public double DistanceWholeLine()
            {
                return Distance(stations.First<BusLineStation>(), stations.Last<BusLineStation>());
            }

            public void Dispose()
            { }//IEnumerator<t> requires me to have this func but i dont need it
        }
        public class Bus
        {
            private string licenseNumber;
            public string LicenseNumber //property
            {
                get
                {
                    return licenseNumber;
                }
                protected set
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
                protected set
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

        public enum Areas { General, North, South, Center, Jerusalem };
        public class BusLine : Bus, IComparable
        {
            private int busLineNumber;
            private static int staticBusLineNumber = 1;
            private readonly BusStationCollention stations;
            private readonly Areas area;
            public object FirstbusStation
            {
                get
                {
                   return stations.stations[1];
                }
            }
            public object LastbusStation
            {
                get
                {
                    return stations.stations.Last();
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
                    busLineNumber = value;
                }
            }
            public BusLine() : base()
            {
                stations = new BusStationCollention();
                busLineNumber = staticBusLineNumber;
                staticBusLineNumber++;
                Console.WriteLine("In what area is the bus located?\n" + "0-General, 1-North, 2-South, 3-Center, 4-Jerusalem, enter the coresponding number");
                int optionA;
                while (!Int32.TryParse(Console.ReadLine(), out optionA))        //trying to get the users chosen option
                {
                    Console.WriteLine("only enter numbers");
                }
                area = (Areas)optionA;
            }
            public BusLine(BusLine original, BusStationCollention bs, Areas a)
            {
                this.BusLineNumber = original.BusLineNumber;
                this.StartOfActivity = original.StartOfActivity;
                this.LicenseNumber = original.LicenseNumber;
                this.LastCheckup = original.LastCheckup;
                this.KillFromRefueling = original.KillFromRefueling;
                this.KillFromLastCheckup = original.KillFromLastCheckup;
                this.AllKilometrage = original.AllKilometrage;
                this.busLineNumber = original.BusLineNumber;
                stations = bs;
                area = a;
            }
            public bool IsBusStation(BusLineStation bs)
            {
                return stations.Equals(bs);
            }
            public override string ToString()
            {
                return "BusLine: " + busLineNumber + " Area:" + area + "\n" + " stions:\n" + stations.ToString();
            }
            public void AddStition(BusLineStation bs)
            {
                stations.Add(bs);
            }
            public void DeleteStition(int key)
            {
                stations.Remove(new BusLineStation(key));
            }
            public double Distance(BusLineStation first, BusLineStation last)
            {
                double distanceBetweenFiratAndLast = stations.Distance(first, last);
                if (distanceBetweenFiratAndLast != -1)
                {
                    return distanceBetweenFiratAndLast;
                }
                Console.WriteLine("last is before first or one of the stations does not exist");
                return -1;
            }
            public double Time(BusLineStation first, BusLineStation last)
            {
                double time = Distance(first, last) * TimeForMeter;
                if (time >= 0)
                    return time;
                return -1;
            }
            public BusLine SubBusLine(BusLineStation first, BusLineStation last)
            {
                if (IsBusStation(first))
                {
                    if (IsBusStation(last))
                    {
                        return (new BusLine(this, stations.CreatNewBusStationCollention(first, last), area));
                    }
                    Console.WriteLine(last.BusStationKey + " does not exsist");
                }
                Console.WriteLine(first.BusStationKey + " does not exsist");
                return null;
            }
            public int CompareTo(object obj)
            {
                BusLine b = (BusLine)obj;
                if (this.stations.DistanceWholeLine() == b.stations.DistanceWholeLine())
                {
                    return 0;
                }
                if (this.stations.DistanceWholeLine() > b.stations.DistanceWholeLine())
                {
                    return 1;
                }
                return -1;
            }
        }
        public class BusLineCollection : IEnumerable, IEnumerator
        {
            private readonly List<BusLine> busLines;
            public IEnumerator<BusLine> IEnumeratorBusStation;
            public List<BusLine> BusLines
            {
                get
                {
                    return busLines;
                }
            }

            public object Current
            {
                get
                {
                    return IEnumeratorBusStation.Current;
                }
            }

            public void Add(BusLine bus)
            {
                busLines.Add(bus);
            }
            public void Remove(BusLine bus)
            {
                busLines.Remove(bus);
            }
            public List<BusLine> LinesAtStation(int StationNumber)
            {
                bool flag = false;
                List<BusLine> lines = new List<BusLine>();
                IEnumerator<BusLine> IEnumeratorBusLines = busLines.GetEnumerator();
                BusLineStation station = new BusLineStation(StationNumber);
                while (IEnumeratorBusLines.MoveNext())
                {
                    try

                    {
                        if (IEnumeratorBusLines.Current.IsBusStation(station))
                        {
                            lines.Add(IEnumeratorBusLines.Current);
                            flag = true;
                        }
                        if (flag == false)
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("there is no lines in this station");
                    }
                }
                return lines;
            }
            public IEnumerator GetEnumerator()
            {
                return IEnumeratorBusStation;
            }

            public bool MoveNext()
            {
                return IEnumeratorBusStation.MoveNext();
            }

            public void Reset()
            {
                IEnumeratorBusStation.Reset();
            }
            public int FindAline(int number)
            {
                int count = 0;
                IEnumerator<BusLine> IEnumeratorBusLines = busLines.GetEnumerator();
                while (IEnumeratorBusLines.MoveNext())
                {
                    if (IEnumeratorBusLines.Current.BusLineNumber == number)
                        return count;
                    count++;

                }
                return -1;
            }
            public List<BusLine> Sort()
            {
                busLines.Sort();
                return busLines;
            }
            public BusLine this[int i]
            {
                get
                {
                    IEnumerator<BusLine> IEnumeratorBusLines = busLines.GetEnumerator();
                    while (IEnumeratorBusLines.MoveNext())
                    {
                        if (IEnumeratorBusLines.Current.BusLineNumber == i)
                            return IEnumeratorBusLines.Current;
                    }

                    throw new ArgumentException();
                }

            }
        }


        enum Opitions { add = 0, delete, search, print, exit };   //enum definition
        static void Main(string[] args)
        {
            BusLineCollection busLines = new BusLineCollection();
            for (int i = 0; i < 10; i++)
            {
                BusLine bus = new BusLine();
                for (int j = 0; j < 4; j++)
                {
                    BusLineStation station = new BusLineStation();
                    bus.AddStition(station);
                }
                busLines.Add(bus);
            }
            Opitions op;
            int optionC;
            do
            {
                Console.WriteLine(
                   "plese enter 0 to add a bus line or a station\n" +
                   "plese enter 1 to remove a bus line or a station\n" +
                   "plese enter 2 to search \n" +
                   "plese enter 3 to print\n" +
                   "plese enter 4 to exit");
                while (!Int32.TryParse(Console.ReadLine(), out optionC))        //trying to get the users chosen option
                {
                    Console.WriteLine("only enter numbers");
                }
                op = (Opitions)optionC;
                switch (op)
                {
                    case Opitions.add:
                        {
                            int opitions;
                            Console.WriteLine("enter one for add a bus line, and 2 for add a station");
                            while (!Int32.TryParse(Console.ReadLine(), out opitions))        //trying to get the users chosen option
                            {
                                Console.WriteLine("only enter numbers");
                            }
                            if (opitions == 1)
                            {
                                busLines.Add(new BusLine());
                            }
                            if (opitions == 2)
                            {
                                try
                                {
                                    Console.WriteLine("please enter a bus line number");
                                    int input;
                                    while (!Int32.TryParse(Console.ReadLine(), out input))        //trying to get the users chosen option
                                    {
                                        Console.WriteLine("only enter numbers");
                                    }
                                    BusLineStation st = new BusLineStation();
                                    if (busLines.FindAline(input) != -1)
                                    {
                                        busLines[busLines.FindAline(input)].AddStition(st);
                                    }
                                    else
                                    {
                                        throw new Exception();
                                     }
                                }
                                catch(Exception)
                                {
                                    Console.WriteLine("this bus line is not exist");
                                }
                            }
                            break;
                        }
                    case Opitions.delete:             //refuel or checkup bus
                        {
                            int opitions;
                            Console.WriteLine("enter one for delete a bus line, and 2 for delete a station");
                            while (!Int32.TryParse(Console.ReadLine(), out opitions))        //trying to get the users chosen option
                            {
                                Console.WriteLine("only enter numbers");
                            }
                            try
                            {
                                if (opitions == 1)
                               {
                                    Console.WriteLine("please enter a bus line number");
                                    int input;
                                    while (!Int32.TryParse(Console.ReadLine(), out input))        //trying to get the users chosen option
                                    {
                                        Console.WriteLine("only enter numbers");
                                    }
                                    if (busLines.FindAline(input) != -1)
                                    {
                                        busLines.Remove(busLines[busLines.FindAline(input)]);
                                    }
                                    else
                                    {
                                        throw new Exception();
                                    }
                                    }
                                    if (opitions == 2)
                                    {
                                        Console.WriteLine("please enter a bus line number");
                                        int input1;
                                        while (!Int32.TryParse(Console.ReadLine(), out input1))        //trying to get the users chosen option
                                        {
                                            Console.WriteLine("only enter numbers");
                                        }
                                        if (busLines.FindAline(input1) != -1)
                                        {
                                            Console.WriteLine("please enter a station number number");
                                            int input2;
                                            while (!Int32.TryParse(Console.ReadLine(), out input2))        //trying to get the users chosen option
                                            {
                                                Console.WriteLine("only enter numbers");
                                            }
                                            if (busLines[busLines.FindAline(input2)].Equals(input2))
                                            {
                                                busLines[busLines.FindAline(input2)].DeleteStition(new BusLineStation(input2));
                                            }
                                            else
                                            {
                                                throw new Exception();
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception();
                                        }
                                    }
                            }
                            catch (Exception)
                            {
                                    Console.WriteLine("this bus line is not exsist");
                            }          
                            break;
                        }
                    case Opitions.print:          //to view the number of killometers each bus has traveled since the last treatment
                        {
                            int opitions;
                            Console.WriteLine("enter one for serch bus line, and 2 for search a route between two stations");
                            while (!Int32.TryParse(Console.ReadLine(), out opitions))        //trying to get the users chosen option
                            {
                                Console.WriteLine("only enter numbers");
                            }
                            if (opitions == 1)
                            {
                    
                            
                            }
                            break;
                        }
                    case Opitions.search:
                        {
                            break;
                        }
                    case Opitions.exit:
                        {
                            Console.WriteLine("have a nice day");
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("you enter a wrong number :( , plese try again or enter 4 to exit");
                            break;
                        }
                }

            } while (op != (Opitions)4);
        }
    }
}

