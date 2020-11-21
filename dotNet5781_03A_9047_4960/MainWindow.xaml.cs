using System;
using dotNet5781_02_9047_4960;
using System.Collections.Generic;
using dotNet5781_02_9047_4960;
using dotNet5781_01_9047_4960;
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
        public MainWindow()
        {
            dotNet5781_02_9047_4960.Program.BusLineCollection busLineCollection = new dotNet5781_02_9047_4960.Program.BusLineCollection();
            InitializeComponent();
        }
    }
}
