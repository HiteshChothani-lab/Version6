using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
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
        private readonly ILocationManager _locationManager;
        private readonly IWindowsManager _windowsManager;

        private List<CityEntity> _cities;

        private List<CountryEntity> _countries;

        private DateTime? _dob;

        private string _dobText = string.Empty;

        private string _firstName;

        private string _gender = "Male";

        private string _homePhone;

        private bool _isUserTypeMobile = true;

        private bool _isUserTypeNonMobile;

        private string _lastName;

        private string _mobile;

        private string _postalCode;

        private string _postalCodeText = "Postal Code";

        private CityEntity _selectedCity;

        private CountryEntity _selectedCountry;

        private StateEntity _selectedState;

        private StoreUserEntity _selectedStoreUser;

        private List<StateEntity> _states;

        public RegisterUserPopupPageViewModel(
            IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IWindowsManager windowsManager,
            ILocationManager locationManager) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _windowsManager = windowsManager;
            _locationManager = locationManager;

            CancelCommand = new DelegateCommand(() => ExecuteCancelCommand());
            SubmitCommand = new DelegateCommand(async () => await ExecuteSubmitCommand());

            CountriesSelectionChangedCommand = new DelegateCommand(() => ExecuteCountriesSelectionChangedCommand());
            StatesSelectionChangedCommand = new DelegateCommand(() => ExecuteStatesSelectionChangedCommand());
        }

        public DelegateCommand CancelCommand { get; private set; }

        public List<CityEntity> Cities
        {
            get => _cities;
            set => SetProperty(ref _cities, value);
        }

        public List<CountryEntity> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        public DelegateCommand CountriesSelectionChangedCommand { get; private set; }

        public DateTime? DOB
        {
            get => _dob;
            set => SetProperty(ref _dob, value);
        }

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

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        public string HomePhone
        {
            get => _homePhone;
            set => SetProperty(ref _homePhone, value);
        }

        public bool IsUserTypeMobile
        {
            get => _isUserTypeMobile;
            set => SetProperty(ref _isUserTypeMobile, value);
        }

        public bool IsUserTypeNonMobile
        {
            get => _isUserTypeNonMobile;
            set => SetProperty(ref _isUserTypeNonMobile, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string Mobile
        {
            get => _mobile;
            set => SetProperty(ref _mobile, value);
        }

        public string PostalCode
        {
            get => _postalCode;
            set => SetProperty(ref _postalCode, value);
        }

        public string PostalCodeText
        {
            get => _postalCodeText;
            set => SetProperty(ref _postalCodeText, value);
        }

        public CityEntity SelectedCity
        {
            get => _selectedCity;
            set => SetProperty(ref _selectedCity, value);
        }

        public CountryEntity SelectedCountry
        {
            get => _selectedCountry;
            set => SetProperty(ref _selectedCountry, value);
        }

        public StateEntity SelectedState
        {
            get => _selectedState;
            set => SetProperty(ref _selectedState, value);
        }

        public StoreUserEntity SelectedStoreUser
        {
            get => _selectedStoreUser;
            set => SetProperty(ref _selectedStoreUser, value);
        }

        public List<StateEntity> States
        {
            get => _states;
            set => SetProperty(ref _states, value);
        }

        public DelegateCommand StatesSelectionChangedCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            // SetUnsetProperties();
         
            var selectedStoreUser = navigationContext.Parameters[NavigationConstants.SelectedStoreUser] as StoreUserEntity;

            if (selectedStoreUser != null)
                SelectedStoreUser = selectedStoreUser;
            Populatefields();
            if (Countries == null)
            {
                Countries = _locationManager.GetCountries();
                SelectedCountry = Countries.FirstOrDefault();

                PostalCodeText = SelectedCountry.Id == 231 ? "Zip Code" : "Postal Code";
            }

            if (States == null && SelectedCountry != null)
            {
                var states = _locationManager.GetStates();
                States = states.Where(x => x.CountryId == SelectedCountry.Id).ToList();
                SelectedState = States.FirstOrDefault();
            }

            if (Cities == null && SelectedState != null)
            {
                var cities = _locationManager.GetCities();
                Cities = cities.Where(x => x.StateId == SelectedState.Id).ToList();
                SelectedCity = Cities.FirstOrDefault();
            }
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
            else if (string.IsNullOrEmpty(Mobile) && IsUserTypeMobile)
            {
                MessageBox.Show("Mobile field is required.", "Required");
                ok = false;
            }
            else if (!IsUserTypeMobile && DOB == null)
            {
                MessageBox.Show("Please enter correct date of birth.", "Required");
                ok = false;
            }
            else if (!IsUserTypeMobile && string.IsNullOrWhiteSpace(Gender))
            {
                MessageBox.Show("Please specify gender.", "Required");
                ok = false;
            }
            else if (!IsUserTypeMobile && !string.IsNullOrWhiteSpace(PostalCode) && SelectedCountry.Id == 231 &&
                     !Regex.IsMatch(PostalCode, "^[0-9]{5}$"))
            {
                MessageBox.Show("Invalid postal code format for US.");
                ok = false;
            }
            else if (!IsUserTypeMobile && !string.IsNullOrWhiteSpace(PostalCode) && SelectedCountry.Id == 101 &&
                     !Regex.IsMatch(PostalCode, "^[0-9]{6}$"))
            {
                MessageBox.Show("Invalid postal code format for India.");
                ok = false;
            }
            else if (!IsUserTypeMobile && !string.IsNullOrWhiteSpace(PostalCode) && (SelectedCountry.Id == 38) &&
                     !Regex.IsMatch(PostalCode,
                         "[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]"))
            {
                MessageBox.Show("Invalid postal code format for Canada.");
                ok = false;
            }

            return ok;
        }

        private void ExecuteCancelCommand()
        {
            RegionNavigationService.Journal.Clear();
            _eventAggregator.GetEvent<EditStoreUserSubmitEvent>().Publish(null);
            SetUnsetProperties();
        }

        private void ExecuteCountriesSelectionChangedCommand()
        {
            var states = _locationManager.GetStates();
            States = states.Where(x => x.CountryId == SelectedCountry.Id).ToList();
            SelectedState = States.FirstOrDefault();

            PostalCodeText = SelectedCountry.Id == 231 ? "Zip Code" : "Postal Code";
        }

        private void ExecuteStatesSelectionChangedCommand()
        {
            if (SelectedState == null) return;
            var cities = _locationManager.GetCities();
            Cities = cities.Where(x => x.StateId == SelectedState.Id).ToList();
            SelectedCity = Cities.FirstOrDefault();
        }

        private async Task ExecuteSubmitCommand()
        {
            if (!string.IsNullOrWhiteSpace(PostalCode))
                PostalCode = PostalCode.ToUpper();

            if (CheckForm())
            {
                var reqEntity = new EditStoreUserRequestEntity
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    CountryCode = SelectedStoreUser.CountryCode,
                    MasterStoreId = SelectedStoreUser.MasterStoreId,
                    UserId = SelectedStoreUser.UserId,
                    SuperMasterId = Config.MasterStore.UserId
                };

                if (IsUserTypeMobile)
                {
                    reqEntity.Mobile = Mobile;
                    reqEntity.HomePhone = string.Empty;
                    reqEntity.PostalCode = string.Empty;
                    reqEntity.Action = "Mobile";
                }
                else if (IsUserTypeNonMobile)
                {
                    reqEntity.Mobile = string.Empty;
                    reqEntity.HomePhone = HomePhone;
                    reqEntity.PostalCode = PostalCode;
                    reqEntity.Action = "Non Mobile";
                    reqEntity.Country = SelectedCountry?.Name;
                    reqEntity.City = SelectedCity?.Name;
                    reqEntity.State = SelectedState?.Name;
                    reqEntity.Gender = Gender;
                    reqEntity.DOB = DOB.Value.ToString("yyyy-MM-dd");
                }

                var result = await _windowsManager.EditStoreUser(reqEntity);

                switch (result.StatusCode)
                {
                    case (int)GenericStatusValue.Success when Convert.ToBoolean(result.Status):
                        RegionNavigationService.Journal.Clear();

                        _eventAggregator.GetEvent<EditStoreUserSubmitEvent>().Publish(new EditStoreUserItemModel());

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
        }

        private void Populatefields()
        {
            FirstName = SelectedStoreUser.Firstname;
            LastName = SelectedStoreUser.Lastname;
            Mobile = SelectedStoreUser.Mobile;
        }

        private void SetUnsetProperties()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            PostalCode = string.Empty;
            Mobile = string.Empty;
            HomePhone = string.Empty;
            IsUserTypeMobile = true;
            IsUserTypeNonMobile = false;
            DOB = null;
            DOBText = string.Empty;
            if (Countries != null)
                SelectedCountry = Countries.FirstOrDefault();
            if (States != null)
                SelectedState = States.FirstOrDefault();
            if (Cities != null)
                SelectedCity = Cities.FirstOrDefault();
            Gender = "Male";
            PostalCodeText = "Postal Code";
        }
    }
}