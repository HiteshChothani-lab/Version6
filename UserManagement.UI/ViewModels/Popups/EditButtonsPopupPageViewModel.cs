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
    public class EditButtonsPopupPageViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowsManager _windowsManager;

        public EditButtonsPopupPageViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IWindowsManager windowsManager) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _windowsManager = windowsManager;

            this.CancelCommand = new DelegateCommand(() => ExecuteCancelCommand());
            this.SubmitCommand = new DelegateCommand(async () => await ExecuteSubmitCommand());
        }

        private StoreUserEntity _selectedStoreUser;
        public StoreUserEntity SelectedStoreUser
        {
            get => _selectedStoreUser;
            set => SetProperty(ref _selectedStoreUser, value);
        }

        private bool IsSelectedStoreUser = false;

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

        private bool _isCheckedButtonA;
        public bool IsCheckedButtonA
        {
            get => _isCheckedButtonA;
            set => SetProperty(ref _isCheckedButtonA, value);
        }

        private bool _isCheckedButtonB;
        public bool IsCheckedButtonB
        {
            get => _isCheckedButtonB;
            set => SetProperty(ref _isCheckedButtonB, value);
        }

        private bool _isCheckedButtonC;
        public bool IsCheckedButtonC
        {
            get => _isCheckedButtonC;
            set => SetProperty(ref _isCheckedButtonC, value);
        }

        private bool _isCheckedButtonD;
        public bool IsCheckedButtonD
        {
            get => _isCheckedButtonD;
            set => SetProperty(ref _isCheckedButtonD, value);
        }

        private bool _serviceUsedStatusYes = true;
        public bool ServiceUsedStatusYes
        {
            get => _serviceUsedStatusYes;
            set => SetProperty(ref _serviceUsedStatusYes, value);
        }

        private bool _serviceUsedStatusNo;
        public bool ServiceUsedStatusNo
        {
            get => _serviceUsedStatusNo;
            set => SetProperty(ref _serviceUsedStatusNo, value);
        }

        private string _question1;
        public string Question1
        {
            get => _question1;
            set => SetProperty(ref _question1, value);
        }

        private string _question2;
        public string Question2
        {
            get => _question2;
            set => SetProperty(ref _question2, value);
        }

        private string _question3;
        public string Question3
        {
            get => _question3;
            set => SetProperty(ref _question3, value);
        }

        private string _question4;
        public string Question4
        {
            get => _question4;
            set => SetProperty(ref _question4, value);
        }

        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }

        private void ExecuteCancelCommand()
        {
            this.RegionNavigationService.Journal.Clear();
            _eventAggregator.GetEvent<EditButtonsSubmitEvent>().Publish(null);
            SetUnsetProperties();
        }

        private async Task ExecuteSubmitCommand()
        {
            if (!this.IsCheckedButtonA && !this.IsCheckedButtonB && !this.IsCheckedButtonC && !this.IsCheckedButtonD)
            {
                MessageBox.Show("You must make a selection for Auto or Health Science or Nature or Other or all.", "Required.");
                return;
            }

            var reqEntity = new UpdateButtonsRequestEntity
            {
                Id = this.SelectedStoreUser.Id,
                UserId = Convert.ToInt32(this.SelectedStoreUser.UserId),
                SuperMasterId = Config.MasterStore.UserId,
                Action = this.IsSelectedStoreUser ? "update_buttons" : "update_buttons_archive"
            };

            reqEntity.Button1 = string.Empty;
            reqEntity.Button2 = string.Empty;
            reqEntity.Button3 = string.Empty;
            reqEntity.Button4 = string.Empty;
            reqEntity.Question1 = string.Empty;
            reqEntity.Question2 = string.Empty;
            reqEntity.Question3 = string.Empty;
            reqEntity.Question4 = string.Empty;

            if (this.IsCheckedButtonA)
            {
                reqEntity.Button1 = "Auto";
            }

            if (this.IsCheckedButtonB)
            {
                reqEntity.Button2 = "Health Science";
            }

            if (this.IsCheckedButtonD)
            {
                reqEntity.Button3 = "Other";
            }

            if (this.IsCheckedButtonC)
            {
                reqEntity.Button4 = "Nature";
            }

            reqEntity.ServiceUsedStatus = this.ServiceUsedStatusYes ? "Yes" : "No";
            reqEntity.Question1 = this.Question1;
            reqEntity.Question2 = this.Question2;
            reqEntity.Question3 = this.Question3;
            reqEntity.Question4 = this.Question4;

            var result = await _windowsManager.UpdateButtons(reqEntity);

            if (result.StatusCode == (int)GenericStatusValue.Success)
            {
                if (Convert.ToBoolean(result.Status))
                {
                    this.RegionNavigationService.Journal.Clear();
                    _eventAggregator.GetEvent<EditButtonsSubmitEvent>().Publish(new EditButtonsItemModel());
                    SetUnsetProperties();
                }
                else
                {
                    MessageBox.Show(result.Messagee, "Unsuccessful");
                }
            }
            else if (result.StatusCode == (int)GenericStatusValue.NoInternetConnection)
            {
                MessageBox.Show(MessageBoxMessage.NoInternetConnection, "Unsuccessful");
            }
            else if (result.StatusCode == (int)GenericStatusValue.HasErrorMessage)
            {
                MessageBox.Show(result.Message, "Unsuccessful");
            }
            else
            {
                MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
            }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            SetUnsetProperties();

            if (navigationContext.Parameters[NavigationConstants.SelectedStoreUser] is StoreUserEntity selectedStoreUser)
            {
                SelectedStoreUser = selectedStoreUser;
            }

            if (navigationContext.Parameters[NavigationConstants.IsSelectedStoreUser] is bool isSelectedStoreUser)
            {
                IsSelectedStoreUser = isSelectedStoreUser;
            }

            this.IsCheckedButtonA = !string.IsNullOrWhiteSpace(SelectedStoreUser.Btn1);
            this.IsCheckedButtonB = !string.IsNullOrWhiteSpace(SelectedStoreUser.Btn2);
            this.IsCheckedButtonC = !string.IsNullOrWhiteSpace(SelectedStoreUser.Btn4);
            this.IsCheckedButtonD = !string.IsNullOrWhiteSpace(SelectedStoreUser.Btn3);

            if ("Yes".Equals(SelectedStoreUser.ServiceUsedStatus))
            {
                this.ServiceUsedStatusYes = true;
                this.ServiceUsedStatusNo = false;
            }
            else if ("No".Equals(SelectedStoreUser.ServiceUsedStatus))
            {
                this.ServiceUsedStatusYes = false;
                this.ServiceUsedStatusNo = true;
            }
            else
            {
                this.ServiceUsedStatusYes = true;
            }

            this.Question1 = SelectedStoreUser.Question1;
            this.Question2 = SelectedStoreUser.Question2;
            this.Question3 = SelectedStoreUser.Question3;
            this.Question4 = SelectedStoreUser.Question4;

        }

        private void SetUnsetProperties()
        {
            this.IsCheckedButtonA = this.IsCheckedButtonB = this.IsCheckedButtonC = this.IsCheckedButtonD = false;
            this.IsUserTypeMobile = false;
            this.IsUserTypeNonMobile = false;
            this.IsSelectedStoreUser = false;
        }
    }
}
