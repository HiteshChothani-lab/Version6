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
using System.Windows.Media;
using UserManagement.Common.Constants;
using UserManagement.Common.Enums;
using UserManagement.Entity;
using UserManagement.Manager;
using UserManagement.UI.Events;
using UserManagement.UI.ItemModels;

namespace UserManagement.UI.ViewModels
{
    public class NonMobileUserPopupPageViewModel : ViewModelBase
    {
        private readonly ILocationManager _locationManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowsManager _windowsManager;

        public NonMobileUserPopupPageViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IWindowsManager windowsManager, ILocationManager locationManager) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _windowsManager = windowsManager;
            _locationManager = locationManager;

            this.CancelCommand = new DelegateCommand(() => ExecuteCancelCommand());

            this.RegisterAndFillCommand = new DelegateCommand(async () => await ExecuteRegisterAndFillCommand());

            this.FillCommand = new DelegateCommand(async () => await ExecuteFillCommand());

            this.CountriesSelectionChangedCommand = new DelegateCommand(() => ExecuteCountriesSelectionChangedCommand());
            this.StatesSelectionChangedCommand = new DelegateCommand(() => ExecuteStatesSelectionChangedCommand());

            if (this.Countries == null)
            {
                this.Countries = _locationManager.GetCountries();
                this.SelectedCountry = this.Countries.FirstOrDefault();

                PostalCodePlaceholder = this.SelectedCountry.Id == 231 ? "Zip Code" : "Postal Code";
            }

            if (this.States == null && this.SelectedCountry != null)
            {
                var states = _locationManager.GetStates();
                this.States = states.Where(x => x.CountryId == this.SelectedCountry.Id).ToList();
                this.SelectedState = this.States.FirstOrDefault();
            }

            if (this.Cities == null && this.SelectedState != null)
            {
                var cities = _locationManager.GetCities();
                this.Cities = cities.Where(x => x.StateId == this.SelectedState.Id).ToList();
                this.SelectedCity = this.Cities.FirstOrDefault();
            }
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
        
        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand RegisterAndFillCommand { get; private set; }
        public DelegateCommand FillCommand { get; private set; }
        public DelegateCommand CountriesSelectionChangedCommand { get; private set; }
        public DelegateCommand StatesSelectionChangedCommand { get; private set; }

        private void ExecuteCancelCommand()
        {
            this.RegionNavigationService.Journal.Clear();
            _eventAggregator.GetEvent<NonMobileUserUpdateEvent>().Publish(null);
            SetUnsetProperties();
        }

        private async Task ExecuteRegisterAndFillCommand()
        {
            if (!string.IsNullOrWhiteSpace(this.PostalCode))
                this.PostalCode = this.PostalCode.ToUpper();

            if (string.IsNullOrEmpty(FirstName))
            {
                MessageBox.Show("First Name is required.", "Required");
            }
            else if (string.IsNullOrEmpty(LastName))
            {
                MessageBox.Show("Last Name is required.", "Required");
            }
            else if (this.DOB == null)
            {
                MessageBox.Show("Please enter correct date of birth.", "Required");
            }
            else if (string.IsNullOrWhiteSpace(Gender))
            {
                MessageBox.Show("Please specify gender.", "Required");
            }
            else if (!string.IsNullOrWhiteSpace(this.PostalCode) && this.SelectedCountry.Id == 231 && !Regex.IsMatch(this.PostalCode, "^[0-9]{5}$"))
            {
                MessageBox.Show("Invalid zip code format for US.");
            }
            else if (!string.IsNullOrWhiteSpace(this.PostalCode) && this.SelectedCountry.Id == 101 && !Regex.IsMatch(this.PostalCode, "^[0-9]{6}$"))
            {
                MessageBox.Show("Invalid postal code format for India.");
            }
            else if (!string.IsNullOrWhiteSpace(this.PostalCode) && this.SelectedCountry.Id == 38 && !Regex.IsMatch(this.PostalCode, "[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]"))
            {
                MessageBox.Show("Invalid postal code format for Canada.");
            }
            else
            {
                var result = await _windowsManager.CheckStoreUser(new CheckUserRequestEntity()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    PostalCode = PostalCode,
                    CountryCode = Config.MasterStore.CountryCode,
                    HomePhone = HomePhone,
                    MasterStoreId = Config.MasterStore.StoreId,
                    SuperMasterId = Config.MasterStore.UserId,
                    Country = SelectedCountry?.Name,
                    City = SelectedCity?.Name,
                    State = SelectedState?.Name,
                    Gender = Gender,
                    DOB = DOB.Value.ToString("yyyy-MM-dd"),
                    Action = "Register"
                });

                if (result.StatusCode == (int)GenericStatusValue.Success)
                {
                    if (Convert.ToBoolean(result.Status))
                    {
                        this.RegionNavigationService.Journal.Clear();

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
                            DOB = DOB.Value.ToString("yyyy-MM-dd")
                        });

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
        }

