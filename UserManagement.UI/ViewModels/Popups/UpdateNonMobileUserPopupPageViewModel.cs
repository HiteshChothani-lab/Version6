using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
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
    public class UpdateNonMobileUserPopupPageViewModel : ViewModelBase
    {
        private readonly ILocationManager _locationManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowsManager _windowsManager;
        
        public UpdateNonMobileUserPopupPageViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IWindowsManager windowsManager, ILocationManager locationManager) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _windowsManager = windowsManager;
            _locationManager = locationManager;

            CancelCommand = new DelegateCommand(() => ExecuteCancelCommand());
            SubmitCommand = new DelegateCommand(async () => await ExecuteSubmit());
            StatesSelectionChangedCommand = new DelegateCommand(() => ExecuteStatesSelectionChangedCommand());
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private DateTime _dob = DateTime.Now;
        public DateTime DOB
        {
            get => _dob;
            set => SetProperty(ref _dob, value);
        }

        private List<CountryEntity> _countries;
        public List<CountryEntity> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        private CountryEntity _selectedCountry;
        public CountryEntity SelectedCountry
        {
            get => _selectedCountry;
            set => SetProperty(ref _selectedCountry, value);
        }

        private List<StateEntity> _states;
        public List<StateEntity> States
        {
            get => _states;
            set => SetProperty(ref _states, value);
        }

        private StateEntity _selectedState;
        public StateEntity SelectedState
        {
            get => _selectedState;
            set => SetProperty(ref _selectedState, value);
        }

        private List<CityEntity> _cities;
        public List<CityEntity> Cities
        {
            get => _cities;
            set => SetProperty(ref _cities, value);
        }

        private CityEntity _selectedCity;
        public CityEntity SelectedCity
        {
            get => _selectedCity;
            set => SetProperty(ref _selectedCity, value);
        }

        private string _gender = "Male";
        public string Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        private string _homePhone;
        public string HomePhone
        {
            get => _homePhone;
            set => SetProperty(ref _homePhone, value);
        }

        private string _postalCode;
        public string PostalCode
        {
            get => _postalCode;
            set => SetProperty(ref _postalCode, value);
        }
        
        private string _postalCodePlaceholder = "Postal Code";
        public string PostalCodePlaceholder
        {
            get => _postalCodePlaceholder;
            set => SetProperty(ref _postalCodePlaceholder, value);
        }
        
        private StoreUserEntity _selectedStoreUser;
        public StoreUserEntity SelectedStoreUser
        {
            get => _selectedStoreUser;
            set => SetProperty(ref _selectedStoreUser, value);
        }

        private string _action;
        private string _roomNumber;

        public string Action
        {
            get => _action;
            set => SetProperty(ref _action, value);
        }

        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }
        public DelegateCommand StatesSelectionChangedCommand { get; private set; }

        private void ExecuteCancelCommand()
        {
            RegionNavigationService.Journal.Clear();
            _eventAggregator.GetEvent<NonMobileUserEditEvent>().Publish(null);
            SetUnsetProperties();
        }

        private async Task ExecuteSubmit()
        {
            if (!string.IsNullOrWhiteSpace(PostalCode))
                PostalCode = PostalCode.ToUpper();

            if (!string.IsNullOrWhiteSpace(PostalCode) && SelectedCountry.Id == 231 && !Regex.IsMatch(PostalCode, "^[0-9]{5}$"))
            {
                MessageBox.Show("Invalid zip code format for US.");
            }
            else if (!string.IsNullOrWhiteSpace(PostalCode) && SelectedCountry.Id == 101 && !Regex.IsMatch(PostalCode, "^[0-9]{6}$"))
            {
                MessageBox.Show("Invalid postal code format for India.");
            }
            else if (!string.IsNullOrWhiteSpace(PostalCode) && SelectedCountry.Id == 38 && !Regex.IsMatch(PostalCode, "[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]"))
            {
                MessageBox.Show("Invalid postal code format for Canada.");
            }
            else
            {
                
                var result = await _windowsManager.UpdateNonMobileStoreUser(new UpdateNonMobileStoreUserRequestEntity()
                {
                    Action = Action,
                    HomePhone = HomePhone,
                    Id = SelectedStoreUser.Id,
                    UserId = Convert.ToInt32(SelectedStoreUser.UserId),
                    SuperMasterId = Config.MasterStore.UserId,
                    MasterStoreId = Config.MasterStore.SuperMasterId.ToString(),
                    PostalCode = PostalCode,
                    FirstName = FirstName,
                    LastName = LastName,
                    Country = SelectedCountry?.Name,
                    City = SelectedCity?.Name,
                    State = SelectedState?.Name,
                    Gender = Gender,
                    DOB = DOB.ToString("yyyy-MM-dd")
                });

                if (result.StatusCode == (int)GenericStatusValue.Success)
                {
                    if (Convert.ToBoolean(result.Status))
                    {
                        RegionNavigationService.Journal.Clear();

                        _eventAggregator.GetEvent<NonMobileUserUpdateEvent>().Publish(new NonMobileUserItemModel()
                        {
                            FirstName = FirstName,
                            HomePhone = HomePhone,
                            LastName = LastName,
                            PostalCode = PostalCode,
                            Country = SelectedCountry?.Name,
                            City = SelectedCity?.Name,
                            State = SelectedState?.Name,
                            Gender = Gender,
                            DOB = DOB.ToString("yyyy-MM-dd"),
                            IsNewRecord = false
                        });

                        SetUnsetProperties();
                    }
                    else
                    {
                        MessageBox.Show(result.Message, "Unsuccessful");
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
        }

        private void ExecuteStatesSelectionChangedCommand()
        {
            if (SelectedState != null)
            {
                var cities = _locationManager.GetCities();
                Cities = cities.Where(x => x.StateId == SelectedState.Id).ToList();

                var selectedCity = Cities.Where(w => w.Name == SelectedStoreUser.City).FirstOrDefault();

                if (string.IsNullOrWhiteSpace(SelectedStoreUser.City) || selectedCity == null)
                    SelectedCity = Cities.FirstOrDefault();
                else
                    SelectedCity = selectedCity;
            }
        }
        
        private void SetUnsetProperties()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            DOB = DateTime.Now;
            if (Countries != null)
                SelectedCountry = Countries.FirstOrDefault();
            if (States != null)
                SelectedState = States.FirstOrDefault();
            if (Cities != null)
                SelectedCity = Cities.FirstOrDefault();
            Gender = "Male";
            PostalCode = string.Empty;
            HomePhone = string.Empty;
            Countries = null;
            States = null;
            Cities = null;
            PostalCodePlaceholder = "Postal Code";
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            SetUnsetProperties();

            Action = navigationContext.Parameters[NavigationConstants.Action].ToString();
            var selectedStoreUser = navigationContext.Parameters[NavigationConstants.SelectedStoreUser] as StoreUserEntity;

            if (selectedStoreUser != null)
            {
                SelectedStoreUser = selectedStoreUser;

                FirstName = SelectedStoreUser.Firstname;
                LastName = SelectedStoreUser.Lastname;
                HomePhone = SelectedStoreUser.HomePhone;
                PostalCode = SelectedStoreUser.PostalCode;
                RoomNumber = selectedStoreUser.RoomNumber;
                DOB = Convert.ToDateTime(SelectedStoreUser.DOB);
                Gender = SelectedStoreUser.Gender;
            }

            if (Countries == null)
            {
                Countries = _locationManager.GetCountries();

                if (string.IsNullOrWhiteSpace(SelectedStoreUser.Country))
                    SelectedCountry = Countries.FirstOrDefault();
                else
                    SelectedCountry = Countries.Where(w => w.Name == SelectedStoreUser.Country).FirstOrDefault();

                PostalCodePlaceholder = SelectedCountry.Id == 231 ? "Zip Code" : "Postal Code";
            }

            if (States == null && SelectedCountry != null)
            {
                var states = _locationManager.GetStates();
                States = states.Where(x => x.CountryId == SelectedCountry.Id).ToList();

                if (string.IsNullOrWhiteSpace(SelectedStoreUser.State))
                    SelectedState = States.FirstOrDefault();
                else
                    SelectedState = States.Where(w => w.Name == SelectedStoreUser.State).FirstOrDefault();
            }

            if (Cities == null && SelectedState != null)
            {
                var cities = _locationManager.GetCities();
                Cities = cities.Where(x => x.StateId == SelectedState.Id).ToList();

                if (string.IsNullOrWhiteSpace(SelectedStoreUser.City))
                    SelectedCity = Cities.FirstOrDefault();
                else
                    SelectedCity = Cities.Where(w => w.Name == SelectedStoreUser.City).FirstOrDefault();
            }
        }

        public string RoomNumber
        {
            get => _roomNumber;
            set => SetProperty(ref _roomNumber , value);
        }
    }
}
