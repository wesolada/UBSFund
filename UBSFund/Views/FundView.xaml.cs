namespace UBSFund.Views
{
    using System.Globalization;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public partial class FundView : Window
    {
        public FundView()
        {
            this.InitializeComponent();
        }

        private void Currency_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string cultureName = ((CultureInfo)e.AddedItems[0]).Name;

            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
            this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
        }
    }
}