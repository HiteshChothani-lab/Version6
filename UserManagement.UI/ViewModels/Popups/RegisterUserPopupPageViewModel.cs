using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Globalization;
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
    public class RegisterUserPopupPageViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowsManager _windowsManager;

        public RegisterUserPopupPageViewModel(IRegionManager regionManager, IEventAggregator eventAggregator,
            IWindowsManager windowsManager) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _windowsManager = windowsManager;

            CancelCommand = new DelegateCommand(() => ExecuteCancelCommand());
            SubmitCommand = new DelegateCommand(async () => await ExecuteSubmitCommand());
        }

        private DateTime? _dob;
        public DateTime? DOB
        {
            get => _dob;
            set => SetProperty(ref _dob, value);
        }

        private string _dobText = string.Empty;
        public string DOBText
        {
            get => _dobText;
            set
            {
                if (DateTime.TryParseExact(value, "MM/dd/yyyy",
                           CultureInfo.InvariantCulture,
                           DateTimeStyles.None, out DateTime dateValue))
                {
                    DOB = dateValue;
                }
                else
                {
                    DOB = null;
                }
                SetProperty(ref _dobText, value);
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _gender = "Male";
        public string Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private string _mobile;
        public string Mobile
        {
            get => _mobile;
            set => SetProperty(ref _mobile, value);
        }

        private SaveUserDataRequestEntity _selectedStoreUser;
        public SaveUserDataRequestEntity SelectedStoreUser
        {
            get => _selectedStoreUser;
            set => SetProperty(ref _selectedStoreUser, value);
        }

        public DelegateCommand SubmitCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            //SetUnsetProperties();

            if (navigationContext.Parameters[NavigationConstants.SelectedStoreUser] is SaveUserDataRequestEntity selectedStoreUser)
                SelectedStoreUser = selectedStoreUser;

            Populatefields();
        }

        private bool CheckForm()
        {
            bool ok = true;

            if (string.IsNullOrEmpty(FirstName))
            {
                MessageBox.Show("First Name field is required.", "Required");
                ok = false;
            }
            else if (string.IsNullOrEmpty(LastName))
            {
                MessageBox.Show("Last Name field is required.", "Required");
                ok = false;
            }
            else if (string.IsNullOrEmpty(Mobile))
            {
                MessageBox.Show("Mobile field is required.", "Required");
                ok = false;
            }
            else if (DOB == null)
            {
                MessageBox.Show("Please enter correct date of birth.", "Required");
                ok = false;
            }
            else if (string.IsNullOrWhiteSpace(Gender))
            {
                MessageBox.Show("Please specify gender.", "Required");
                ok = false;
            }

            return ok;
        }

        private void ExecuteCancelCommand()
        {
            RegionNavigationService.Journal.Clear();
            _eventAggregator.GetEvent<RegisterStoreUserSubmitEvent>().Publish(null);
            SetUnsetProperties();
        }

        private async Task ExecuteSubmitCommand()
        {
            if (CheckForm())
            {
                SelectedStoreUser.FirstName = FirstName;
                SelectedStoreUser.LastName = LastName;
                SelectedStoreUser.SuperMasterId = Config.MasterStore.UserId;
                SelectedStoreUser.MasterStoreId = Config.MasterStore.StoreId;
                SelectedStoreUser.Mobile = Mobile;
                SelectedStoreUser.Action = "Mobile";
                SelectedStoreUser.Gender = Gender;
                SelectedStoreUser.DOB = DOB != null ? DOB.Value.ToString("yyyy-MM-dd") : string.Empty;

                //Save as a dummy data and after success we can save the user
                var dummyResult = await _windowsManager.SaveUserData(SelectedStoreUser, true);

                if (dummyResult.StatusCode == (int)GenericStatusValue.Success)
                {
                   var result = await _windowsManager.SaveUserData(SelectedStoreUser, false);

                    switch (result.StatusCode)
                    {
                        case (int)GenericStatusValue.Success when Convert.ToBoolean(result.Status):
                            RegionNavigationService.Journal.Clear();

                            _eventAggregator.GetEvent<RegisterStoreUserSubmitEvent>().Publish(new EditStoreUserItemModel());

                            SetUnsetProperties();
                            break;

                        case (int)GenericStatusValue.Success:
                            MessageBox.Show(result.Message, "Unsuccessful");
                            break;

                        case (int)GenericStatusValue.NoInternetConnection:
                            MessageBox.Show(MessageBoxMessage.NoInternetConnection, "Unsuccessful");
                            break;

                        case (int)GenericStatusValue.HasErrorMessage:
                            MessageBox.Show(((EntityBase)result).Message, "Unsuccessful");
                            break;

                        default:
                            MessageBox.Show(MessageBoxMessage.UnknownErorr, "Unsuccessful");
                            break;
                    }
                }
                else
                    MessageBox.Show(dummyResult.Messagee, "Unsuccessful");
            }
        }

        private void Populatefields()
        {
            FirstName = SelectedStoreUser.FirstName;
            LastName = SelectedStoreUser.LastName;
            Mobile = SelectedStoreUser.Mobile;
        }

        private void SetUnsetProperties()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Mobile = string.Empty;
            DOB = null;
            DOBText = string.Empty;
            Gender = "Male";
        }
    }
}