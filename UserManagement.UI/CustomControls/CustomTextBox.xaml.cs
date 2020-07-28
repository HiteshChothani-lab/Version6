using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace UserManagement.UI.CustomControls
{
    /// <summary>
    /// Interaction logic for CustomTextBox.xaml
    /// </summary>
    public partial class CustomTextBox : Grid
    {
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(CustomTextBox));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text), typeof(string), typeof(CustomTextBox),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty ControlFontSizeProperty = DependencyProperty.Register(nameof(ControlFontSize), typeof(double), typeof(CustomTextBox), new PropertyMetadata(14.0));
        public static readonly DependencyProperty ControlMaxLengthProperty = DependencyProperty.Register(nameof(ControlMaxLength), typeof(double), typeof(CustomTextBox), new PropertyMetadata(100.0));
        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register(nameof(BorderBrush), typeof(SolidColorBrush), typeof(CustomTextBox), new PropertyMetadata(Brushes.LightGray));
        public static readonly DependencyProperty CustomColorFontWeightProperty = DependencyProperty.Register(nameof(CustomColorFontWeight), typeof(FontWeight), typeof(CustomTextBox), new PropertyMetadata(FontWeights.Normal));
        public static readonly DependencyProperty IsNumericProperty = DependencyProperty.Register(nameof(IsNumeric), typeof(bool), typeof(CustomTextBox));
        public static readonly DependencyProperty IsDateFormatProperty = DependencyProperty.Register(nameof(IsDateFormat), typeof(bool), typeof(CustomTextBox), new PropertyMetadata(false));

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

        public double ControlMaxLength
        {
            get { return System.Convert.ToDouble(GetValue(ControlMaxLengthProperty)); }
            set { SetValue(ControlMaxLengthProperty, value); }
        }

        public double ControlFontSize
        {
            get { return System.Convert.ToDouble(GetValue(ControlFontSizeProperty)); }
            set { SetValue(ControlFontSizeProperty, value); }
        }

        public FontWeight CustomColorFontWeight
        {
            get { return (FontWeight)GetValue(CustomColorFontWeightProperty); }
            set { SetValue(CustomColorFontWeightProperty, value); }
        }

        public Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        public bool IsNumeric
        {
            get { return (bool)GetValue(IsNumericProperty); }
            set { SetValue(IsNumericProperty, value); }
        }

        public bool IsDateFormat
        {
            get { return (bool)GetValue(IsDateFormatProperty); }
            set { SetValue(IsDateFormatProperty, value); }
        }

        public CustomTextBox()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }
        
        private void TextBlock_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (IsNumeric)
                e.Handled = !IsTextAllowed(e.Text);
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void DateFormat(string text)
        {
            this.Text = string.Empty;

            text = text.Trim().Replace(" ", string.Empty)
                            .Replace("/", string.Empty)
                            .Replace(".", string.Empty)
                            .Replace("-", string.Empty);

            var count = text.Length;

            if (count == 2)
            {
                text = text + "/";
            }
            else if (count == 3)
            {
                text = text.Insert(2, "/");
            }
            else if (count >= 4)
            {
                text = text.Insert(2, "/").Insert(5, "/");
            }

            this.Text = text.Length >= 11 ? text.Remove(10) : text;
            textBox.CaretIndex = textBox.Text.Length;
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Text) && IsDateFormat &&
                e.Key != Key.Left && e.Key != Key.Right &&
                e.Key != Key.Up && e.Key != Key.Down &&
                e.Key != Key.Back && e.Key != Key.Delete)
                DateFormat(this.Text);
        }
    }
}
