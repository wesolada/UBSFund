namespace UBSFund.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class ErrorPanel : UserControl
    {
        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register("ErrorMessage", typeof (string), typeof (ErrorPanel),
                new FrameworkPropertyMetadata(string.Empty));

        public ErrorPanel()
        {
            this.InitializeComponent();
        }

        public string ErrorMessage
        {
            get { return (string)this.GetValue(ErrorMessageProperty); }
            set { this.SetValue(ErrorMessageProperty, value); }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}