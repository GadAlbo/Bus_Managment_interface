using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNet5781_01_9047_4960;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;
using System.Windows.Threading;

namespace dotNet5781_3B_9047_4960
{
    
   public enum state { ReadyToGo, midRide, refueling, handling };
    public class Bus:ObservaleObject, IValueConverter
    {
        static Random w = new Random();
        private state s=state.ReadyToGo;
        public state State
        {
            get
            {
                return s;
            }
            set
            {
                s = value;
                OnPropertyChanged("State");
            }
        }
        private string licenseNumber="";
        public string LicenseNumber //property
        {
            get
            {
                return licenseNumber;
            }
            set
            {
                int i;
                if (Int32.TryParse(value, out i))
                {
                    if (StartOfActivity.Year < 2018)          //checks if the year is before 2018
                    {                  //the while is false now 
                            licenseNumber = value[0] + "" + value[1] + "-" + value[2] + value[3] + value[4] + "-" + value[5] + value[6]; //enter the right order o nums and -
                    }
                    else
                    {        //the while is false now 
                            licenseNumber = "" + value[0] + "" + value[1] + "" + value[2] + "-" + value[3] + value[4] + "-" + value[5] + value[6] + value[7]; //enter the right order o nums and -
                    }
                }
                OnPropertyChanged("LicenseNumber");
            }
        }
        private DateTime startOfActivity;
        public DateTime StartOfActivity//property
        {
            get
            {
                return startOfActivity;
            }
            set
            {
                startOfActivity = value;
                OnPropertyChanged("StartOfActivity");
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
                OnPropertyChanged("LastCheckup");
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
                {
                    allKilometrage = value;
                    OnPropertyChanged("AllKilometrage");
                }
            }
        }

        private int killFromLastCheckup = 0;
      /*  public DispatcherTimer DispatcherTimerRefual 
        { 
            get;
            set
            {

            }
        }
        public DispatcherTimer DispatcherTimerTreat { get; set; }*/
        public int KillFromLastCheckup//property
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
                    AllKilometrage += value;
                    OnPropertyChanged("KillFromLastCheckup");
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
                if (value >= 0)
                {
                    killFromRefueling = value;
                    AllKilometrage += value;
                    OnPropertyChanged("KillFromRefueling");
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
            LastCheckup = new DateTime(w.Next(1908, 2050), w.Next(1, 12), w.Next(1, 28));
            if(LastCheckup< startOfActivity)
            {
                LastCheckup = startOfActivity;
            }
        }
        public Bus()
        {
            //int y = w.Next(1908, 2050);
            //if (y < 2018)
            //    LicenseNumber = w.Next(1111111, 9999999).ToString();
            //else
            //{
            //    LicenseNumber = w.Next(11111111, 99999999).ToString();
            //}//lisens number
            startOfActivity = DateTime.Now;
            LastCheckup = startOfActivity;
        }
        public void AddKilometrage(string add) //adds Kilometrage to all the necessary variables
        {
            int addKill;
            Int32.TryParse(add, out addKill);
            if (addKill >= 0) //needs to be positive
            {
                killFromLastCheckup += addKill;
                KillFromRefueling += addKill;
            }
        }
        public override string ToString() //override the toString func
        {
            return "License Number:" + licenseNumber + " total Kilometrage from last checkup:" + killFromLastCheckup;
        }
        public void refuel()
        {
            KillFromRefueling = 0;
        }
        public void Treat()
        {
            LastCheckup = DateTime.Now;
            KillFromRefueling = 0;
        }
        public bool isBusTrue()
        {
            if (StartOfActivity.Year > 2018)
            {
                if (licenseNumber.Length == 10)
                    return true;
            }
            else if (licenseNumber.Length == 9)
                return true;
            return false;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(!(value is state))
            {
                return Binding.DoNothing;
            }
            if((KillFromRefueling>1150)||(LastCheckup.Subtract(DateTime.Now).Days>365)||(KillFromLastCheckup>15000))
                return new BitmapImage(new Uri("warning.png"));
            state s = (state)value;
            switch (s)
            {
                case state.ReadyToGo:
                    return new BitmapImage(new Uri("parking.png"));
                    break;
                case state.midRide:
                    return new BitmapImage(new Uri("steering-wheel.png"));
                case state.refueling:
                    return new BitmapImage(new Uri("refuel.png"));
                    break;
                case state.handling:
                    return new BitmapImage(new Uri("wrench.png"));
                    break;
                default:
                    return null;
                    break;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
