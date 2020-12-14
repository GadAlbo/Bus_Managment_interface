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
            state s = (state)value;
            switch (s)
            {
                case state.ReadyToGo:
                    return "image/parking.png";
                    break;
                case state.midRide:
                    return "image/steering-wheel.png";
                    break;
                case state.refueling:
                    return "image/refuel.png";
                    break;
                case state.handling:
                    return "image/wrench.png";
                    break;
                default:
                    return null;
                    break;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
