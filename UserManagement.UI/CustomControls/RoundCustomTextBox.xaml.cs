using System.Windows;
using System.Windows.Controls;

namespace UserManagement.UI.CustomControls
{
    /// <summary>
    /// Interaction logic for CustomTextBox.xaml
    /// </summary>
    public partial class RoundCustomTextBox : Grid
    {
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(RoundCustomTextBox));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(RoundCustomTextBox), new FrameworkPropertyMetadata(
            string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty ControlFontSizeProperty = DependencyProperty.Register(nameof(ControlFontSize), typeof(double), typeof(RoundCustomTextBox), new PropertyMetadata(14.0));

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public double ControlFontSize
        {
            get { return System.Convert.ToDouble( GetValue(ControlFontSizeProperty)); }
            set { SetValue(ControlFontSizeProperty, value); }
        }

        public RoundCustomTextBox()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
