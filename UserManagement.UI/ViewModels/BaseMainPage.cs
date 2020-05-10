using Prism.Events;
using Prism.Regions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using UserManagement.Manager;


namespace UserManagement.UI.ViewModels
{
    public class BaseMainPage : ViewModelBase
    {
        
        protected bool _canTapAddCommand = true;
        protected IEventAggregator _eventAggregator;
        protected string _expressTime;
        protected IWindowsManager _windowsManager;
        protected object locker = new object();
        protected Timer UpdateExpressTime;
        private string _loaderMessage = string.Empty;
        private Visibility _loaderVisibility = Visibility.Collapsed;

        public BaseMainPage(IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IWindowsManager windowsManager) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _windowsManager = windowsManager;
            UpdateExpressTime = new Timer(60000); /* 60000 Millisecond = 1 Minute (Interval) */
        }

        public bool CanTapAddCommand
        {
            get => _canTapAddCommand;
            set => SetProperty(ref _canTapAddCommand, value);
        }

        public string ExpressTime
        {
            get => _expressTime;
            set => SetProperty(ref _expressTime, value);
        }

        public string LoaderMessage
        {
            get => _loaderMessage;
            set => SetProperty(ref _loaderMessage, value);
        }

        public Visibility LoaderVisibility
        {
            get => _loaderVisibility;
            set => SetProperty(ref _loaderVisibility, value);
        }

        protected virtual async Task GetData()
        {
            LoaderVisibility = Visibility.Visible;
            if (UpdateExpressTime.Enabled)
                UpdateExpressTime.Stop();
        }

        protected virtual void ResetFields()
        {
        }

        protected void SetLoaderVisibility(string message = "")
        {
            LoaderMessage = message;

            LoaderVisibility = string.IsNullOrEmpty(message) ? Visibility.Collapsed : Visibility.Visible;
        }

        protected void StartStopExpressTimer()
        {
            if (!UpdateExpressTime.Enabled)
                UpdateExpressTime.Start();
            else UpdateExpressTime.Stop();
        }

        protected virtual void SetDelegateCommands()
        {
            
        }
    }
}