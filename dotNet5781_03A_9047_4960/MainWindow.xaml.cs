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
        private dotNet5781_02_9047_4960.Program.BusLineCollection busLineCollection = new dotNet5781_02_9047_4960.Program.BusLineCollection();
        private dotNet5781_02_9047_4960.Program.BusLine currentDisplayBusLine;
        public MainWindow()
        {
            dotNet5781_02_9047_4960.Program.BusLineStation bs;
            dotNet5781_02_9047_4960.Program.BusLine busLine;
            for (int i = 1; i < 11; i++)
            {
                busLine = new dotNet5781_02_9047_4960.Program.BusLine();
                busLineCollection.Add(busLine);
                for (int j = 0; j < 2; j++)
                {
                     bs = new dotNet5781_02_9047_4960.Program.BusLineStation();
                    busLineCollection[i].AddStition(bs);
                }
            }
            InitializeComponent();
            cbBusLines.ItemsSource = busLineCollection;
            cbBusLines.DisplayMemberPath = "BusLineNumber";
            cbBusLines.SelectedIndex = 0;
            ShowBusLine(cbBusLines.SelectedIndex+1);
        }
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = busLineCollection[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.stations;
            tbArea.Text = busLineCollection[index].area.ToString();
        }

        private void cbBusLines_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedItem as dotNet5781_02_9047_4960.Program.BusLine).BusLineNumber);
        }
    }
}

