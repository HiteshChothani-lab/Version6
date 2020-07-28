using System.Windows;

namespace UserManagement.UI.Views
{
    /// <summary>
    /// Interaction logic for UserDetailsPage.xaml
    /// </summary>
    public partial class UserDetailsPage : Window
    {
        public UserDetailsPage()
        {
            InitializeComponent();
        }

        private void UserDetailsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            //this.Left = desktopWorkingArea.Right - this.Width; (Display window on right side of screen)
            this.Left = desktopWorkingArea.Left; //(Display window on left side of screen)
        }
    }
}
