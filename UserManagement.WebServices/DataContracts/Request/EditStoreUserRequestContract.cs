namespace UserManagement.WebServices.DataContracts.Request
{
    public class EditStoreUserRequestContract
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string HomePhone { get; set; } = string.Empty;
        public string MasterStoreId { get; set; }
        public string CountryCode { get; set; }
        public string UserId { get; set; }
        public string Mobile { get; set; } = string.Empty;
        public string Action { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string DOB { get; set; } = string.Empty;
        public long SuperMasterId { get; set; }
    }
}
