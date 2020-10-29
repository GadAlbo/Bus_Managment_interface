using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dotNet5781_01_9047_4960
{
    public class Program
    {
        class Bus
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
                    bool flag = true;
                    while (flag)
                    {
                        if (startOfActivity.Year<2018)
                        {
                            if (value.Length == 7)
                            {
                                flag = false;
                                licenseNumber = value[0] + ""+ value [1]+"-"+value[2]+value[3] + value[4] + "-"+value[5] + value[6];
                            }
                            else
                            {
                                Console.WriteLine("license number is not compatible with the year of departure");
                                Console.Write("Please Enter The License Number No Spaces Or - :");
                                value = Console.ReadLine();
                            }
                        }
                        else
                        {
                            if (value.Length == 8)
                            {
                                flag = false;
                                licenseNumber = value[0]+"" + value[1]+"" + value[2] + "-" + value[3] + value[4] + "-" + value[5] + value[6] + value[7]; ;
                            }
                            else
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
            private DateTime lastCheckup = new DateTime();
            public DateTime LastCheckup
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
                    if (value >= 0)
                    {
                        killFromLastCheckup = value;
                    }
                    else
                    {
                        Console.WriteLine("kilometrage sould be positive");
                    }
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
                    if(value>=0)
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
                while(flag)
                {
                    try
                    {
                        Console.WriteLine("Please Enter The Starting Date Of Activity");
                        Console.Write("year:");
                        int yearSA;
                        while(!Int32.TryParse(Console.ReadLine(),out yearSA))
                        {
                            Console.WriteLine("only enter numbers\n"+ "year:");
                        }
                        Console.Write("mounth:");
                        int mounthSA; 
                        while(!Int32.TryParse(Console.ReadLine(), out mounthSA))
                        {
                            Console.WriteLine("only enter numbers\n" + "mounth:");
                        }
                        Console.Write("day:");
                        int daySA;
                        while(!Int32.TryParse(Console.ReadLine(), out daySA))
                        {
                            Console.WriteLine("only enter numbers\n" + "day:");
                        }
                   
                        startOfActivity = new DateTime(yearSA, mounthSA, daySA);
                        flag = false;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("ERROR:start activity date invalid, please try again");
                    }
                }
                flag = true;
                Console.Write("Please Enter The License Number No Spaces Or - :");
                LicenseNumber = Console.ReadLine();
                Console.WriteLine("If You Would Like To Tell Us More About The Vehicle Please Enter 1. else press any other number to continue");
                int decison;
                while(!Int32.TryParse(Console.ReadLine(),out decison))
                {
                    Console.WriteLine("only enter numbers");
                }
                if(decison==1)
                {
                    int op;
                   
                    do
                    {
                        Console.WriteLine("enter 0 to add last checkup date\n"
                        +"enter 1 to add the amount of all the killometers the car has\n"
                        +"enter 2 to add the amount of all the killometers from last checkup\n"
                        +"enter 3 to add the amount of all the killometers the car has from last refuel\n"
                        +"press 4 to end");
                        while(!Int32.TryParse(Console.ReadLine(),out op))
                        {
                            Console.WriteLine("only enter numbers");
                        }
                        switch (op)
                            {
                            case 0:
                                while (flag)
                                {
                                    try
                                    {
                                        Console.WriteLine("Please Enter The Last Checkup Date");
                                        Console.Write("year:");
                                        int yearC;
                                        while(!Int32.TryParse(Console.ReadLine(), out yearC))
                                        {
                                            Console.WriteLine("only enter numbers\n"+ "year:");
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
                            case 1:
                                Console.WriteLine("Please Enter The total kl amount");
                                int allk;
                                while (!Int32.TryParse(Console.ReadLine(), out allk))
                                {
                                    Console.WriteLine("only enter numbers\n" + "total kl:");
                                }
                                AllKilometrage = allk;
                                break;
                            case 2:
                                Console.WriteLine("Please Enter The  kl amount from last checkup");
                                int kilometersC;
                                while (!Int32.TryParse(Console.ReadLine(), out kilometersC))
                                {
                                    Console.WriteLine("only enter numbers\n" + "kl amount from last checkup:");
                                }
                                KillFromLastCheckup = kilometersC;
                                break;
                            case 3:
                                Console.WriteLine("Please Enter The  kl amount from last refuel");
                                int kilometersR;
                                while (!Int32.TryParse(Console.ReadLine(), out kilometersR))
                                {
                                    Console.WriteLine("only enter numbers\n" + "kl amount from last refuel:");
                                }
                                KillFromRefueling = kilometersR;
                                break;
                            case 4:
                                break;
                            default:
                                Console.WriteLine("please try again:)");
                                break;
                            }

                    } while (op!=4);
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
            public override string ToString() 
            {
                return "License Number:" + licenseNumber + " Kilometrage from last checkup:" + killFromLastCheckup;
            }
        }
        enum Opitions { addBus, chooseBus, busTreatment, showKillFromLastCheckup, exit };
        static void Main(string[] args)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            List<Bus> busList = new List<Bus>();
            Opitions op;
            do
            {
                Console.WriteLine(
                    "plese enter 0 to add a new bus\n" +
                    "plese enter 1 to choose bus for a ride\n" +
                    "plese enter 2 to refuel or treat bus\n" +
                    "plese enter 3 to view the number of killometers each bus has traveled since the last treatment\n" +
                    "plese enter 4 to exit\n");
                int optionC;
                while (!Int32.TryParse(Console.ReadLine(), out optionC))
                {
                    Console.WriteLine("only enter numbers");
                }
                op = (Opitions)optionC;
                switch (op)
                {
                    case Opitions.addBus:
                        {
                            Bus b=new Bus();
                            busList.Add(b);
                        }
                        ;
                        break;
                    case Opitions.chooseBus:
                        {
                            Console.WriteLine("plese enter a Bus license number\n");
                            string Lnumber = Console.ReadLine();
                            int random= r.Next(0,1200);
                            IEnumerator<Bus> t = busList.GetEnumerator();
                            bool check = false;
                            while((t.MoveNext()))
                            {
                                if(t.Current.LicenseNumber==Lnumber)
                                {
                                    int help = t.Current.KillFromLastCheckup;
                                    int help1 = t.Current.KillFromRefueling;
                                    if(help+random>20000)
                                    {
                                        Console.WriteLine("you can not drive more than 20000 killometrs without checkup\n");
                                        check = true;
                                        break;
                                    }
                                    else if(help1 + random > 1200)
                                    {
                                        Console.WriteLine("you can not drive more than 1200 killometrs without refuel\n");
                                        check = true;
                                        break;
                                    }
                                    else
                                    {
                                        t.Current.AddKilometrage(random);
                                        check = true;
                                        Console.WriteLine("the drive is successful");
                                        break;
                                    }
                                }

                            }
                            if (check == false)
                            {
                                Console.WriteLine("this Bus license number is not exists\n ");
                            }
                            break;
                        }
                    case Opitions.busTreatment:
                        {
                            Console.WriteLine("plese enter a Bus license number");
                            string Lnumber = Console.ReadLine();
                            IEnumerator<Bus> t = busList.GetEnumerator();
                            bool check = false;
                            while ((t.MoveNext()))
                            {
                                if (t.Current.LicenseNumber == Lnumber)
                                {
                                    Console.WriteLine("plese enter 1 for refuel and 2 treat");
                                    int help;
                                    while (!Int32.TryParse(Console.ReadLine(), out help))
                                    {
                                        Console.WriteLine("only enter numbers\n" + "plese enter 1 for refuel and 2 treat");
                                    }
                                    if (help==1)
                                    {
                                        t.Current.KillFromRefueling = 0;
                                        check = true;
                                    }
                                    else if(help==2)
                                    {
                                        t.Current.KillFromLastCheckup = 0;
                                        t.Current.LastCheckup= DateTime.Now;
                                        check = true;

                                    }
                                    else
                                    {
                                        Console.WriteLine("you not enter 1 or 2 :( plese try again");
                                        check = true;
                                        break;
                                    }
                                }
                            }
                            if (check != true)
                            {
                                Console.WriteLine("this Bus license number does not exists");
                            }


                            break;
                        }
                    case Opitions.showKillFromLastCheckup:
                        {
                           foreach(Bus abus in busList)
                            {
                                Console.WriteLine(abus);
                            }
                                break;
                        }
                    case Opitions.exit:
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
