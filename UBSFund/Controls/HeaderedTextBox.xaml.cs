namespace UBSFund.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class HeaderedTextBox : UserControl
    {
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof (string), typeof (HeaderedTextBox));

        public static readonly DependencyProperty ContentValueProperty =
            DependencyProperty.Register("ContentValue", typeof (string), typeof (HeaderedTextBox),
                new FrameworkPropertyMetadata(string.Empty));

        public static readonly DependencyProperty ContentWidthProperty =
            DependencyProperty.Register("ContentWidth", typeof (double), typeof (HeaderedTextBox),
                new FrameworkPropertyMetadata(double.NaN));

        public static readonly DependencyProperty ContentAlignmentProperty =
            DependencyProperty.Register("ContentAlignment", typeof (HorizontalAlignment), typeof (HeaderedTextBox),
                new FrameworkPropertyMetadata(HorizontalAlignment.Left));

        public HeaderedTextBox()
        {
            this.InitializeComponent();
            Validation.SetValidationAdornerSiteFor(this.ContentTbx, this);
        }

        public string Header
        {
            get { return (string)this.GetValue(HeaderProperty); }
            set { this.SetValue(HeaderProperty, value); }
        }

        public string ContentValue
        {
            get { return (string)this.GetValue(ContentValueProperty); }
            set { this.SetValue(ContentValueProperty, value); }
        }

        public double ContentWidth
        {
            get { return (int)this.GetValue(ContentWidthProperty); }
            set { this.SetValue(ContentWidthProperty, value); }
        }

        public HorizontalAlignment ContentAlignment
        {
            get { return (HorizontalAlignment)this.GetValue(ContentAlignmentProperty); }
            set { this.SetValue(ContentAlignmentProperty, value); }
        }

        private void TextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}