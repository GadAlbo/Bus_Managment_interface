using System;
using System.Collections.Generic;
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
            private DateTime LastCheckup;
            public DateTime lastCheckup
            {
                get
                {
                    return lastCheckup;
                }
                private set
                {
                    lastCheckup = value;
                }
            }
            private int allKilometrage = 0;
            public int AllKilometrage { get=>allKilometrage; private set=>allKilometrage=value; }
            
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
            private int killFromRefueling;
            public int KillFromRefueling
            {
                get
                {
                    return killFromRefueling;
                }
                private set
                {
                    killFromRefueling = value;
                }
            }
            bus(string Lnumber,int yearSA, int mounthSA, int daySA, int yearC, int mounthC, int dayC, int allK=0, int kilometersC=0, int kilometersR=0)
            {
                LicenseNumber = Lnumber;
                try
                {
                    startOfActivity = new DateTime(yearSA, mounthSA, daySA);
                }
                catch (ArgumentOutOfRangeException outOfRange)
                {
                    Console.WriteLine("Error: {0} in the start activity date", outOfRange.Message);
                }
                try
                {
                    LastCheckup = new DateTime(yearC, mounthC, dayC);
                }
                catch (ArgumentOutOfRangeException outOfRange)
                {
                    Console.WriteLine("Error: {0} in the last checkup date", outOfRange.Message);
                }
                AllKilometrage = allK;
                KillFromLastCheckup = kilometersC;
                KillFromRefueling = kilometersR;
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
        enum Opitions{ addBus, chooseBus, busTreatment, showKillFromLastCheckup, exit };
        static void Main(string[] args)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            List<bus> busList = new List<bus>();
            Opitions op;
            op =(Opitions)0;
            while (op != (Opitions)4) 
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
                        break;
                    case Opitions.showKillFromLastCheckup:
                        break;
                    case Opitions.exit:
                        break;
                    default:
                        break;
                }
            } while (op != (Opitions)4);
        }
    }
}
