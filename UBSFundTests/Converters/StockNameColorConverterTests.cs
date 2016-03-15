namespace UBSFundTests.Converters
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Windows.Media;
    using UBSFund.Converters;
    using NUnit.Framework;

    [TestFixture]
    public class StockNameColorConverterTests
    {
        [Test, TestCaseSource(typeof (TestDataFactory), nameof(TestDataFactory.ConvertTestCases))]
        public Color ConvertTest(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var converter = new StockNameColorConverter();

            return ((SolidColorBrush)converter.Convert(values, null, null, null)).Color;
        }
    }

    public class TestDataFactory
    {
        public static IEnumerable ConvertTestCases
        {
            get
            {
                yield return new TestCaseData(new object[] {10, 10, 1}, null, null, null).Returns(Colors.Black);
                yield return new TestCaseData(new object[] {-20, 10, 1}, null, null, null).Returns(Colors.Red);
                yield return new TestCaseData(new object[] {10, 255255, 1}, null, null, null).Returns(Colors.Red);

                yield return new TestCaseData(new object[] {10, 10, 2}, null, null, null).Returns(Colors.Black);
                yield return new TestCaseData(new object[] {-20, 10, 2}, null, null, null).Returns(Colors.Red);
                yield return new TestCaseData(new object[] {10, 128128, 2}, null, null, null).Returns(Colors.Red);

                yield return new TestCaseData(new object[] {0, 0, 0}, null, null, null).Returns(Colors.Black);
            }
        }
    }
}