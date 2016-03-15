namespace UBSFund.Converters
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using Enums;
    using MVVMClasses;

    public class StockNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((StockType)value).GetAttributeOfType<DescriptionAttribute>().Description;
        }

        [Obsolete]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}