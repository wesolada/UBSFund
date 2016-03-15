namespace FundTests.Enums
{
    using UBSFund.Enums;
    using MVVMClasses;
    using NUnit.Framework;
    using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

    [TestFixture]
    public class StockTypeTests
    {
        [Test]
        [TestCase(StockType.Equity, ExpectedResult = "Equity")]
        [TestCase(StockType.Bond, ExpectedResult = "Bond")]
        public string StockType_Description_Test(StockType stockType)
        {
            return stockType.GetAttributeOfType<DescriptionAttribute>().Description;
        }

        [Test]
        [TestCase(StockType.Equity, ExpectedResult = 1)]
        [TestCase(StockType.Bond, ExpectedResult = 2)]
        public int StockType_Value_Test(StockType stockType)
        {
            return (int)stockType;
        }
    }
}