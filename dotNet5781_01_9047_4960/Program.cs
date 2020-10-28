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
            bus(string Lnumber,int yearSA, int mounthSA, int daySA, int yearC, int mounthC, int dayC,int allK, int kilometersC, int kilometersR)
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
        static void Main(string[] args)
        {
        }
    }
}
