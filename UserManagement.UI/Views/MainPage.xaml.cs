using System.Collections.Specialized;
using System.Windows.Controls;

namespace UserManagement.UI.Views
{
    /// <summary>
    /// Interaction logic for MainPage
    /// </summary>
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            ((INotifyCollectionChanged)MainTableListView.Items).CollectionChanged += MainTableListView_CollectionChanged;
        }

        private void MainTableListView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                if (MainTableListView != null && MainTableListView.Items != null && MainTableListView.Items.Count > 1)
                    MainTableListView.ScrollIntoView(MainTableListView.Items[MainTableListView.Items.Count - 1]);
            }
            catch { }
        }
    }
}
