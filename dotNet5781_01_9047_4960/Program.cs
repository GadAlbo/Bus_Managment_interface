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
                            Console.WriteLine("license number is to long");
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
                            Console.WriteLine("license number is to long");
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
            public int AllKilometrage
            {
                get
                {
                    return allKilometrage;
                }
                private set
                {
                    if(value>0)
                    {
                        allKilometrage += value;
                    }
                    else
                    {
                        Console.WriteLine("can not reduce the amount of kilometrage");
                    }
                }
            }
            private int killFromLastCheckup;
            public int KillFromLastCheckup
            {
                get
                {
                    return killFromLastCheckup;
                }
                private set
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

        }
        static void Main(string[] args)
        {
        }
    }
}
