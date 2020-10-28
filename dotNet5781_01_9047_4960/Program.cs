using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_01_9047_4960
{
    class Program
    {
        class bus
        {
            const int maxKilometrage = 20000;
            private string licenseNumber;
            public string LicenseNumber 
            { 
                get
                {
                    return licenseNumber;
                }
                private set 
                {
                    if (startOfActivity.Year<2018)
                    {
                        if (value.Length == 7)
                        {
                            licenseNumber = value[0]+ value [1]+"-"+value[2]+value[3] + value[4] + "-"+value[5] + value[6];
                        }
                        else
                        {
                            Console.WriteLine("license number is not compatible with the year of departure");
                        }
                    }
                    else
                    {
                        if (value.Length == 8)
                        {
                            licenseNumber = value[0] + value[1] + value[2] + "-" + value[3] + value[4] + "-" + value[5] + value[6] + value[7]; ;
                        }
                        else
                        {
                            Console.WriteLine("license number is not compatible with the year of departure");
                        }
                    }
                } 
            }
            private DateTime startOfActivity;
            public DateTime StartOfActivity
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
            private DateTime LastCheckup = new DateTime();
            public DateTime lastCheckup
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
            public int AllKilometrage 
            {
                get
                {
                    return allKilometrage;
                }
                set
                {
                    if(value>0)
                        allKilometrage = value;
                    else
                        Console.WriteLine("kilometrage sould be positive");
                }
            }
            
            private int killFromLastCheckup=0;
            public int KillFromLastCheckup
            {
                get
                {
                    return killFromLastCheckup;
                }
                set 
                {
                    killFromLastCheckup = value;
                }
            }
            private int killFromRefueling=0;
            public int KillFromRefueling
            {
                get
                {
                    return killFromRefueling;
                }
                 set
                {
                    killFromRefueling = value;
                }
            }
            bus()
            {
                try
                {
                    Console.WriteLine("Please Enter The Starting Date Of Activity");
                    Console.Write("year:");
                    int yearSA = Console.Read();
                    Console.Write(" mounth:");
                    int mounthSA = Console.Read();
                    Console.Write(" day:");
                    int daySA = Console.Read();
                    startOfActivity = new DateTime(yearSA, mounthSA, daySA);
                }
                catch (ArgumentOutOfRangeException outOfRange)
                {
                    Console.WriteLine("ERROR: {0} in the start activity date", outOfRange.Message);
                }
                Console.Write("Please Enter The License Number No Spaces Or - :");
                LicenseNumber = Console.ReadLine();
                Console.WriteLine("If You Would Like To Tell Us More About The Vehicle Please Enter 1. else press any key to continue");
                var decison = (char)Console.Read();
                if(decison=='1')
                {
                    char op;
                   
                    do
                    {
                        Console.WriteLine("enter 0 to add last checkup date\n"
                        +"enter 1 to add the amount of all the killometers the car has\n"
                        +"enter 2 to add the amount of all the killometers from last checkup\n"
                        +"enter 3 to add the amount of all the killometers the car has from last refuel"
                        +"press e to end");
                        op= Console.ReadKey();
                        switch (op)
                            {
                            case '0':
                                try
                                {
                                    Console.WriteLine("Please Enter The Last Checkup Date");
                                    Console.Write("year:");
                                    int yearC = Console.Read();
                                    Console.Write(" mounth:");
                                    int mounthC = Console.Read();
                                    Console.Write(" day:");
                                    int dayC = Console.Read();
                                    LastCheckup = new DateTime(yearC, mounthC, dayC);
                                }
                                catch (ArgumentOutOfRangeException outOfRange)
                                {
                                    Console.WriteLine("Error: {0} in the last checkup date", outOfRange.Message);
                                }
                                break;
                            case '1':
                                Console.WriteLine("Please Enter The total kl amount");
                                int allk = Console.ReadLine();
                                AllKilometrage = allk;
                                break;
                            case '2':
                                Console.WriteLine("Please Enter The  kl amount from last checkup");
                                int kilometersC = Console.ReadLine();
                                KillFromLastCheckup = kilometersC;
                                break;
                            case '3':
                                Console.WriteLine("Please Enter The  kl amount from last refuel");
                                int kilometersR = Console.ReadLine();
                                KillFromRefueling = kilometersR;
                                break;
                                default:
                                    break;
                            }

                    } while (op!='e');
                }
            }
            public void AddKilometrage(int addKill)
            {
                    if(addKill > 0)
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
        }
        enum Opitions { addBus, chooseBus, busTreatment, showKillFromLastCheckup, exit };
        static void Main(string[] args)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            List<bus> busList = new List<bus>();
            Opitions op;
            do
            {
                Console.WriteLine(
                    "plese enter 0 to add a new bus\n" +
                    "plese enter 1 to choose bus for a ride\n" +
                    "plese enter 2 to refuel or treat bus\n" +
                    "plese enter 3 to view the number of killometers each bus has traveled since the last treatment\n" +
                    "plese enter 4 to exit\n");
                op = (Opitions)Console.Read();
                switch (op)
                {
                    case Opitions.addBus:
                        break;
                    case Opitions.chooseBus:
                        {
                            Console.WriteLine("plese enter a bus license number\n");
                            string Lnumber = Console.ReadLine();
                            int random= r.Next();
                            IEnumerator<bus> t = busList.GetEnumerator();
                            bool check = false;
                            while((t.MoveNext()&&(check==false)))
                            {
                                if(t.Current.LicenseNumber==Lnumber)
                                {
                                    int help = t.Current.KillFromLastCheckup;
                                    int help1 = t.Current.KillFromRefueling;
                                    if(help+random>20000)
                                    {
                                        Console.WriteLine("you can not drive more than 20000 killometrs without checkup\n");
                                        check = true;
                                    }
                                    else if(help1 + random > 1200)
                                    {
                                        Console.WriteLine("you can not drive more than 1200 killometrs without refuel\n");
                                        check = true;
                                    }
                                    else
                                    {
                                        t.Current.AddKilometrage(random);
                                    }
                                }

                            }
                            Console.WriteLine("this bus license number is not exists\n ");
                            break;
                        }
                    case Opitions.busTreatment:
                        {
                            Console.WriteLine("plese enter a bus license number\n");
                            string Lnumber = Console.ReadLine();
                            IEnumerator<bus> t = busList.GetEnumerator();
                            while ((t.MoveNext()))
                            {
                                if (t.Current.LicenseNumber == Lnumber)
                                {
                                    Console.WriteLine("plese enter 1 for refuel and 2 treat\n");
                                    int help = Console.Read();
                                    if(help==1)
                                    {
                                        t.Current.KillFromRefueling = 0;
                                    }
                                    else if(help==2)
                                    {
                                        t.Current.KillFromLastCheckup = 0;
                                        t.Current.lastCheckup= DateTime.Now; ;
                                    }
                                    else
                                    {
                                        Console.WriteLine("you not enter 1 or 2 :( plese try again\n");
                                        break;
                                    }
                                }
                            }
                            Console.WriteLine("this bus license number is not exists\n");


                            break;
                        }
                    case Opitions.showKillFromLastCheckup:
                        {
                           foreach(bus abus in busList)
                            {
                                Console.WriteLine(abus);
                            }
                                break;
                        }
                    case Opitions.exit:
                        break;
                    default:
                        break;
                }
            } while (op != (Opitions)4);
        }
    }
}
