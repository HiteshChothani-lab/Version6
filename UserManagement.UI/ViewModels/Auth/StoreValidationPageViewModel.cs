using Prism.Commands;
using Prism.Regions;
using System;
using System.Threading.Tasks;
using System.Windows;
using UserManagement.Common.Constants;
using UserManagement.Common.Enums;
using UserManagement.Manager;
using UserManagement.UI.ItemModels;

namespace UserManagement.UI.ViewModels
{
    public class StoreValidationPageViewModel : ViewModelBase
    {
        private readonly IWindowsManager _windowsManager;
        public StoreValidationPageViewModel(
            IRegionManager regionManager,
            IWindowsManager windowsManager) : base(regionManager)
        {
            _windowsManager = windowsManager;

            SubmitCommand = new DelegateCommand(
                    async () => await ExecuteSubmitCommand(),
                () =>
                      !string.IsNullOrEmpty(Username) &&
                      !string.IsNullOrEmpty(AccessCode) &&
                      CanExecuteSubmitCommand)

                .ObservesProperty(() => Username)
                .ObservesProperty(() => AccessCode)
                .ObservesProperty(() => CanExecuteSubmitCommand);
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _accessCode;
        public string AccessCode
        {
            get => _accessCode;
            set => SetProperty(ref _accessCode, value);
        }

        private bool _canExecuteSubmitCommand = true;
        public bool CanExecuteSubmitCommand
        {
            get => _canExecuteSubmitCommand;
            set => SetProperty(ref _canExecuteSubmitCommand, value);
        }
        public DelegateCommand SubmitCommand { get; private set; }

        private async Task ExecuteSubmitCommand()
        {
            CanExecuteSubmitCommand = false;

            var result = await _windowsManager.ValidateUser(new Entity.ValidateUserRequestEntity()
            {
                Username = Username,
                AccessCode = AccessCode
            });

            switch (result.StatusCode)
            {
                case (int)GenericStatusValue.Success when Convert.ToBoolean(result.Status):
                {
                    var parameters = new NavigationParameters
                    {
                        {NavigationConstants.MasterStoreModel, new MasterStoreItemModel() {UserId = result.UserId}}
                    };

                    RegionManager.RequestNavigate(
                        "ContentRegion",
                        ViewNames.RegisterMasterStore1Page, parameters);
                    break;
                }
                case (int)GenericStatusValue.Success:
                    MessageBox.Show(result.Messagee, "Unsuccessful");
                    break;
                case (int)GenericStatusValue.NoInternetConnection:
                    MessageBox.Show(MessageBoxMessage.NoInternetConnection, "Unsuccessful");
                    break;
                case (int)GenericStatusValue.HasErrorMessage:
                    MessageBox.Show(result.Message, "Unsuccessful");
                    break;
                default:
                    MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
                    break;
            }

            CanExecuteSubmitCommand = true;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

#if DEBUG
            Username = "Store4";
            AccessCode = "111111";
#endif
        }
    }
}
