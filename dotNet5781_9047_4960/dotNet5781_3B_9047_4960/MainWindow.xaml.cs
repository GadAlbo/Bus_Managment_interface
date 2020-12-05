using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNet5781_01_9047_4960;
namespace dotNet5781_3B_9047_4960
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection <dotNet5781_01_9047_4960.Program.Bus> buses = new ObservableCollection<Program.Bus>();
        public MainWindow()
        {
            //BusLineCollection Buses = new BusLineCollection();
            //dotNet5781_01_9047_4960.Program.Bus bus;
            //for (int i = 0; i < 10; i++)
            //{
            //    bus = new dotNet5781_01_9047_4960.Program.Bus();
            //    Buses.buses.Add(bus);
            //}
            //Buses.buses[0].LastCheckup = new DateTime(2018, 12, 6);
            //Buses.buses[3].KillFromRefueling = 1198;
            //Buses.buses[6].KillFromLastCheckup = 19997;
            //busesBox.ItemsSource = Buses;
            //InitializeComponent();
            InitializeComponent();
            dotNet5781_01_9047_4960.Program.Bus bus;
            for (int i = 0; i < 10; i++)
            {
                bus = new dotNet5781_01_9047_4960.Program.Bus();
                buses.Add(bus);
            }
            buses[0].LastCheckup = new DateTime(2018, 12, 6);
            buses[3].KillFromRefueling = 1198;
            buses[4].KillFromRefueling = 1198;
            buses[5].KillFromRefueling = 50;
            buses[6].KillFromRefueling = 400;
            buses[6].KillFromLastCheckup = 19997;
            busesBox.ItemsSource = buses;
        }
    }
}
