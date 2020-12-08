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
using System.ComponentModel;

namespace dotNet5781_3B_9047_4960
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection <Bus> ObservableCollectionBus = new ObservableCollection<Bus>();
        public MainWindow()
        {
            InitializeComponent();
            Bus bus;
            for (int i = 0; i < 10; i++)
            {
                bus = new Bus(0);
                ObservableCollectionBus.Add(bus);
            }
            ObservableCollectionBus[0].LastCheckup = new DateTime(2018, 12, 6);
            ObservableCollectionBus[3].KillFromRefueling = 1198;
            ObservableCollectionBus[4].KillFromRefueling = 1198;
            ObservableCollectionBus[5].KillFromRefueling = 50;
            ObservableCollectionBus[6].KillFromRefueling = 400;
            ObservableCollectionBus[6].KillFromLastCheckup = 19997;
            busesBox.ItemsSource = ObservableCollectionBus;
        }

        private void addBusButon_Click(object sender, RoutedEventArgs e)
        {
            Bus b = new Bus();
            AddNewBus add = new AddNewBus(b);
            add.ShowDialog();
            ObservableCollectionBus.Add(b);
        }

        private void MouseDoubleClickBus(object sender, MouseButtonEventArgs e)
        {
            Bus b= (busesBox.SelectedItem as Bus);
            if (b != null)
            {
                showBus show = new showBus(b);
                show.Show();
            }
        }
    }
}
