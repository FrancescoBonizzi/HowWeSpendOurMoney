using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace HowWeSpendOurMoneyGui.Converters
{
    public class TagsNumberIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var baseDirectory = $"{Assembly.GetExecutingAssembly().GetName().Name}/;component";
            return (int)value == 0
                ? (BitmapImage)Application.Current.Resources["NoTick"]
                : (BitmapImage)Application.Current.Resources["OkTick"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
