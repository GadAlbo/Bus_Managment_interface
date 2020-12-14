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
using System.Threading;
using System.ComponentModel;

namespace dotNet5781_3B_9047_4960
{
    /// <summary>
    /// Interaction logic for driveBus.xaml
    /// </summary>
    public partial class driveBus : Window//create a window for drive a bus from the list
    {
        BackgroundWorker workerDRIVE;
        static Random w = new Random();
        string driveNumber;
        public driveBus(Bus b)//constractor
        {
            InitializeComponent();
            busesgrid.DataContext = b;
            drive.DataContext = b;
            workerDRIVE = new BackgroundWorker();
            workerDRIVE.DoWork += Worker_DoWorkDrive;
            workerDRIVE.RunWorkerCompleted += Worker_RunWorkerCompletedDrive;
            workerDRIVE.WorkerReportsProgress = true;
        }
        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)//allow only numbers
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            //allow get out of the text box
            if  ((e.Key == Key.Enter)|| e.Key == Key.Return || e.Key == Key.Tab)
                return;

            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
             || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox

            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
            return;
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)//send to drive when the user enter- Enter
        {
            if(e.Key==Key.Enter)
            {
                if(drive.Text!="")
                {
                    driveNumber = drive.Text;
                    workerDRIVE.RunWorkerAsync(busesgrid.DataContext);
                }
                else
                {
                    MessageBox.Show("You do not enret a number", "Drive message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void Worker_DoWorkDrive(object sender, DoWorkEventArgs e)//do worker- send to drive only when the bus is ready to go and it refual and checkuo close enught
        {
            if((e.Argument as Bus).State == state.ReadyToGo)
             {
                if ((e.Argument as Bus).KillFromLastCheckup + Convert.ToInt32(driveNumber) < 20000)
                {
                    if ((e.Argument as Bus).KillFromRefueling + Convert.ToInt32(driveNumber) < 1200)
                    {
                        (e.Argument as Bus).State = state.midRide;
                        Thread.Sleep(w.Next(20, 50) * Convert.ToInt32(driveNumber) * 2);
                        e.Result = e.Argument;
                    }
                    else
                    {
                        MessageBox.Show("Drive Can not be handled because the bus needs refuel", "Drive message", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Drive Can not be handled because the the bus needs treatment", "Drive message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Drive Can not be handled because the bus is occupied, try later", "Drive message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Worker_RunWorkerCompletedDrive(object sender, RunWorkerCompletedEventArgs e)//drive the bus
        {
            (busesgrid.DataContext as Bus).State = state.ReadyToGo;
            if(e.Result is Bus)
            {
                (e.Result as Bus).AddKilometrage(driveNumber);
            }
            if ((busesgrid.DataContext as Bus).KillFromLastCheckup != 0)
            {
                MessageBox.Show("Drive completed", "Drive message", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
