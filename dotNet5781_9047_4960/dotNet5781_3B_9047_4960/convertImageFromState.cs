using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace dotNet5781_3B_9047_4960
{
    public class convertImageFromState : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is state))
            {
                return Binding.DoNothing;
            }
            string path = null;
            //if ((KillFromRefueling > 1150) || (LastCheckup.Subtract(DateTime.Now).Days > 365) || (KillFromLastCheckup > 15000))
            //    return new BitmapImage(new Uri("image/warning.png", UriKind.Relative));
            state s = (state)value;
            switch (s)
            {
                case state.ReadyToGo:
                    path = "image/parking.png";
                    break;
                case state.midRide:
                    path = "image/steering-wheel.png";
                    break;
                case state.refueling:
                    path = "image/refuel.png";
                    break;
                case state.handling:
                    path = "image/wrench.png";
                    break;
                default:
                    return null;
                    break;
            }
            return path;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
