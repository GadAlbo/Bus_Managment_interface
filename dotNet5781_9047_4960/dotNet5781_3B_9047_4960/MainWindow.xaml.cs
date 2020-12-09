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
        BackgroundWorker workerREFUAL;
        BackgroundWorker workerTREAT;
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
        private void refuelingButton_Click(object sender, RoutedEventArgs e)
        {
            //Button refuel = (Button)sender;
            workerREFUAL = new BackgroundWorker();
            workerREFUAL.DoWork += Worker_DoWork;
            workerREFUAL.ProgressChanged += Worker_ProgressChanged;
            workerREFUAL.RunWorkerCompleted += Worker_RunWorkerCompleted;
            workerREFUAL.WorkerReportsProgress = true;
            workerREFUAL.RunWorkerAsync(busesgrid.DataContext);
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            (e.Argument as Bus).refuel();
            e.Result = (e.Argument as Bus);
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Thread.Sleep(12000);
            MessageBox.Show("Refuel completed", "Refuel message", MessageBoxButton.OK, MessageBoxImage.Information);
            busesgrid.DataContext = (e.Result as Bus);
        }
        private void treatmentButton_Click(object sender, RoutedEventArgs e)
        {
            workerTREAT = new BackgroundWorker();
            workerTREAT.DoWork += Worker_DoWork1;
            workerTREAT.ProgressChanged += Worker_ProgressChanged2;
            workerTREAT.RunWorkerCompleted += Worker_RunWorkerCompleted3;
            workerTREAT.WorkerReportsProgress = true;
            workerTREAT.RunWorkerAsync(busesgrid.DataContext);
        }

        private void Worker_DoWork1(object sender, DoWorkEventArgs e)
        {
            (e.Argument as Bus).Treat();
            (e.Argument as Bus).refuel();
            e.Result = (e.Argument as Bus);
        }
        private void Worker_ProgressChanged2(object sender, ProgressChangedEventArgs e)
        {
        }
        private void Worker_RunWorkerCompleted3(object sender, RunWorkerCompletedEventArgs e)
        {
            Thread.Sleep(12000);
            MessageBox.Show("Treatment completed", "Treatment message", MessageBoxButton.OK, MessageBoxImage.Information);
            busesgrid.DataContext = (e.Result as Bus);
        }

    }
}
