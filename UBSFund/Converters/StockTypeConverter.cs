namespace UBSFund.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using Enums;

    public class StockTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)(StockType)value).Equals(System.Convert.ToInt32(parameter));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToBoolean(value) ? parameter : null;
        }
    }
}