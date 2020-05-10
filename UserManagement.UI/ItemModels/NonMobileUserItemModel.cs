using Prism.Mvvm;

namespace UserManagement.UI.ItemModels
{
    public class NonMobileUserItemModel : BindableBase
    {
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

        private string _country;
        public string Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        private string _city;
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        private string _state;
        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        private string _gender;
        public string Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        private string _dob;
        private string _roomNumber;

        public string DOB
        {
            get => _dob;
            set => SetProperty(ref _dob, value);
        }

        public bool IsNewRecord { get; set; } = true;

        public string RoomNumber
        {
            get => _roomNumber;
            set => SetProperty(ref _roomNumber, value);
        }
    }
}
