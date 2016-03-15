namespace UBSFundTests.Converters
{
    using System.Collections;
    using UBSFund.Converters;
    using UBSFund.Enums;
    using NUnit.Framework;

    [TestFixture]
    public class StockTypeConverterTests
    {
        [Test, TestCaseSource(typeof (TestCaseDataFactory), nameof(TestCaseDataFactory.ConvertTestCases))]
        public bool Convert_Test(StockType stockType, int parameter)
        {
            var converter = new StockTypeConverter();

            return (bool)converter.Convert(stockType, null, parameter, null);
        }

        [Test, TestCaseSource(typeof (TestCaseDataFactory), nameof(TestCaseDataFactory.ConvertBackTestCases))]
        public StockType? ConvertBack_Test(bool value, StockType parameter)
        {
            var converter = new StockTypeConverter();

            return (StockType?)converter.ConvertBack(value, null, parameter, null);
        }
    }

    public class TestCaseDataFactory
    {
        public static IEnumerable ConvertTestCases
        {
            get
            {
                yield return new TestCaseData(StockType.Equity, 1).Returns(true);
                yield return new TestCaseData(StockType.Equity, 2).Returns(false);
                yield return new TestCaseData(StockType.Bond, 1).Returns(false);
                yield return new TestCaseData(StockType.Bond, 2).Returns(true);
            }
        }

        public static IEnumerable ConvertBackTestCases
        {
            get
            {
                yield return new TestCaseData(true, StockType.Equity).Returns(StockType.Equity);
                yield return new TestCaseData(false, StockType.Equity).Returns(null);
                yield return new TestCaseData(true, StockType.Bond).Returns(StockType.Bond);
                yield return new TestCaseData(false, StockType.Bond).Returns(null);
            }
        }
    }
}