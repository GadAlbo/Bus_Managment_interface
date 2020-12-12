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
using System.Windows.Shapes;
using System.ComponentModel;
using System.Threading;

namespace dotNet5781_3B_9047_4960
{
    /// <summary>
    /// Interaction logic for showBus.xaml
    /// </summary>
    public partial class showBus : Window
    {
        BackgroundWorker workerTREAT;
        BackgroundWorker workerREFUAL;
        public showBus(Bus b)
        {
            InitializeComponent();
            grid1.DataContext = b;
            refuelButton.DataContext = b;
            workerREFUAL = new BackgroundWorker();
            workerREFUAL.DoWork += Worker_DoWork;
            workerREFUAL.ProgressChanged += Worker_ProgressChanged;
            workerREFUAL.RunWorkerCompleted += Worker_RunWorkerCompleted;
            workerREFUAL.WorkerReportsProgress = true;
            workerTREAT = new BackgroundWorker();
            workerTREAT.DoWork += Worker_DoWork1;
            workerTREAT.ProgressChanged += Worker_ProgressChanged2;
            workerTREAT.RunWorkerCompleted += Worker_RunWorkerCompleted3;
            workerTREAT.WorkerReportsProgress = true;
        }

        private void refuelButton_Click(object sender, RoutedEventArgs e)
        {
            //Button refuel = (Button)sender;
            workerREFUAL.RunWorkerAsync(grid1.DataContext);
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
            grid1.DataContext = (e.Result as Bus);
        }

        private void treatmentButton_Click(object sender, RoutedEventArgs e)
        {
            workerTREAT.RunWorkerAsync(grid1.DataContext);
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
            grid1.DataContext = (e.Result as Bus);
        }

    }
}
