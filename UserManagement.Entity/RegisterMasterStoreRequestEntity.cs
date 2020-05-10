using System;

namespace UserManagement.Entity
{
	public class RegisterMasterStoreRequestEntity
	{
        public long SuperMasterId { get; set; }
        public string UserId { get; set; }
		public string StoreName { get; set; }
		public string PhoneNumber { get; set; }
		public string Street { get; set; }
		public string PostalCode { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public long CountryCode { get; set; }
		public string StorePreferedLanguage { get; set; }
		public string AppVersionName { get; set; }
		public string DeviceToken { get; set; }
		public string DeviceId { get; set; }
        public string TimeZone { get; set; }
        public string DeviceType { get; set; }
        public TimeZoneInfo SelectedTimeZone { get; set; }
        public string FacilityType { get; set; }
    }
}
