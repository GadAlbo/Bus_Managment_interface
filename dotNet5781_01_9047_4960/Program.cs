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
        Random r = new Random();
        public class Bus
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
                        if (startOfActivity.Year<2018)          //checks if the year is before 2018
                        {
                            if (value.Length == 7)              //checks if the Length is right
                            {
                                flag = false;                   //the while is false now 
                                licenseNumber = value[0] + ""+ value [1]+"-"+value[2]+value[3] + value[4] + "-"+value[5] + value[6]; //enter the right order o nums and -
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
                                licenseNumber = value[0]+"" + value[1]+"" + value[2] + "-" + value[3] + value[4] + "-" + value[5] + value[6] + value[7]; //enter the right order o nums and -
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
                    if(value> allKilometrage) // can not subtract
                        allKilometrage = value;
                    else
                        Console.WriteLine("kilometrage sould be positive and we can not subtract kilometrage");
                }
            }
            
            private int killFromLastCheckup=0;
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
            private int killFromRefueling=0;
            public int KillFromRefueling//property
            {
                get
                {
                    return killFromRefueling;
                }
                 set
                {
                    if(value>=0)//shoud be positive
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
                while(flag)//true until we get a corect date 
                {
                    try
                    {
                       /* Console.WriteLine("Please Enter The Starting Date Of Activity");
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
                        }*/
                   
                        startOfActivity = new DateTime((int)r.Next(1917,2021), mounthSA, daySA); // trys to cunstract a datetime, if it doesnt work sends an exsseption
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
                /*Console.WriteLine("If You Would Like To Tell Us More About The Vehicle Please Enter 1. else press any other number to continue"); 
                int decison;
                while(!Int32.TryParse(Console.ReadLine(),out decison))
                {
                    Console.WriteLine("only enter numbers");
                }
                if(decison==1) //if the user wants to tell us more
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
                            case 0: //adds last checkup date
                                while (flag) //true until we get a corect date 
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

                    } while (op!=4); //exit*/
                }
            }
            public void AddKilometrage(int addKill) //adds Kilometrage to all the necessary variables
            {
                    if(addKill > 0) //needs to be positive
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
        enum Opitions { addBus, chooseBus, busTreatment, showKillFromLastCheckup, exit };   //enum definition
        static void Main(string[] args)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            List<Bus> busList = new List<Bus>();                //bus list
            Opitions op;                                       
            do
            {
                Console.WriteLine(
                    "plese enter 0 to add a new bus\n" +
                    "plese enter 1 to choose bus for a ride\n" +
                    "plese enter 2 to refuel or checkup bus\n" +
                    "plese enter 3 to view the number of killometers each bus has traveled since the last treatment\n" +
                    "plese enter 4 to exit");
                int optionC;
                while (!Int32.TryParse(Console.ReadLine(), out optionC))        //trying to get the users chosen option
                {
                    Console.WriteLine("only enter numbers");
                }
                op = (Opitions)optionC;
                switch (op)
                {
                    case Opitions.addBus: //add a new bus to the list
                        {
                            Bus b=new Bus();
                            busList.Add(b);
                        }
                        ;
                        break;
                    case Opitions.chooseBus:    //choose bus for a ride
                        {
                            Console.WriteLine("plese enter a Bus license number with -");
                            string Lnumber = Console.ReadLine();                    //license number
                            int random= r.Next(0,1200);                             //the killometrs for the ride
                            IEnumerator<Bus> t = busList.GetEnumerator();           //creating a iterator for the list, pointing to the start
                            bool check = false;                                     //flag
                            while((t.MoveNext()))                                   //checks if ther is wher to move
                            {
                                if(t.Current.LicenseNumber==Lnumber)                //if it is the current nodes License Number
                                {
                                    int kFromLastCheckup = t.Current.KillFromLastCheckup;
                                    int kFromRefueling = t.Current.KillFromRefueling;
                                    DateTime lastCheckup = t.Current.LastCheckup;
                                    TimeSpan subtraction = DateTime.Now.Subtract(lastCheckup);
                                    if (kFromLastCheckup + random>20000)                        //if the sum of killometrs (killometer from last checkup and the killometrs for the ride) are above 20000
                                    {
                                        Console.WriteLine("you can not drive more than 20000 killometrs without checkup");
                                        check = true;
                                        break;
                                    }
                                    else if(kFromRefueling + random > 1200)                 //if the sum of killometrs (killometer from refuel and the killometrs for the ride) are above 1200      
                                    {
                                        Console.WriteLine("you can not drive more than 1200 killometrs without refuel");
                                        check = true;
                                        break;
                                    }  
                                    else if (subtraction.TotalDays >365)                    //if the last cheackup was more then 1 year ago
                                    {
                                        check = true;
                                        Console.WriteLine("please renew your license, the bus can not be on the roud without a checkup");
                                    }           
                                    else//if its non of the above, then its soutable to drive
                                    {
                                        t.Current.AddKilometrage(random);
                                        check = true;
                                        Console.WriteLine("the drive is successful");
                                        break;
                                    }
                                }

                            }
                            if (check == false)             //if the license number is not found
                            {
                                Console.WriteLine("this Bus license number is not exists ");
                            }
                            break;
                        }
                    case Opitions.busTreatment:             //refuel or checkup bus
                        {
                            Console.WriteLine("plese enter a Bus license number");
                            string Lnumber = Console.ReadLine();                    //licens num
                            IEnumerator<Bus> t = busList.GetEnumerator();           //creating a iterator for the list, pointing to the start
                            bool check = false;                                     //flag
                            while ((t.MoveNext()))                                  //checks if there's a next
                            {
                                if (t.Current.LicenseNumber == Lnumber)             //if it is the current nodes License Number
                                {
                                    Console.WriteLine("plese enter 1 for refuel and 2 for checkup");
                                    int treat;
                                    while (!Int32.TryParse(Console.ReadLine(), out treat))      //the optiom that is choosen
                                    {
                                        Console.WriteLine("only enter numbers\n" + "plese enter 1 for refuel and 2 for checkup");
                                    }
                                    if (treat == 1)                                             //if we want to refuel
                                    {
                                        t.Current.KillFromRefueling = 0;
                                        check = true;
                                        Console.WriteLine("the refuel was sucssesul");
                                    }
                                    else if(treat == 2)                                         //if we want to checkup
                                    {
                                        t.Current.KillFromLastCheckup = 0;
                                        t.Current.LastCheckup= DateTime.Now;
                                        check = true;
                                        Console.WriteLine("the checkup was sucssesul");
                                    }
                                    else
                                    {
                                        Console.WriteLine("you not enter 1 or 2 :( plese try again");
                                        check = true;
                                        break;
                                    }
                                }
                            }
                            if (check != true)                  //if we didint found the license number
                            {
                                Console.WriteLine("this Bus license number does not exists");
                            }
                            break;
                        }
                    case Opitions.showKillFromLastCheckup:          //to view the number of killometers each bus has traveled since the last treatment
                        {
                           foreach(Bus abus in busList)
                            {
                                Console.WriteLine(abus);            //using toString
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
            } while (op != (Opitions)4);    //if we want to exit
        }
    }
}

    public Bus()
    {
        Random w = new Random();
        int y = w.Next(1, 2050);
        startOfActivity = new DateTime(y, w.Next(1, 12), w.Next(1, 28)); // trys to cunstract a datetime, if it doesnt work sends an exsseption
        if (y < 2018)
            LicenseNumber = w.Next(1111111, 9999999).ToString();
        else
        {
            licenseNumber = w.Next(11111111, 99999999).ToString();
        }//lisens number
        LastCheckup = new DateTime(w.Next(1, 2050), w.Next(1, 12), w.Next(1, 28));
    }
  