using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            BusLineCollection buses = new BusLineCollection();
            dotNet5781_01_9047_4960.Program.Bus bus;
            for (int i=0;i<10;i++)
            {
                 bus = new dotNet5781_01_9047_4960.Program.Bus();
                buses.buses.Add(bus);
            }
            buses.buses[0].LastCheckup = new DateTime(2018, 12, 6);
            buses.buses[3].KillFromRefueling = 1198;
            buses.buses[6].KillFromLastCheckup=19997;
            InitializeComponent();
        }
    }
}
