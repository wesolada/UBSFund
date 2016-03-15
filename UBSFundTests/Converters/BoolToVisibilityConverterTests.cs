namespace FundTests.Converters
{
    using System.Collections;
    using System.Windows;
    using NUnit.Framework;
    using UBSFund.Converters;

    [TestFixture]
    public class BoolToVisibilityConverterTests
    {
        [Test, TestCaseSource(typeof (TestCaseDataFactory), nameof(TestCaseDataFactory.ConvertTestCases))]
        public Visibility Convert_Test(bool value)
        {
            var converter = new BoolToVisibilityConverter();

            return (Visibility)converter.Convert(value, null, null, null);
        }

        [Test, TestCaseSource(typeof (TestCaseDataFactory), nameof(TestCaseDataFactory.ConvertBackTestCases))]
        public bool ConvertBack_Test(Visibility value)
        {
            var converter = new BoolToVisibilityConverter();

            return (bool)converter.ConvertBack(value, null, null, null);
        }
    }

    public class TestCaseDataFactory
    {
        public static IEnumerable ConvertTestCases
        {
            get
            {
                yield return new TestCaseData(true).Returns(Visibility.Visible);
                yield return new TestCaseData(false).Returns(Visibility.Collapsed);
            }
        }

        public static IEnumerable ConvertBackTestCases
        {
            get
            {
                yield return new TestCaseData(Visibility.Visible).Returns(true);
                yield return new TestCaseData(Visibility.Collapsed).Returns(false);
                yield return new TestCaseData(Visibility.Hidden).Returns(false);
            }
        }
    }
}