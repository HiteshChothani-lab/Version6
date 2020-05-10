using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using UserManagement.Common.Constants;
using UserManagement.Entity;
using UserManagement.Manager;
using UserManagement.UI.ItemModels;

namespace UserManagement.UI.ViewModels
{
    public class RegisterMasterStore1PageViewModel : ViewModelBase
    {
        private readonly ILocationManager _locationManager;

        public RegisterMasterStore1PageViewModel(IRegionManager regionManager, ILocationManager locationManager) : base(regionManager)
        {
            _locationManager = locationManager;

            CountriesSelectionChangedCommand = new DelegateCommand(ExecuteCountriesSelectionChangedCommand);
            StatesSelectionChangedCommand = new DelegateCommand(ExecuteStatesSelectionChangedCommand);
            SubmitCommand = new DelegateCommand(ExecuteSubmitCommand,
                () => SelectedCountry != null &&
                      SelectedState != null &&
                      SelectedCity != null &&
                      !string.IsNullOrEmpty(PostalCode))

                .ObservesProperty(() => SelectedCountry)
                .ObservesProperty(() => SelectedState)
                .ObservesProperty(() => SelectedCity)
                .ObservesProperty(() => PostalCode);
        }

        private bool _canExecuteSubmitCommand = true;
        public bool CanExecuteSubmitCommand
        {
            get => _canExecuteSubmitCommand;
            set => SetProperty(ref _canExecuteSubmitCommand, value);
        }

        private MasterStoreItemModel _masterStore;
        public MasterStoreItemModel MasterStore
        {
            get => _masterStore;
            set => SetProperty(ref _masterStore, value);
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

        private string _postalCode = string.Empty;
        public string PostalCode
        {
            get => _postalCode;
            set => SetProperty(ref _postalCode, value);
        }

        private string _postalCodeText = "Postal Code";
        public string PostalCodeText
        {
            get => _postalCodeText;
            set => SetProperty(ref _postalCodeText, value);
        }

        private TimeZoneInfo _selectedTimeZone;
        public TimeZoneInfo SelectedTimeZone
        {
            get => _selectedTimeZone;
            set => SetProperty(ref _selectedTimeZone, value);
        }

        private List<TimeZoneInfo> _timeZones;
        public List<TimeZoneInfo> TimeZones
        {
            get => _timeZones;
            set => SetProperty(ref _timeZones, value);
        }

        public DelegateCommand CountriesSelectionChangedCommand { get; private set; }
        public DelegateCommand StatesSelectionChangedCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }
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

        private void ExecuteSubmitCommand()
        {
            CanExecuteSubmitCommand = false;
            PostalCode = PostalCode.ToUpper();
            if (SelectedCountry.Id != 38 && SelectedCountry.Id != 231 && SelectedCountry.Id != 101)
                MessageBox.Show("The selected country is not allowed yet. The available countries are Canada, US and India.");
            else
                switch (SelectedCountry.Id)
                {
                    case 231 when !Regex.IsMatch(PostalCode, "^[0-9]{5}$"):
                        MessageBox.Show("Invalid zip code format for US.");
                        break;
                    case 101 when !Regex.IsMatch(PostalCode, "^[0-9]{6}$"):
                        MessageBox.Show("Invalid postal code format for India.");
                        break;
                    case 38 when !Regex.IsMatch(PostalCode, "[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]"):
                        MessageBox.Show("Invalid postal code format for Canada.");
                        break;
                    default:
                        {
                            var parameters = new NavigationParameters();
                            MasterStore.Country = SelectedCountry;
                            MasterStore.City = SelectedCity;
                            MasterStore.State = SelectedState;
                            MasterStore.PostalCode = PostalCode;
                            MasterStore.TimeZone = SelectedTimeZone.BaseUtcOffset.ToString();
                            MasterStore.TimeZoneDisplayName =
                                $"{SelectedTimeZone.BaseUtcOffset} {SelectedTimeZone.StandardName}";
                            MasterStore.SelectedTimeZone = SelectedTimeZone;

                            parameters.Add(NavigationConstants.MasterStoreModel, MasterStore);

                            RegionManager.RequestNavigate("ContentRegion", ViewNames.RegisterMasterStore2Page, parameters);
                            break;
                        }
                }

            CanExecuteSubmitCommand = true;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (Countries == null)
            {
                Countries = _locationManager.GetCountries();
                SelectedCountry = Countries.FirstOrDefault();
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

            if (TimeZones == null)
            {
                TimeZones = TimeZoneInfo.GetSystemTimeZones().ToList();
                SelectedTimeZone = TimeZones.FirstOrDefault(s => s.Id == TimeZone.CurrentTimeZone.StandardName);
            }

            if (navigationContext.Parameters.Any(x => x.Key == NavigationConstants.MasterStoreModel))
            {
                MasterStore = navigationContext.Parameters[NavigationConstants.MasterStoreModel] as MasterStoreItemModel;
            }

#if DEBUG
            PostalCode = "A1A2A3";
#endif
        }
    }
}
