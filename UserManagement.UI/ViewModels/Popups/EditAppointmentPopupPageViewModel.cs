using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Threading.Tasks;
using System.Windows;
using UserManagement.Common.Constants;
using UserManagement.Common.Enums;
using UserManagement.Entity;
using UserManagement.Manager;
using UserManagement.UI.Events;
using UserManagement.UI.ItemModels;

namespace UserManagement.UI.ViewModels
{
    public class EditAppointmentPopupPageViewModel : Version4Buttons
    {

        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowsManager _windowsManager;

        public EditAppointmentPopupPageViewModel(
            IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IWindowsManager windowsManager) :
            base(regionManager, eventAggregator, windowsManager)
        {
            _eventAggregator = eventAggregator;
            _windowsManager = windowsManager;

            CancelCommand = new DelegateCommand(() => ExecuteCancelCommand());
            SubmitCommand = new DelegateCommand(async () => await ExecuteSubmitCommand());
        }


        private StoreUserEntity _selectedStoreUser;
        public StoreUserEntity SelectedStoreUser
        {
            get => _selectedStoreUser;
            set => SetProperty(ref _selectedStoreUser, value);
        }

        private bool _isUserTypeMobile = true;
        public bool IsUserTypeMobile
        {
            get => _isUserTypeMobile;
            set => SetProperty(ref _isUserTypeMobile, value);
        }

        private bool _isUserTypeNonMobile;
        public bool IsUserTypeNonMobile
        {
            get => _isUserTypeNonMobile;
            set => SetProperty(ref _isUserTypeNonMobile, value);
        }


        private bool IsSelectedStoreUser = false;
        

        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }

        private void ExecuteCancelCommand()
        {
            RegionNavigationService.Journal.Clear();
            _eventAggregator.GetEvent<EditUserAgeOrNeedleSubmitEvent>().Publish(null);
        }

        private async Task ExecuteSubmitCommand()
        {
            if (!CheckSubmit()) return;

            var reqEntity = new UpdateButtonsRequestEntity
            {
                Id = SelectedStoreUser.Id,
                UserId = Convert.ToInt32(SelectedStoreUser.UserId),
                SuperMasterId = Config.MasterStore.UserId,
                Action = IsSelectedStoreUser ? "update_buttons" : "update_buttons_archive"
            };

            reqEntity.Button1 = string.Empty;

            UpdateServiceButtons(reqEntity);

            var result = await _windowsManager.UpdateButtons(reqEntity);

            if (result.StatusCode == (int)GenericStatusValue.Success)
            {
                if (Convert.ToBoolean(result.Status))
                {
                    RegionNavigationService.Journal.Clear();
                    _eventAggregator.GetEvent<EditUserAgeOrNeedleSubmitEvent>().Publish(new EditUserAgeOrNeedleItemModel());
                    SetUnsetProperties();
                }
                else
                    MessageBox.Show(result.Message, "Unsuccessful");
            }
            else if (result.StatusCode == (int)GenericStatusValue.NoInternetConnection)
                MessageBox.Show(MessageBoxMessage.NoInternetConnection, "Unsuccessful");
            else if (result.StatusCode == (int)GenericStatusValue.HasErrorMessage)
                MessageBox.Show(((EntityBase) result).Message, "Unsuccessful");
            else
                MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");

        }


        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            SetUnsetProperties();

            if (navigationContext.Parameters[NavigationConstants.SelectedStoreUser] is StoreUserEntity selectedStoreUser)
                SelectedStoreUser = selectedStoreUser;


            if (navigationContext.Parameters[NavigationConstants.IsSelectedStoreUser] is bool isSelectedStoreUser)
                IsSelectedStoreUser = isSelectedStoreUser;

            SetServiceButtons(SelectedStoreUser);
        }

        private void SetUnsetProperties()
        {
            SetServiceButtons();
            IsUserTypeMobile = true;
            IsUserTypeNonMobile = false;
            IsSelectedStoreUser = false;
            SelectedStoreUser = new StoreUserEntity();
        }
    }
}
