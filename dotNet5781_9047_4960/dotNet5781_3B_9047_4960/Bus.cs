using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet5781_01_9047_4960;

namespace dotNet5781_3B_9047_4960
{
    
   public enum state { ReadyToGo, midRide, refueling, handling };
    public class Bus
    {
        static Random w = new Random();
        private state s;
        public state State
        {
            get
            {
                return s;
            }
            set
            {
                s = value;
            }
        }
        public int Num //property
        {
            get
            {
                return ((1200 - KillFromRefueling) / 1200) * 100;
            }
            set { }
        }
        private string licenseNumber;
        public string LicenseNumber //property
        {
            get
            {
                return licenseNumber;
            }
            set
            {
                bool flag = true;
                while (flag)                                //while the license Number is not corect
                {
                    if (startOfActivity.Year < 2018)          //checks if the year is before 2018
                    {
                            flag = false;                   //the while is false now 
                            licenseNumber = value[0] + "" + value[1] + "-" + value[2] + value[3] + value[4] + "-" + value[5] + value[6]; //enter the right order o nums and -
                    }
                    else
                    {
                            flag = false; //the while is false now 
                            licenseNumber = "" + value[0] + "" + value[1] + "" + value[2] + "-" + value[3] + value[4] + "-" + value[5] + value[6] + value[7]; //enter the right order o nums and -
                    }
                    //licenseNumber = value;
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
            }
        }

        public Bus(int x)
        {
            int y = w.Next(1908, 2050);
            startOfActivity = new DateTime(y, w.Next(1, 12), w.Next(1, 28)); // trys to cunstract a datetime, if it doesnt work sends an exsseption
            if (y < 2018)
                LicenseNumber = w.Next(1111111, 9999999).ToString();
            else
            {
                LicenseNumber = w.Next(11111111, 99999999).ToString();
            }//lisens number
            LastCheckup = new DateTime(w.Next(1, 2050), w.Next(1, 12), w.Next(1, 28));
        }
        public Bus()
        {

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
}
