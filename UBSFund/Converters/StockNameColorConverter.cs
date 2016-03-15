namespace UBSFund.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;
    using Enums;

    public class StockNameColorConverter : IMultiValueConverter
    {
        private readonly int bondTolerance = 100000;
        private readonly int equityTolerance = 200000;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = new SolidColorBrush(Colors.Black);
            decimal marketValue = System.Convert.ToDecimal(values[0]);
            decimal transactionCost = System.Convert.ToDecimal(values[1]);

            switch ((StockType)values[2])
            {
                case StockType.Equity:
                    if (marketValue < 0 || transactionCost > this.equityTolerance)
                    {
                        return new SolidColorBrush(Colors.Red);
                    }
                    break;

                case StockType.Bond:
                    if (marketValue < 0 || transactionCost > this.bondTolerance)
                    {
                        return new SolidColorBrush(Colors.Red);
                    }
                    break;
            }

            return brush;
        }

        [Obsolete]
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}