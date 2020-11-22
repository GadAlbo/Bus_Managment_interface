using System;
using dotNet5781_02_9047_4960;
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

namespace dotNet5781_03A_9047_4960
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        dotNet5781_02_9047_4960.Program.BusLineCollection busLineCollection = new dotNet5781_02_9047_4960.Program.BusLineCollection();
        public MainWindow()
        {
            for (int i = 1; i < 11; i++)
            {
                dotNet5781_02_9047_4960.Program.BusLine busLine = new dotNet5781_02_9047_4960.Program.BusLine();
                busLineCollection.Add(busLine);
                for (int j = 0; j < 2; j++)
                {
                    dotNet5781_02_9047_4960.Program.BusLineStation bs = new dotNet5781_02_9047_4960.Program.BusLineStation();
                    busLineCollection[i].AddStition(bs);
                }
            }
            InitializeComponent();
            cbBusLines.ItemsSource = busLineCollection;
            cbBusLines.DisplayMemberPath = " BusLineNum ";
            cbBusLines.SelectedIndex = 0;
            ShowBusLine();
        }
           private dotNet5781_02_9047_4960.Program.BusLine currentDisplayBusLine;
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = busLineCollection[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.stations;
        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as dotNet5781_02_9047_4960.Program.BusLine).BusLineNumber);
        }
    }
}

