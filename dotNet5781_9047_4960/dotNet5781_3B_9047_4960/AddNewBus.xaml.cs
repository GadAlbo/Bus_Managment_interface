using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;


namespace dotNet5781_3B_9047_4960
{
    /// <summary>
    /// Interaction logic for AddNewBus.xaml
    /// </summary>
    public partial class AddNewBus : Window
    {
        public AddNewBus(Bus b)
        {
            InitializeComponent();
            grid1.DataContext = b;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            Close();
        }
    }
}
