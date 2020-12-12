﻿using System;
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
            workerREFUAL.DoWork += Worker_DoWorkRefuel;
            workerREFUAL.RunWorkerCompleted += Worker_RunWorkerCompletedRefuel;
            workerREFUAL.WorkerReportsProgress = true;
            workerTREAT = new BackgroundWorker();
            workerTREAT.DoWork += Worker_DoWorktreatment;
            workerTREAT.RunWorkerCompleted += Worker_RunWorkerCompletedtreatment;
            workerTREAT.WorkerReportsProgress = true;
        }

        private void refuelButton_Click(object sender, RoutedEventArgs e)
        {

            if ((grid1.DataContext as Bus).State == state.ReadyToGo)
            {
                workerREFUAL.RunWorkerAsync(grid1.DataContext);
            }
            else
            {
                MessageBox.Show("refuel Can not be handled because the bus is occupied, try later", "Refuel message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Worker_DoWorkRefuel(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(12000);
            (e.Argument as Bus).refuel();
            (e.Argument as Bus).State = state.refueling;
           // Thread.Sleep(12000);
        }
        private void Worker_RunWorkerCompletedRefuel(object sender, RunWorkerCompletedEventArgs e)
        {
            (grid1.DataContext as Bus).State = state.ReadyToGo;
            MessageBox.Show("Refuel completed", "Refuel message", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void treatmentButton_Click(object sender, RoutedEventArgs e)
        {
            if((grid1.DataContext as Bus).State==state.ReadyToGo)
            {
                workerTREAT.RunWorkerAsync(grid1.DataContext);
            }
            else
            {
                MessageBox.Show("Treatment Can not be handled because the bus is occupied, try later", "Treatment message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
            
        private void Worker_DoWorktreatment(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(6000);
            (e.Argument as Bus).refuel();
            (e.Argument as Bus).Treat();
            (e.Argument as Bus).State = state.handling;
            //Thread.Sleep(6000);
        }
        private void Worker_RunWorkerCompletedtreatment(object sender, RunWorkerCompletedEventArgs e)
        {
            (grid1.DataContext as Bus).State = state.ReadyToGo;
            MessageBox.Show("Treatment completed", "Treatment message", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
