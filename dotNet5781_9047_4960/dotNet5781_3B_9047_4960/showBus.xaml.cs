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
        BackgroundWorker worker;
        public showBus(Bus b)
        {
            InitializeComponent();
            grid1.DataContext = b;
            refuelButton.DataContext = b;
        }

        private void refuelButton_Click(object sender, RoutedEventArgs e)
        {
            //Button refuel = (Button)sender;
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(refuelButton.DataContext);
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            (e.Argument as Bus).refuel();
           
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Thread.Sleep(12000);
            MessageBox.Show("Refuel completed", "Refuel message", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
