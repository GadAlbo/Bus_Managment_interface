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
    public partial class MainWindow : Window
    {
        ObservableCollection <Bus> ObservableCollectionBus = new ObservableCollection<Bus>();
        BackgroundWorker workerREFUAL;
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
            workerREFUAL = new BackgroundWorker();
            workerREFUAL.DoWork += Worker_DoWorkRefuel;
            workerREFUAL.RunWorkerCompleted += Worker_RunWorkerCompletedRefuel;
            workerREFUAL.WorkerReportsProgress = true;
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
        private void refuelingButton_Click(object sender, RoutedEventArgs e)
        {
            if (((sender as Button).DataContext as Bus).State == state.ReadyToGo)
            {
                workerREFUAL.RunWorkerAsync((sender as Button).DataContext);
            }
            else
            {
                MessageBox.Show("refuel Can not be handled because the bus is occupied, try later", "Refuel message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Worker_DoWorkRefuel(object sender, DoWorkEventArgs e)
        {
            (e.Argument as Bus).refuel();
            (e.Argument as Bus).State = state.refueling;
            e.Result = e.Argument;
            Thread.Sleep(12000);
        }
        private void Worker_RunWorkerCompletedRefuel(object sender, RunWorkerCompletedEventArgs e)
        {
            (e.Result as Bus).State = state.ReadyToGo;
            MessageBox.Show("Refuel of "+ (e.Result as Bus).LicenseNumber+ " bus is completed", "Refuel message", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
