using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using UserManagement.Entity;
using UserManagement.Manager;

namespace UserManagement.UI.Views
{
    /// <summary>
    /// Interaction logic for PackAndShipPage.xaml
    /// </summary>
    public partial class PackAndShipPage : Window, INotifyPropertyChanged
    {
        public PackAndShipPage()
        {
            InitializeComponent();
        }

        public double BaseWidth { private get; set; }
        public double BaseHeight { private get; set; }
        public ILocationManager LocationManager { get; set; }

        private List<CountryEntity> _toCountries;
        public List<CountryEntity> ToCountries
        {
            get => _toCountries;
            set
            {
                _toCountries = value;
                OnPropertyRaised("ToCountries");
            }
        }

        private List<StateEntity> _toStates;
        public List<StateEntity> ToStates
        {
            get => _toStates;
            set { _toStates = value; OnPropertyRaised("ToStates"); }
        }

        private List<CityEntity> _toCities;
        public List<CityEntity> ToCities
        {
            get => _toCities;
            set { _toCities = value; OnPropertyRaised("ToCities"); }
        }

        private CountryEntity _selectedToCountry;
        public CountryEntity SelectedToCountry
        {
            get => _selectedToCountry;
            set { _selectedToCountry = value; OnPropertyRaised("SelectedToCountry"); }
        }

        private StateEntity _selectedToState;
        public StateEntity SelectedToState
        {
            get => _selectedToState;
            set { _selectedToState = value; OnPropertyRaised("SelectedToState"); }
        }

        private CityEntity _selectedToCity;
        public CityEntity SelectedToCity
        {
            get => _selectedToCity;
            set { _selectedToCity = value; OnPropertyRaised("SelectedToCity"); }
        }

        private string _toPostalCode = string.Empty;
        public string ToPostalCode
        {
            get => _toPostalCode;
            set { _toPostalCode = value; OnPropertyRaised("ToPostalCode"); }
        }

        private string _toPostalCodeText = "Postal Code";
        public string ToPostalCodeText
        {
            get => _toPostalCodeText;
            set { _toPostalCodeText = value; OnPropertyRaised("ToPostalCodeText"); }
        }

        private string _toPhoneNumber = string.Empty;
        public string ToPhoneNumber
        {
            get => _toPhoneNumber;
            set { _toPhoneNumber = value; OnPropertyRaised("ToPhoneNumber"); }
        }

        private string _toName = string.Empty;
        public string ToName
        {
            get => _toName;
            set { _toName = value; OnPropertyRaised("ToName"); }
        }

        private List<CountryEntity> _fromCountries;
        public List<CountryEntity> FromCountries
        {
            get => _fromCountries;
            set { _fromCountries = value; OnPropertyRaised("FromCountries"); }
        }

        private List<StateEntity> _fromStates;
        public List<StateEntity> FromStates
        {
            get => _fromStates;
            set { _fromStates = value; OnPropertyRaised("FromStates"); }
        }

        private List<CityEntity> _fromCities;
        public List<CityEntity> FromCities
        {
            get => _fromCities;
            set { _fromCities = value; OnPropertyRaised("FromCities"); }
        }

        private CountryEntity _selectedFromCountry;
        public CountryEntity SelectedFromCountry
        {
            get => _selectedFromCountry;
            set { _selectedFromCountry = value; OnPropertyRaised("SelectedFromCountry"); }
        }

        private StateEntity _selectedFromState;
        public StateEntity SelectedFromState
        {
            get => _selectedFromState;
            set { _selectedFromState = value; OnPropertyRaised("SelectedFromState"); }
        }

        private CityEntity _selectedFromCity;
        public CityEntity SelectedFromCity
        {
            get => _selectedFromCity;
            set { _selectedFromCity = value; OnPropertyRaised("SelectedFromCity"); }
        }

        private string _fromPostalCode = string.Empty;
        public string FromPostalCode
        {
            get => _fromPostalCode;
            set { _fromPostalCode = value; OnPropertyRaised("FromPostalCode"); }
        }

        private string _fromPostalCodeText = "Postal Code";
        public string FromPostalCodeText
        {
            get => _fromPostalCodeText;
            set { _fromPostalCodeText = value; OnPropertyRaised("FromPostalCodeText"); }
        }

        private string _fromPhoneNumber = string.Empty;
        public string FromPhoneNumber
        {
            get => _fromPhoneNumber;
            set { _fromPhoneNumber = value; OnPropertyRaised("FromPhoneNumber"); }
        }

        private string _fromName = string.Empty;
        public string FromName
        {
            get => _fromName;
            set { _fromName = value; OnPropertyRaised("FromName"); }
        }

        private bool _toAddressResidential;
        public bool ToAddressResidential
        {
            get => _toAddressResidential;
            set { _toAddressResidential = value; OnPropertyRaised("ToAddressResidential"); }
        }

        private bool _toAddressCommercia;
        public bool ToAddressCommercia
        {
            get => _toAddressCommercia;
            set { _toAddressCommercia = value; OnPropertyRaised("ToAddressCommercia"); }
        }

        private bool _toAddressYes;
        public bool ToAddressYes
        {
            get => _toAddressYes;
            set { _toAddressYes = value; OnPropertyRaised("ToAddressYes"); }
        }

        private bool _toAddressNo;
        public bool ToAddressNo
        {
            get => _toAddressNo;
            set { _toAddressNo = value; OnPropertyRaised("ToAddressNo"); }
        }

