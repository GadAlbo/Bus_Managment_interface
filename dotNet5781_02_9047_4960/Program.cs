using System;
using System.Collections;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace dotNet5781_02_9047_4960
{
    class Program
    {
        /* foreach (BusLineStation busS in stations)
                {
                }*/
        const double TimeForMeter = 0.01; // the time who take us to move a meter
        public class BusStation
        {
            readonly Random r = new Random(DateTime.Now.Millisecond);// random number for the cordinate
            public static int staticBusStationKey = 1; // the key number- make sure that is no two stations with the same number  
            private int busStationKey;
            public int BusStationKey// the station number
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

            private readonly string address; // the station adress
            private GeoCoordinate coordinates;
            public GeoCoordinate Coordinates// the station cordinats
            {
                get
                {
                    return coordinates;
                }    
                set
                {
                    coordinates = value;
                }
            }


            public BusStation()//constractor
            {
                BusStationKey = staticBusStationKey; // add the station number
                staticBusStationKey++;
                double latitude = r.NextDouble() * (33.3 - 31) + 31;// random latitude
                double longitude = r.NextDouble() * (35.5 - 34.3) + 34.3;// random longitude
                coordinates = new GeoCoordinate();
                coordinates.Latitude = latitude;//add latitude
                coordinates.Longitude = longitude;//add longitude
                Console.WriteLine("Please give us the address of the station or enter");
                address = Console.ReadLine();// add adress
                Console.WriteLine("the bus station that was created is");
                Console.WriteLine(this);
            }
            public BusStation(int key) // create a station with key
            {
                BusStationKey = key;
            }
            public override string ToString() //override the toString func
            {
                return "Bus Station Code: " + BusStationKey + ", " + coordinates.Latitude + "°N " + coordinates.Longitude + "°E\n";
            }
            public double Distance(BusStation bs)// distance between this stations and bs
            {
                return ((coordinates.GetDistanceTo(bs.coordinates)));
            }
            public BusStation(BusStation original)
            {
                BusStationKey = original.busStationKey; // add the station number
                coordinates = original.Coordinates;
                address = original.address;
            }

        }
        public class BusLineStation : BusStation
        {
            private double distanceFromLastStation;
            public double DistanceFromLastStation { get { return distanceFromLastStation; } set { distanceFromLastStation = value; } } // diatance from privious station
            public double TimeFromLastStation { get { return distanceFromLastStation*TimeForMeter; } } // time is take to move between privious station
            public BusLineStation():base()// constractor
            {
                DistanceFromLastStation = 0;
            }
            public BusLineStation(int key): base(key)// constractor with key
            {
                DistanceFromLastStation = 0;
            }
            public BusLineStation(BusLineStation original):base(original)
            {
                distanceFromLastStation = original.distanceFromLastStation;
            }
        }
        public class BusStationCollention : IEnumerable
        {
            public List<BusLineStation> stations;// the list of the stations
            IEnumerator<BusLineStation> IEnumeratorBusStation;
            public BusStationCollention()// constractor
            {
                stations = new List<BusLineStation>();
                IEnumeratorBusStation =stations.GetEnumerator();
            }
            public IEnumerator GetEnumerator()// return iterator
            { return stations.GetEnumerator(); }
            public bool Equals(BusLineStation bs)// check if bs and this are the same
            {
                foreach (BusLineStation busS in stations)
                {
                    if (busS.BusStationKey == bs.BusStationKey)
                    {
                        return true;
                    }
                }

                return false;
            }
            public void Add(BusLineStation bs)//adds the new station to the nearest station
            { 
                int index = 0;
                double minDistance=0;
                IEnumerator<BusLineStation> IEnumeratorBusStation = stations.GetEnumerator();
                if (IEnumeratorBusStation.MoveNext())//moves to the first element
                {
                    IEnumerator<BusLineStation> IEnumeratorBusStationNext = IEnumeratorBusStation;
                    int count = 1;
                    index = count;
                    minDistance = IEnumeratorBusStation.Current.Distance(bs);    //set initial minimum distance to the distance
                    while (IEnumeratorBusStationNext.MoveNext())
                    {
                        
                        if (IEnumeratorBusStation.Current.Distance(bs) <= minDistance)
                        {
                            minDistance = IEnumeratorBusStation.Current.Distance(bs);
                            if (IEnumeratorBusStation.Current.Distance(bs) <= IEnumeratorBusStation.Current.Distance(IEnumeratorBusStationNext.Current))// if bs clother to the next
                            {
                                if (IEnumeratorBusStationNext.Current.Distance(bs) <= IEnumeratorBusStation.Current.Distance(IEnumeratorBusStationNext.Current))// if next closer to the first (and not to bs)
                                {
                                    index = count;
                                }
                                else
                                {
                                    index = count - 1;
                                }
                            }
                            else
                            {
                                index = count + 1;
                            }
                        }
                        count++;
                        IEnumeratorBusStation.MoveNext();
                    } 
                    
                }
                bs.DistanceFromLastStation = minDistance;
                stations.Insert(index, bs);
            }
            public void Remove(BusLineStation bs) //delete elemet
            {
                stations.Remove(bs);
            }
            public double Distance(BusLineStation first, BusLineStation last)// returm the distance between two stations
            {
                if (last.BusStationKey == first.BusStationKey)// if the first and the last station are same
                {
                    return 0;
                }
                IEnumerator<BusLineStation> IEnumeratorBusStation = stations.GetEnumerator();
                double Distance = 0;
                while (IEnumeratorBusStation.MoveNext())// while the list not end
                {
                    if (IEnumeratorBusStation.Current.BusStationKey == last.BusStationKey)// if the last station is before the first
                    {
                        return -1;
                    }
                    if (IEnumeratorBusStation.Current.BusStationKey == first.BusStationKey)// we in the first station
                    {
                        IEnumerator<BusLineStation> help = IEnumeratorBusStation;
                        while ((help.MoveNext()) && help.Current.BusStationKey != last.BusStationKey)// while we are not at last, and ir is not the end of the list 
                        {
                            Distance += IEnumeratorBusStation.Current.Distance(help.Current);// add the distance between the close stations
                            IEnumeratorBusStation.MoveNext();

                        }
                        if (help.Current == null)// if last is not in the list
                        {
                            return -1;
                        }
                        Distance += IEnumeratorBusStation.Current.Distance(help.Current);// add the distance between the privios and last station
                        return Distance;
                    }
                }
                return -1;
            }
            public BusStationCollention CreatNewBusStationCollention(BusLineStation first, BusLineStation last)// creat a BusStationCollention while first is the first station and last is the last
            {
                BusStationCollention bs = new BusStationCollention();
                IEnumerator<BusLineStation> IEnumeratorBusStation = stations.GetEnumerator();
                while (IEnumeratorBusStation.MoveNext())// while the list not end
                {
                    if (IEnumeratorBusStation.Current.BusStationKey == first.BusStationKey)
                    {
                        bs.Add(first);
                        while (IEnumeratorBusStation.MoveNext() && IEnumeratorBusStation.Current.BusStationKey != last.BusStationKey)
                        {
                            bs.Add(IEnumeratorBusStation.Current);
                        }
                        if (IEnumeratorBusStation.Current == null)// if last is not un the line
                        {
                            return null;
                        }
                        return bs;
                    }
                    Console.WriteLine("one or more ot the stations are not exsist");
                }
                return null;
            }
            public double DistanceWholeLine()// distance from the first to the last station
            {
                return Distance(stations.First<BusLineStation>(), stations.Last<BusLineStation>());// send the first and the last station to distance
            }
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
        public enum Areas { General, North, South, Center, Jerusalem };// the opsional areas
        public class BusLine : Bus, IComparable
        {
            private int busLineNumber;
            private static int staticBusLineNumber = 1;// static bus line number, make sure there is no two same
            private readonly BusStationCollention stations;// stations
            private readonly Areas area;// area
            public object FirstbusStation// first station
            {
                get
                {
                   return stations.stations[1];
                }
            }
            public object LastbusStation// last atation
            {
                get
                {
                    return stations.stations.Last();
                }
                set
                {
                    
                }
            }
            public int BusLineNumber// bus number
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
            public BusLine() : base()// constractor
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
            public BusLine(BusLine original, BusStationCollention bs, Areas a)// create a sub bus line
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
            public bool IsBusStation(BusLineStation bs)// check if the station exsist in the line
            {
                return stations.Equals(bs);
            }
            public override string ToString()// override tostring
            {
                return "BusLine: " + busLineNumber + " Area:" + area + "\n" + " stions:\n" + stations.ToString();
            }
            public void AddStition(BusLineStation bs)// add a station to the line
            {
                stations.Add(bs);
            }
            public void DeleteStition(int key)// delete a station from the line
            {
                stations.Remove(new BusLineStation(key));
            }
            public double Distance(BusLineStation first, BusLineStation last)// return the diistance between two stations
            {
                double distanceBetweenFiratAndLast = stations.Distance(first, last);// return the distance between first and last
                if (distanceBetweenFiratAndLast != -1)// if first before last
                {
                    return distanceBetweenFiratAndLast;
                }
                Console.WriteLine("last is before first or one of the stations does not exist");
                return -1;
            }
            public double Time(BusLineStation first, BusLineStation last)// return the time to drive between first and last 
            {
                double time = Distance(first, last) * TimeForMeter;// time*distance
                if (time >= 0)// if first is before last
                    return time;
                return -1;
            }
            public BusLine SubBusLine(BusLineStation first, BusLineStation last)// return a sub line
            {
                if (IsBusStation(first))// if first exsist
                {
                    if (IsBusStation(last))// if last exsist
                    {
                        return (new BusLine(this, stations.CreatNewBusStationCollention(first, last), area));// return the bus line between first and last
                    }
                    Console.WriteLine(last.BusStationKey + " does not exsist");
                }
                Console.WriteLine(first.BusStationKey + " does not exsist");
                return null;
            }
            public int CompareTo(object obj)// conpare, ovveride compare to
            {
                BusLine b = (BusLine)obj;// casting- obj to busLine
                if (this.stations.DistanceWholeLine() == b.stations.DistanceWholeLine())// the distances are equals
                {
                    return 0;
                }
                if (this.stations.DistanceWholeLine() > b.stations.DistanceWholeLine())// station distance is bigest
                {
                    return 1;
                }
                return -1;
            }
            public BusLineStation this[int i]
            {
                get
                {
                    foreach (BusLineStation bs in stations)
                    {
                        if (bs.BusStationKey==i)
                            return bs;
                    }
                    throw new ArgumentException();
                }

            }
        }
        public class BusLineCollection : IEnumerable
        {
            private readonly List<BusLine> busLines;
            public IEnumerator<BusLine> IEnumeratorBusStation;
            public BusLineCollection()
            {
                busLines = new List<BusLine>();
                IEnumeratorBusStation = busLines.GetEnumerator();
            }
            public List<BusLine> BusLines
            {
                get
                {
                    return busLines;
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
                List<BusLine> lines = new List<BusLine>();
                BusLineStation station = new BusLineStation(StationNumber);
                foreach (BusLine bs in busLines)
                {
                    try
                    {
                        
                        if (bs.IsBusStation(station))
                        {
                            lines.Add(bs);
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("there is no lines in this station or this station is not exist");
                    }
                }
                return lines;
            }
            public IEnumerator GetEnumerator()
            {
                return busLines.GetEnumerator();
            }
            public int FindAline(int number)
            {
                int count = 0;
                IEnumerator<BusLine> IEnumeratorBusLines = busLines.GetEnumerator();
                foreach(BusLine busL in busLines)
                {
                    count++;
                    if (busL.BusLineNumber == number)
                        return count;
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
                    foreach  (BusLine bs in busLines)
                    {
                        if (bs.BusLineNumber == i)
                            return bs;
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
                                    if (busLines.FindAline(input) != -1)
                                    {
                                        Console.WriteLine("1 for a new station 2 for an existing satation");
                                        int option;
                                        while ((!Int32.TryParse(Console.ReadLine(), out option)) && ((option != 1) && (option != 2)))       //trying to get the users chosen option
                                        {
                                            Console.WriteLine("only enter numbers 1 or 2");
                                        }
                                        if (option == 1)
                                        {
                                            busLines[busLines.FindAline(input)].AddStition(new BusLineStation());
                                            Console.WriteLine("the add was sucsesful");
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("enter the existing bus station number that you want to add");
                                            while (!Int32.TryParse(Console.ReadLine(), out input))        //trying to get the users chosen option
                                            {
                                                Console.WriteLine("only enter numbers");
                                            }
                                            BusLineStation theWantedStation=null;
                                            bool flage = false;
                                            foreach (BusLine bs in busLines)
                                            {
                                                if(bs.BusLineNumber!= input)
                                                {
                                                    try
                                                    {
                                                        theWantedStation =new BusLineStation (bs[input]);
                                                        flage = true;
                                                    }
                                                    catch { }
                                                }
                                            }
                                            if(flage)
                                            {
                                                busLines[busLines.FindAline(input)].AddStition(theWantedStation);
                                                Console.WriteLine("the add was sucsesful");
                                            }
                                            else
                                            {
                                                throw new Exception();//the station was never found
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception();
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("this bus line does not exist");
                                }
                            }
                            break;
                        }
                    case Opitions.delete:             //refuel or checkup bus
                        {
                            int opitions;
                            Console.WriteLine("enter one to delete a bus line, and 2 to delete a station");
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
                                else if (opitions == 2)
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
                                            busLines[busLines.FindAline(input2)].DeleteStition(input2);
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
                                else
                                {
                                    Console.WriteLine("Can not translate the desired result base on the input, please try again");
                                }
                            }

                            catch (Exception)
                            {
                                Console.WriteLine("this bus line does not exsist");
                            }
                            break;
                        }
                    case Opitions.search:          //to view the number of killometers each bus has traveled since the last treatment
                        {
                            int opitions;
                            Console.WriteLine("enter one for serch a statuon's bus lines, and 2 for search a route between two stations");
                            while (!Int32.TryParse(Console.ReadLine(), out opitions))        //trying to get the users chosen option
                            {
                                Console.WriteLine("only enter numbers");
                            }
                            if (opitions == 1)
                            {
                                Console.WriteLine("please enter a bus station munber");
                                int input1;
                                while (!Int32.TryParse(Console.ReadLine(), out input1))        //trying to get the users chosen option
                                {
                                    Console.WriteLine("only enter numbers");
                                }
                                List<BusLine> buses = (busLines.LinesAtStation(input1));
                            }
                            if (opitions == 2)
                            {
                                Console.WriteLine("please enter a  sorce bus station munber and a destination bus station munber");
                                int input1;
                                while (!Int32.TryParse(Console.ReadLine(), out input1))        //trying to get the users chosen option
                                {
                                    Console.WriteLine("only enter numbers");
                                }
                                int input2;
                                while (!Int32.TryParse(Console.ReadLine(), out input2))        //trying to get the users chosen option
                                {
                                    Console.WriteLine("only enter numbers");
                                }
                                BusLineCollection lineCollection = new BusLineCollection();
                                List<BusLine> buses1 = busLines.LinesAtStation(input1);
                                for (int i = 0; i < buses1.Count; i++)
                                {
                                    if (buses1[i].IsBusStation(new BusLineStation(input2)))
                                    {
                                        buses1[i].LastbusStation = new BusLineStation(input2);
                                        lineCollection.Add(buses1[i]);
                                    }
                                }
                                lineCollection.Sort();
                            }
                            break;
                        }
                    case Opitions.print:
                        {
                            Console.WriteLine("if you want to print all of the lines- enter one, if you want to print all of the stations- enter 2");
                            int opitions;
                            while (!Int32.TryParse(Console.ReadLine(), out opitions))        //trying to get the users chosen option
                            {
                                Console.WriteLine("only enter numbers");
                            }
                            if (opitions == 1)
                            {
                                foreach (BusLine bus in busLines)
                                {
                                    Console.WriteLine(bus);
                                }
                            }
                            if (opitions == 2)
                            {
                                for (int i = 0; i < BusStation.staticBusStationKey; i++)
                                {
                                    if (busLines.LinesAtStation(i) != null)
                                    {
                                        Console.WriteLine("the station number is" + i);
                                        Console.WriteLine(busLines.LinesAtStation(i));
                                    }
                                }
                                break;
                            }
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

/*2000
1
12
0000001
1
1
12
4
1
Addres1_1
Addres2_1
Addres3_1
Addres4_1
2003
2
23
0000002
1
1
23
4
1
Addres1_2
Addres2_2
Addres3_2
Addres4_2
2006
3
12
0000003
1
1
45
4
1
Addres1_3
Addres2_3
Addres3_3
Addres4_3
2009
4
5
0000004
1
1
54
4
1
Addres1_4
Addres2_4
Addres3_4
Addres4_4
2012
5
2
0000005
1
1
87
4
1
Addres1_5
Addres2_5
Addres3_5
Addres4_5
2015
6
2
0000006
1
1
8
4
1
Addres1_6
Addres2_6
Addres3_6
Addres4_6
2018
7
12
00000007
1
1
12
4
1
Addres1_7
Addres2_7
Addres3_7
Addres4_7
2021
8
1
00000008
1
1
12
4
1
Addres1_8
Addres2_8
Addres3_8
Addres4_8
2024
9
22
00000009
1
1
12
4
1
Addres1_9
Addres2_9
Addres3_9
Addres4_9
2027
10
1
00000010
1
1
12
4
1
Addres1_10
Addres2_10
Addres3_10
Addres4_10
*/