using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
        private readonly ILocationManager _locationManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowsManager _windowsManager;

        public EditButtonsPopupPageViewModel(IRegionManager regionManager, IEventAggregator eventAggregator,
            IWindowsManager windowsManager, ILocationManager locationManager) : base(regionManager)
        {
            _eventAggregator = eventAggregator;
            _windowsManager = windowsManager;
            _locationManager = locationManager;

            this.ToCountriesSelectionChangedCommand = new DelegateCommand(() => ExecuteToCountriesSelectionChangedCommand());
            this.ToStatesSelectionChangedCommand = new DelegateCommand(() => ExecuteToStatesSelectionChangedCommand());
            this.FromCountriesSelectionChangedCommand = new DelegateCommand(() => ExecuteFromCountriesSelectionChangedCommand());
            this.FromStatesSelectionChangedCommand = new DelegateCommand(() => ExecuteFromStatesSelectionChangedCommand());
            this.ToggleButtonCommand = new DelegateCommand<string>((item) => ExecuteToggleButtonCommand(item));

            CancelCommand = new DelegateCommand(() => ExecuteCancelCommand());
            SubmitCommand = new DelegateCommand(async () => await ExecuteSubmitCommand());
            AddNewItemCommand = new DelegateCommand(() => AddNewItemMethod());
            RemoveItemCommand = new DelegateCommand<ItemsCollection>((item) => RemoveItemMethod(item));

            ItemCollections = new ObservableCollection<ItemsCollection>();
        }

        private List<CountryEntity> _toCountries;
        public List<CountryEntity> ToCountries
        {
            get => _toCountries;
            set => SetProperty(ref _toCountries, value);
        }

        private List<StateEntity> _toStates;
        public List<StateEntity> ToStates
        {
            get => _toStates;
            set => SetProperty(ref _toStates, value);
        }

        private List<CityEntity> _toCities;
        public List<CityEntity> ToCities
        {
            get => _toCities;
            set => SetProperty(ref _toCities, value);
        }

        private CountryEntity _selectedToCountry;
        public CountryEntity SelectedToCountry
        {
            get => _selectedToCountry;
            set => SetProperty(ref _selectedToCountry, value);
        }

        private StateEntity _selectedToState;
        public StateEntity SelectedToState
        {
            get => _selectedToState;
            set => SetProperty(ref _selectedToState, value);
        }

        private CityEntity _selectedToCity;
        public CityEntity SelectedToCity
        {
            get => _selectedToCity;
            set => SetProperty(ref _selectedToCity, value);
        }

        private string _toPostalCode = string.Empty;
        public string ToPostalCode
        {
            get => _toPostalCode;
            set => SetProperty(ref _toPostalCode, value);
        }

        private string _toPostalCodeText = "Postal Code";
        public string ToPostalCodeText
        {
            get => _toPostalCodeText;
            set => SetProperty(ref _toPostalCodeText, value);
        }

        private List<CountryEntity> _fromCountries;
        public List<CountryEntity> FromCountries
        {
            get => _fromCountries;
            set => SetProperty(ref _fromCountries, value);
        }

        private List<StateEntity> _fromStates;
        public List<StateEntity> FromStates
        {
            get => _fromStates;
            set => SetProperty(ref _fromStates, value);
        }

        private List<CityEntity> _fromCities;
        public List<CityEntity> FromCities
        {
            get => _fromCities;
            set => SetProperty(ref _fromCities, value);
        }

        private CountryEntity _selectedFromCountry;
        public CountryEntity SelectedFromCountry
        {
            get => _selectedFromCountry;
            set => SetProperty(ref _selectedFromCountry, value);
        }

        private StateEntity _selectedFromState;
        public StateEntity SelectedFromState
        {
            get => _selectedFromState;
            set => SetProperty(ref _selectedFromState, value);
        }

        private CityEntity _selectedFromCity;
        public CityEntity SelectedFromCity
        {
            get => _selectedFromCity;
            set => SetProperty(ref _selectedFromCity, value);
        }

        private string _fromPostalCode = string.Empty;
        public string FromPostalCode
        {
            get => _fromPostalCode;
            set => SetProperty(ref _fromPostalCode, value);
        }

        private string _fromPostalCodeText = "Postal Code";
        public string FromPostalCodeText
        {
            get => _fromPostalCodeText;
            set => SetProperty(ref _fromPostalCodeText, value);
        }

        private bool IsSelectedStoreUser = false;

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

        private bool _toAddressResidential;
        public bool ToAddressResidential
        {
            get => _toAddressResidential;
            set => SetProperty(ref _toAddressResidential, value);
        }

        private bool _toAddressCommercia;
        public bool ToAddressCommercia
        {
            get => _toAddressCommercia;
            set => SetProperty(ref _toAddressCommercia, value);
        }

        private bool _toAddressYes;
        public bool ToAddressYes
        {
            get => _toAddressYes;
            set => SetProperty(ref _toAddressYes, value);
        }

        private bool _toAddressNo;
        public bool ToAddressNo
        {
            get => _toAddressNo;
            set => SetProperty(ref _toAddressNo, value);
        }

        private bool _parcelBreakableNo;
        public bool ParcelBreakableNo
        {
            get => _parcelBreakableNo;
            set => SetProperty(ref _parcelBreakableNo, value);
        }

        private bool _parcelBreakableYes;
        public bool ParcelBreakableYes
        {
            get => _parcelBreakableYes;
            set => SetProperty(ref _parcelBreakableYes, value);
        }

        private bool _parcelReplaceableNo;
        public bool ParcelReplaceableNo
        {
            get => _parcelReplaceableNo;
            set => SetProperty(ref _parcelReplaceableNo, value);
        }

        private bool _parcelReplaceableYes;
        public bool ParcelReplaceableYes
        {
            get => _parcelReplaceableYes;
            set => SetProperty(ref _parcelReplaceableYes, value);
        }

        private bool _deliveryNo;
        public bool DeliveryNo
        {
            get => _deliveryNo;
            set => SetProperty(ref _deliveryNo, value);
        }

        private bool _deliveryYes;
        public bool DeliveryYes
        {
            get => _deliveryYes;
            set => SetProperty(ref _deliveryYes, value);
        }

        private bool _packagingShippingNo;
        public bool PackagingShippingNo
        {
            get => _packagingShippingNo;
            set => SetProperty(ref _packagingShippingNo, value);
        }

        private bool _packagingShippingYes;
        public bool PackagingShippingYes
        {
            get => _packagingShippingYes;
            set => SetProperty(ref _packagingShippingYes, value);
        }

        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand SubmitCommand { get; private set; }

        public DelegateCommand ToCountriesSelectionChangedCommand { get; private set; }
        public DelegateCommand ToStatesSelectionChangedCommand { get; private set; }
        public DelegateCommand FromCountriesSelectionChangedCommand { get; private set; }
        public DelegateCommand FromStatesSelectionChangedCommand { get; private set; }
        public DelegateCommand<string> ToggleButtonCommand { get; private set; }

        public ObservableCollection<ItemsCollection> ItemCollections { get; set; }
        public ICommand AddNewItemCommand { get; private set; }
        public ICommand RemoveItemCommand { get; private set; }

        private void ExecuteToggleButtonCommand(string parameter)
        {
            if (string.IsNullOrEmpty(parameter) && !parameter.Contains("_"))
                return;

            var values = parameter.Split('_');
            //toaddress_residential toaddress_commercia
            //toaddress_no toaddress_yes
            //parcelbreakable_no parcelbreakable_yes
            //parcelreplaceable_no parcelreplaceable_yes
            //delivery_no delivery_yes
            //packagingshipping_no packagingshipping_yes

            if ("toaddress".Equals(values[0]))
            {
                switch (values[1])
                {
                    case "residential":
                        ToAddressResidential = true; ToAddressCommercia = false;
                        break;
                    case "commercia":
                        ToAddressResidential = false; ToAddressCommercia = true;
                        break;
                    case "no":
                        ToAddressNo = true; ToAddressYes = false;
                        break;
                    case "yes":
                        ToAddressNo = false; ToAddressYes = true;
                        break;
                }
            }
            else if ("parcelbreakable".Equals(values[0]))
            {
                switch (values[1])
                {
                    case "no":
                        ParcelBreakableNo = true; ParcelBreakableYes = false;
                        break;
                    case "yes":
                        ParcelBreakableNo = false; ParcelBreakableYes = true;
                        break;
                }
            }
            else if ("parcelreplaceable".Equals(values[0]))
            {
                switch (values[1])
                {
                    case "no":
                        ParcelReplaceableNo = true; ParcelReplaceableYes = false;
                        break;
                    case "yes":
                        ParcelReplaceableNo = false; ParcelReplaceableYes = true;
                        break;
                }
            }
            else if ("delivery".Equals(values[0]))
            {
                switch (values[1])
                {
                    case "no":
                        DeliveryNo = true; DeliveryYes = false;
                        break;
                    case "yes":
                        DeliveryNo = false; DeliveryYes = true;
                        break;
                }
            }
            else if ("packagingshipping".Equals(values[0]))
            {
                switch (values[1])
                {
                    case "no":
                        PackagingShippingNo = true; PackagingShippingYes = false;
                        break;
                    case "yes":
                        PackagingShippingNo = false; PackagingShippingYes = true;
                        break;
                }
            }
        }

        private void ExecuteToCountriesSelectionChangedCommand()
        {
            var states = _locationManager.GetStates();
            this.ToStates = states.Where(x => x.CountryId == this.SelectedToCountry.Id).ToList();
            this.SelectedToState = this.ToStates.FirstOrDefault();

            ToPostalCodeText = this.SelectedToCountry.Id == 231 ? "Zip Code" : "Postal Code";
        }

        private void ExecuteToStatesSelectionChangedCommand()
        {
            if (this.SelectedToState != null)
            {
                var cities = _locationManager.GetCities();
                this.ToCities = cities.Where(x => x.StateId == this.SelectedToState.Id).ToList();
                this.SelectedToCity = this.ToCities.FirstOrDefault();
            }
        }

        private void ExecuteFromCountriesSelectionChangedCommand()
        {
            var states = _locationManager.GetStates();
            this.FromStates = states.Where(x => x.CountryId == this.SelectedFromCountry.Id).ToList();
            this.SelectedFromState = this.FromStates.FirstOrDefault();

            FromPostalCodeText = this.SelectedFromCountry.Id == 231 ? "Zip Code" : "Postal Code";
        }

        private void ExecuteFromStatesSelectionChangedCommand()
        {
            if (this.SelectedFromState != null)
            {
                var cities = _locationManager.GetCities();
                this.FromCities = cities.Where(x => x.StateId == this.SelectedFromState.Id).ToList();
                this.SelectedFromCity = this.FromCities.FirstOrDefault();
            }
        }

        private void AddNewItemMethod()
        {
            ItemCollections.Add(new ItemsCollection
            {
                Header = $"Item {ItemCollections.Count + 2}",
                Text1 = string.Empty,
                Text2 = string.Empty
            });
        }

        private void RemoveItemMethod(ItemsCollection item)
        {
            ItemCollections.Remove(item);

            for (int i = 0; i < ItemCollections.Count; i++)
            {
                ItemCollections[i].Header = $"Item {i + 2}";
            }
        }

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
                MessageBox.Show("You must make a selection for Pack and Ship or Print or Mailboxes or Business Services or all.", "Required.");
                return;
            }

            this.ToPostalCode = this.ToPostalCode.ToUpper();

            if (this.SelectedToCountry.Id != 38 && this.SelectedToCountry.Id != 231 && this.SelectedToCountry.Id != 101)
            {
                MessageBox.Show("The selected country is not allowed yet. The available countries are Canada, US and India.");
            }
            else if (this.SelectedToCountry.Id == 231 && !Regex.IsMatch(this.ToPostalCode, "^[0-9]{5}$"))
            {
                MessageBox.Show("Invalid zip code format for US.");
            }
            else if (this.SelectedToCountry.Id == 101 && !Regex.IsMatch(this.ToPostalCode, "^[0-9]{6}$"))
            {
                MessageBox.Show("Invalid postal code format for India.");
            }
            else if ((this.SelectedToCountry.Id == 38) && !Regex.IsMatch(this.ToPostalCode, "[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]"))
            {
                MessageBox.Show("Invalid postal code format for Canada.");
            }
            else
            {
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

                if (this.IsCheckedButtonA)
                {
                    reqEntity.Button1 = "Pack and Ship";
                }

                if (this.IsCheckedButtonB)
                {
                    reqEntity.Button2 = "Print";
                }

                if (this.IsCheckedButtonC)
                {
                    reqEntity.Button3 = "Mailboxes";
                }

                if (this.IsCheckedButtonD)
                {
                    reqEntity.Button4 = "Business Services";
                }

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
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            SetUnsetProperties();

            if (ToCountries == null && FromCountries == null)
            {
                ToCountries = _locationManager.GetCountries();
                FromCountries = _locationManager.GetCountries();
                SelectedToCountry = ToCountries.FirstOrDefault();
                this.SelectedFromCountry = FromCountries.FirstOrDefault();
            }

            if (this.ToStates == null && this.FromStates == null && this.SelectedToCountry != null && SelectedFromCountry != null)
            {
                var states = _locationManager.GetStates();
                ToStates = states.Where(x => x.CountryId == SelectedToCountry.Id).ToList();
                FromStates = states.Where(x => x.CountryId == SelectedFromCountry.Id).ToList();
                SelectedToState = ToStates.FirstOrDefault();
                SelectedFromState = FromStates.FirstOrDefault();
            }

            if (ToCities == null && FromCities == null && SelectedToState != null && SelectedFromState != null)
            {
                var cities = _locationManager.GetCities();
                ToCities = cities.Where(x => x.StateId == SelectedToState.Id).ToList();
                FromCities = cities.Where(x => x.StateId == SelectedFromState.Id).ToList();
                SelectedToCity = ToCities.FirstOrDefault();
                SelectedFromCity = FromCities.FirstOrDefault();
            }

            if (navigationContext.Parameters[NavigationConstants.SelectedStoreUser] is StoreUserEntity selectedStoreUser)
                SelectedStoreUser = selectedStoreUser;

            if (navigationContext.Parameters[NavigationConstants.IsSelectedStoreUser] is bool isSelectedStoreUser)
                IsSelectedStoreUser = isSelectedStoreUser;

            this.IsCheckedButtonA = !string.IsNullOrWhiteSpace(SelectedStoreUser.Btn1);
            this.IsCheckedButtonB = !string.IsNullOrWhiteSpace(SelectedStoreUser.Btn2);
            this.IsCheckedButtonC = !string.IsNullOrWhiteSpace(SelectedStoreUser.Btn3);
            this.IsCheckedButtonD = !string.IsNullOrWhiteSpace(SelectedStoreUser.Btn4);
        }

        private void SetUnsetProperties()
        {
            this.IsCheckedButtonA = this.IsCheckedButtonB = this.IsCheckedButtonC = this.IsCheckedButtonD = false;
            this.IsUserTypeMobile = false;
            this.IsUserTypeNonMobile = false;
            this.IsSelectedStoreUser = false;
        }
    }

    public class ItemsCollection : INotifyPropertyChanged
    {
        private string _header = string.Empty;
        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
                OnPropertyRaised("Header");
            }
        }

        public string Text1 { get; set; }

        public string Text2 { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
