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
using System.Threading;

namespace dotNet5781_3B_9047_4960
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window//window that shows all of the buses with refual and drive buttons, dablle tap to show bus and on optiong to add buses
    {
        ObservableCollection <Bus> ObservableCollectionBus = new ObservableCollection<Bus>();
        public MainWindow()// constractor- add the list of buses
        {
            InitializeComponent();
            Bus bus;
            for (int i = 0; i < 10; i++)
            {
                bus = new Bus(0);
                ObservableCollectionBus.Add(bus);
            }
            ObservableCollectionBus[0].LastCheckup = new DateTime(2018, 12, 6);
            ObservableCollectionBus[3].KillFromRefueling = 1198;//close to the next refual
            ObservableCollectionBus[4].KillFromRefueling = 1198;//close to the next refual
            ObservableCollectionBus[5].KillFromRefueling = 50;
            ObservableCollectionBus[6].KillFromRefueling = 400;
            ObservableCollectionBus[6].KillFromLastCheckup = 19997;//close to next checkup
            busesBox.ItemsSource = ObservableCollectionBus;
        }
        private void addBusButon_Click(object sender, RoutedEventArgs e)// add a new bus- open the addBus window and set the new data to the main window
        {
            Bus b = new Bus();
            AddNewBus add = new AddNewBus(b);
            add.ShowDialog();
            if(b.isBusTrue())
            {
                ObservableCollectionBus.Add(b);
            }
        }

        private void MouseDoubleClickBus(object sender, MouseButtonEventArgs e)//show bus- open the showBus window
        {
            Bus b= (busesBox.SelectedItem as Bus);
            if (b != null)
            {
                showBus show = new showBus(b);
                show.Show();
            }
        }
        private void refuelingButton_Click(object sender, RoutedEventArgs e)//refual the bus if the bus is ready to go
        {
            if (((sender as Button).DataContext as Bus).State == state.ReadyToGo)
            {
                ((sender as Button).DataContext as Bus).worker = new BackgroundWorker();
                ((sender as Button).DataContext as Bus).worker.DoWork += Worker_DoWorkRefuel;
                ((sender as Button).DataContext as Bus).worker.RunWorkerCompleted += Worker_RunWorkerCompletedRefuel;
                ((sender as Button).DataContext as Bus).worker.WorkerReportsProgress = true;
                ((sender as Button).DataContext as Bus).worker.RunWorkerAsync((sender as Button).DataContext);
            }
            else
            {
                MessageBox.Show("refuel Can not be handled because the bus is occupied, try later", "Refuel message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Worker_DoWorkRefuel(object sender, DoWorkEventArgs e)//do worker for the refual- wait the time
        {
            (e.Argument as Bus).State = state.refueling;
            e.Result = e.Argument;
            Thread.Sleep(12000);
        }
        private void Worker_RunWorkerCompletedRefuel(object sender, RunWorkerCompletedEventArgs e)//refual the bus
        {
            (e.Result as Bus).State = state.ReadyToGo;
            (e.Result as Bus).refuel();
            MessageBox.Show("Refuel of "+ (e.Result as Bus).LicenseNumber+ " bus is completed", "Refuel message", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void driveButton_Click(object sender, RoutedEventArgs e)//drive bus- open the driveBus window
        {
            Bus b = (sender as Button).DataContext as Bus;
            if (b != null)
            {
                driveBus drive = new driveBus(b);
                drive.Show();
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)//search a bus
        {
            foreach (var item in ObservableCollectionBus)
            {
                ListBoxItem bus = (ListBoxItem)busesBox.ItemContainerGenerator.ContainerFromItem(item);
                string searchS = SearchBox.Text;
                int num = searchS.Length;
                if ((num <= item.LicenseNumber.Length) && (searchS == (item as Bus).LicenseNumber.Substring(0, num)))
                {
                    bus.Visibility = Visibility.Visible;
                }
                else
                    bus.Visibility = Visibility.Collapsed;
            }
        }
    }
}