        private async Task ExecuteFillCommand()
        {
            if (!string.IsNullOrWhiteSpace(this.PostalCode))
                this.PostalCode = this.PostalCode.ToUpper();

            if (string.IsNullOrEmpty(FirstName))
            {
                MessageBox.Show("First Name field is required.", "Required");
            }
            else if (string.IsNullOrEmpty(LastName))
            {
                MessageBox.Show("Last Name field is required.", "Required");
            }
            else if (this.DOB == null)
            {
                MessageBox.Show("Please enter correct date of birth.", "Required");
            }
            else if (string.IsNullOrWhiteSpace(Gender))
            {
                MessageBox.Show("Please specify gender.", "Required");
            }
            else if (!string.IsNullOrWhiteSpace(this.PostalCode) && this.SelectedCountry.Id == 231 && !Regex.IsMatch(this.PostalCode, "^[0-9]{5}$"))
            {
                MessageBox.Show("Invalid zip code format for US.");
            }
            else if (!string.IsNullOrWhiteSpace(this.PostalCode) && this.SelectedCountry.Id == 101 && !Regex.IsMatch(this.PostalCode, "^[0-9]{6}$"))
            {
                MessageBox.Show("Invalid postal code format for India.");
            }
            else if (!string.IsNullOrWhiteSpace(this.PostalCode) && this.SelectedCountry.Id == 38 && !Regex.IsMatch(this.PostalCode, "[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]"))
            {
                MessageBox.Show("Invalid postal code format for Canada.");
            }
            else
            {
                var result = await _windowsManager.CheckStoreUser(new CheckUserRequestEntity()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    PostalCode = PostalCode,
                    CountryCode = Config.MasterStore.CountryCode,
                    HomePhone = HomePhone,
                    MasterStoreId = Config.MasterStore.StoreId,
                    SuperMasterId = Config.MasterStore.UserId,
                    Country = SelectedCountry?.Name,
                    City = SelectedCity?.Name,
                    State = SelectedState?.Name,
                    Gender = Gender,
                    DOB = DOB.Value.ToString("yyyy-MM-dd"),
                    Action = "Fill"
                });

                if (result.StatusCode == (int)GenericStatusValue.Success)
                {
                    if (Convert.ToBoolean(result.Status))
                    {
                        this.RegionNavigationService.Journal.Clear();

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
                            DOB = DOB.Value.ToString("yyyy-MM-dd")
                        });

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
        }

        private void ExecuteCountriesSelectionChangedCommand()
        {
            var states = _locationManager.GetStates();
            this.States = states.Where(x => x.CountryId == this.SelectedCountry.Id).ToList();
            this.SelectedState = this.States.FirstOrDefault();

            PostalCodePlaceholder = this.SelectedCountry.Id == 231 ? "Zip Code" : "Postal Code";
        }

        private void ExecuteStatesSelectionChangedCommand()
        {
            if (this.SelectedState != null)
            {
                var cities = _locationManager.GetCities();
                this.Cities = cities.Where(x => x.StateId == this.SelectedState.Id).ToList();
                this.SelectedCity = this.Cities.FirstOrDefault();
            }
        }
        
        private void SetUnsetProperties()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            DOBText = string.Empty;
            DOB = null;
            if (Countries != null)
                this.SelectedCountry = Countries.FirstOrDefault();
            if (States != null)
                this.SelectedState = States.FirstOrDefault();
            if (Cities != null)
                this.SelectedCity = Cities.FirstOrDefault();
            this.Gender = "Male";
            this.PostalCode = string.Empty;
            this.HomePhone = string.Empty;
            PostalCodePlaceholder = "Postal Code";
        }
    }
}
