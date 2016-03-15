namespace UBSFundTests.Converters
{
    using UBSFund.Converters;
    using UBSFund.Enums;
    using NUnit.Framework;

    [TestFixture]
    public class StockNameConverterTests
    {
        [Test]
        [TestCase(StockType.Equity, ExpectedResult = "Equity")]
        [TestCase(StockType.Bond, ExpectedResult = "Bond")]
        public string Convert_Test(int value)
        {
            var converter = new StockNameConverter();

            return (string)converter.Convert(value, null, null, null);
        }
    }
}