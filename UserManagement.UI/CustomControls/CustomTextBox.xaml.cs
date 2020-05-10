using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Input;
using System.Windows.Media;

namespace UserManagement.UI.CustomControls
{
    /// <summary>
    /// Interaction logic for CustomTextBox.xaml
    /// </summary>
    public partial class CustomTextBox
    {
        public static readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register(
                nameof(BorderBrush),
                typeof(SolidColorBrush),
                typeof(CustomTextBox),
                new PropertyMetadata(Brushes.LightGray));

        public static readonly DependencyProperty ControlFontSizeProperty =
            DependencyProperty.Register(
                nameof(ControlFontSize),
                typeof(double),
                typeof(CustomTextBox),
                new PropertyMetadata(14.0));

        public static readonly DependencyProperty ControlMaxLengthProperty =
            DependencyProperty.Register(
                nameof(ControlMaxLength),
                typeof(double),
                typeof(CustomTextBox),
                new PropertyMetadata(100.0));

        public static readonly DependencyProperty IsDateFormatProperty =
            DependencyProperty.Register(
                nameof(IsDateFormat),
                typeof(bool),
                typeof(CustomTextBox),
                new PropertyMetadata(false));

        public static readonly DependencyProperty IsNumericProperty =
            DependencyProperty.Register(
                nameof(IsNumeric),
                typeof(bool),
                typeof(CustomTextBox));

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(
                nameof(Placeholder),
                typeof(string),
                typeof(CustomTextBox));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(CustomTextBox),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private static readonly Regex _regex = new Regex("[^0-9.-/]+");

        public CustomTextBox()
        {
            InitializeComponent();
        }

        public Brush BorderBrush
        {
            get => (Brush)GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }

        public double ControlFontSize
        {
            get => Convert.ToDouble(GetValue(ControlFontSizeProperty));
            set => SetValue(ControlFontSizeProperty, value);
        }

        public double ControlMaxLength
        {
            get => Convert.ToDouble(GetValue(ControlMaxLengthProperty));
            set => SetValue(ControlMaxLengthProperty, value);
        }

        public bool IsDateFormat
        {
            get => (bool)GetValue(IsDateFormatProperty);
            set => SetValue(IsDateFormatProperty, value);
        }

        public bool IsNumeric
        {
            get => (bool)GetValue(IsNumericProperty);
            set => SetValue(IsNumericProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new CustomTextBoxAutomationPeer(this);
        }

        private void DateFormat(string text)
        {
            Text = string.Empty;

            text = text.Trim().Replace(" ", string.Empty)
                            .Replace("/", string.Empty)
                            .Replace(".", string.Empty)
                            .Replace("-", string.Empty);
            
            switch (text.Length)
            {
                case 0: break;
                case 1: break;
                case 2: text += "/"; break;
                case 3: text = text.Insert(2, "/"); break;
                default:  text = text.Insert(2, "/").Insert(5, "/"); break;
            }
            
            Text = text.Length >= 11 ? text.Remove(10) : text;
            textBox.CaretIndex = textBox.Text.Length;
        }

        //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void TextBlock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (IsNumeric) e.Handled = !IsTextAllowed(e.Text);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Text) &&
                IsDateFormat &&
                e.Key != Key.Left &&
                e.Key != Key.Right &&
                e.Key != Key.Up &&
                e.Key != Key.Down &&
                e.Key != Key.Back &&
                e.Key != Key.Delete)
                DateFormat(Text);
        }
    }
}