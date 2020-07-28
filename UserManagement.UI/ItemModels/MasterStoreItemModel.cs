using UserManagement.Entity;

namespace UserManagement.UI.ItemModels
{
	public class MasterStoreItemModel
	{
		public string UserId { get; set; }
		public CountryEntity Country { get; set; }
		public StateEntity State { get; set; }
		public CityEntity City { get; set; }
		public string PostalCode { get; set; }
        public string TimeZone { get; set; }
        public string TimeZoneDisplayName { get; set; }

        public string StoreName { get; set; }
		public string PhoneNumber { get; set; }
		public string Street { get; set; }
		public string Address { get; set; }

		public string CityFull
		{
			get => $"{City.Name}, {State.Name}, {Country.Name}";
		}
		public string AddressFull
		{
			get => $"{Street}, {Address}";
		}
	}
}
