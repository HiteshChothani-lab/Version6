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

            this.CountriesSelectionChangedCommand = new DelegateCommand(() => ExecuteCountriesSelectionChangedCommand());
            this.StatesSelectionChangedCommand = new DelegateCommand(() => ExecuteStatesSelectionChangedCommand());
            this.SubmitCommand = new DelegateCommand(() => ExecuteSubmitCommand(),
                () => this.SelectedCountry != null && this.SelectedState != null && this.SelectedCity != null && !string.IsNullOrEmpty(this.PostalCode))
                .ObservesProperty(() => this.SelectedCountry)
                .ObservesProperty(() => this.SelectedState)
                .ObservesProperty(() => this.SelectedCity)
                .ObservesProperty(() => this.PostalCode);
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
            this.States = states.Where(x => x.CountryId == this.SelectedCountry.Id).ToList();
            this.SelectedState = this.States.FirstOrDefault();

            PostalCodeText = this.SelectedCountry.Id == 231 ? "Zip Code" : "Postal Code";
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

        private void ExecuteSubmitCommand()
        {
            this.CanExecuteSubmitCommand = false;
            this.PostalCode = this.PostalCode.ToUpper();

            if (this.SelectedCountry.Id != 38 && this.SelectedCountry.Id != 231 && this.SelectedCountry.Id != 101)
            {
                MessageBox.Show("The selected country is not allowed yet. The available countries are Canada, US and India.");
            }
            else if (this.SelectedCountry.Id == 231 && !Regex.IsMatch(this.PostalCode, "^[0-9]{5}$"))
            {
                MessageBox.Show("Invalid zip code format for US.");
            }
            else if (this.SelectedCountry.Id == 101 && !Regex.IsMatch(this.PostalCode, "^[0-9]{6}$"))
            {
                MessageBox.Show("Invalid postal code format for India.");
            }
            else if ((this.SelectedCountry.Id == 38) && !Regex.IsMatch(this.PostalCode, "[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]"))
            {
                MessageBox.Show("Invalid postal code format for Canada.");
            }
            else
            {
                var parameters = new NavigationParameters();
                this.MasterStore.Country = this.SelectedCountry;
                this.MasterStore.City = this.SelectedCity;
                this.MasterStore.State = this.SelectedState;
                this.MasterStore.PostalCode = this.PostalCode;
                this.MasterStore.TimeZone = this.SelectedTimeZone.BaseUtcOffset.ToString();
                this.MasterStore.TimeZoneDisplayName = $"{this.SelectedTimeZone.BaseUtcOffset.ToString()} {this.SelectedTimeZone.StandardName}";

                parameters.Add(NavigationConstants.MasterStoreModel, this.MasterStore);

                this.RegionManager.RequestNavigate("ContentRegion", ViewNames.RegisterMasterStore2Page, parameters);
            }

            this.CanExecuteSubmitCommand = true;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (this.Countries == null)
            {
                this.Countries = _locationManager.GetCountries();
                this.SelectedCountry = this.Countries.FirstOrDefault();
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

            if (this.TimeZones == null)
            {
                this.TimeZones = TimeZoneInfo.GetSystemTimeZones().ToList();
                this.SelectedTimeZone = this.TimeZones.FirstOrDefault(s => s.Id == TimeZone.CurrentTimeZone.StandardName);
            }

            if (navigationContext.Parameters.Any(x => x.Key == NavigationConstants.MasterStoreModel))
            {
                this.MasterStore = navigationContext.Parameters[NavigationConstants.MasterStoreModel] as MasterStoreItemModel;
            }
        }
    }
}