        private bool _parcelBreakableNo;
        public bool ParcelBreakableNo
        {
            get => _parcelBreakableNo;
            set { _parcelBreakableNo = value; OnPropertyRaised("ParcelBreakableNo"); }
        }

        private bool _parcelBreakableYes;
        public bool ParcelBreakableYes
        {
            get => _parcelBreakableYes;
            set { _parcelBreakableYes = value; OnPropertyRaised("ParcelBreakableYes"); }
        }

        private bool _parcelReplaceableNo;
        public bool ParcelReplaceableNo
        {
            get => _parcelReplaceableNo;
            set { _parcelReplaceableNo = value; OnPropertyRaised("ParcelReplaceableNo"); }
        }

        private bool _parcelReplaceableYes;
        public bool ParcelReplaceableYes
        {
            get => _parcelReplaceableYes;
            set { _parcelReplaceableYes = value; OnPropertyRaised("ParcelReplaceableYes"); }
        }

        private bool _deliveryNo;
        public bool DeliveryNo
        {
            get => _deliveryNo;
            set { _deliveryNo = value; OnPropertyRaised("DeliveryNo"); }
        }

        private bool _deliveryYes;
        public bool DeliveryYes
        {
            get => _deliveryYes;
            set { _deliveryYes = value; OnPropertyRaised("DeliveryYes"); }
        }

        private bool _packagingShippingNo;
        public bool PackagingShippingNo
        {
            get => _packagingShippingNo;
            set { _packagingShippingNo = value; OnPropertyRaised("PackagingShippingNo"); }
        }

        private bool _packagingShippingYes;
        public bool PackagingShippingYes
        {
            get => _packagingShippingYes;
            set { _packagingShippingYes = value; OnPropertyRaised("PackagingShippingYes"); }
        }

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
            var states = LocationManager.GetStates();
            this.ToStates = states.Where(x => x.CountryId == this.SelectedToCountry.Id).ToList();
            this.SelectedToState = this.ToStates.FirstOrDefault();

            ToPostalCodeText = this.SelectedToCountry.Id == 231 ? "Zip Code" : "Postal Code";
        }

        private void ExecuteToStatesSelectionChangedCommand()
        {
            if (this.SelectedToState != null)
            {
                var cities = LocationManager.GetCities();
                this.ToCities = cities.Where(x => x.StateId == this.SelectedToState.Id).ToList();
                this.SelectedToCity = this.ToCities.FirstOrDefault();
            }
        }

        private void ExecuteFromCountriesSelectionChangedCommand()
        {
            var states = LocationManager.GetStates();
            this.FromStates = states.Where(x => x.CountryId == this.SelectedFromCountry.Id).ToList();
            this.SelectedFromState = this.FromStates.FirstOrDefault();

            FromPostalCodeText = this.SelectedFromCountry.Id == 231 ? "Zip Code" : "Postal Code";
        }

        private void ExecuteFromStatesSelectionChangedCommand()
        {
            if (this.SelectedFromState != null)
            {
                var cities = LocationManager.GetCities();
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

        private void ExecuteSubmitCommand()
        {
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
                
            }
        }

        private void PackAndShipWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            //this.Left = desktopWorkingArea.Right - this.Width; (Display window on right side of screen)
            Top = 0;
            Left = desktopWorkingArea.Left; //(Display window on left side of screen)
            Width = desktopWorkingArea.Width - BaseWidth;
            Height = desktopWorkingArea.Height;

            //SetUnsetProperties();

            if (ToCountries == null && FromCountries == null)
            {
                ToCountries = LocationManager.GetCountries();
                FromCountries = LocationManager.GetCountries();
                SelectedToCountry = ToCountries.FirstOrDefault();
                this.SelectedFromCountry = FromCountries.FirstOrDefault();
            }

            if (this.ToStates == null && this.FromStates == null && this.SelectedToCountry != null && SelectedFromCountry != null)
            {
                var states = LocationManager.GetStates();
                ToStates = states.Where(x => x.CountryId == SelectedToCountry.Id).ToList();
                FromStates = states.Where(x => x.CountryId == SelectedFromCountry.Id).ToList();
                SelectedToState = ToStates.FirstOrDefault();
                SelectedFromState = FromStates.FirstOrDefault();
            }

            if (ToCities == null && FromCities == null && SelectedToState != null && SelectedFromState != null)
            {
                var cities = LocationManager.GetCities();
                ToCities = cities.Where(x => x.StateId == SelectedToState.Id).ToList();
                FromCities = cities.Where(x => x.StateId == SelectedFromState.Id).ToList();
                SelectedToCity = ToCities.FirstOrDefault();
                SelectedFromCity = FromCities.FirstOrDefault();
            }

            ToCountriesSelectionChangedCommand = new DelegateCommand(() => ExecuteToCountriesSelectionChangedCommand());
            ToStatesSelectionChangedCommand = new DelegateCommand(() => ExecuteToStatesSelectionChangedCommand());
            FromCountriesSelectionChangedCommand = new DelegateCommand(() => ExecuteFromCountriesSelectionChangedCommand());
            FromStatesSelectionChangedCommand = new DelegateCommand(() => ExecuteFromStatesSelectionChangedCommand());
            ToggleButtonCommand = new DelegateCommand<string>((item) => ExecuteToggleButtonCommand(item));

            AddNewItemCommand = new DelegateCommand(() => AddNewItemMethod());
            RemoveItemCommand = new DelegateCommand<ItemsCollection>((item) => RemoveItemMethod(item));

            ItemCollections = new ObservableCollection<ItemsCollection>();

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
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
