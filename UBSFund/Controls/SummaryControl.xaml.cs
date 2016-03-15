namespace UBSFund.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class SummaryControl : UserControl
    {
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof (string), typeof (SummaryControl),
                new FrameworkPropertyMetadata(string.Empty));

        public static readonly DependencyProperty TotalNumberProperty =
            DependencyProperty.Register("TotalNumber", typeof (int), typeof (SummaryControl));

        public static readonly DependencyProperty TotalWeightProperty =
            DependencyProperty.Register("TotalWeight", typeof (decimal), typeof (SummaryControl));

        public static readonly DependencyProperty TotalMarketValueProperty =
            DependencyProperty.Register("TotalMarketValue", typeof (decimal), typeof (SummaryControl));

        public SummaryControl()
        {
            this.InitializeComponent();
        }

        public string Header
        {
            get { return (string)this.GetValue(HeaderProperty); }
            set { this.SetValue(HeaderProperty, value); }
        }

        public int TotalNumber
        {
            get { return (int)this.GetValue(TotalNumberProperty); }
            set { this.SetValue(TotalNumberProperty, value); }
        }

        public decimal TotalWeight
        {
            get { return (decimal)this.GetValue(TotalWeightProperty); }
            set { this.SetValue(TotalWeightProperty, value); }
        }

        public decimal TotalMarketValue
        {
            get { return (decimal)this.GetValue(TotalMarketValueProperty); }
            set { this.SetValue(TotalMarketValueProperty, value); }
        }
    }
}