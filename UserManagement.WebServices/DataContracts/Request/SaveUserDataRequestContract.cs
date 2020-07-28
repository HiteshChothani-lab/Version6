namespace UserManagement.WebServices.DataContracts.Request
{
    public class SaveUserDataRequestContract
    {
        public string Action { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public long CountryCode { get; set; }
        public string Mobile { get; set; } = string.Empty;
        public long MasterStoreId { get; set; }
        public string Button1 { get; set; } = string.Empty;
        public string Button2 { get; set; } = string.Empty;
        public string Button3 { get; set; } = string.Empty;
        public string Button4 { get; set; } = string.Empty;
        public long OrphanStatus { get; set; }
        public long SuperMasterId { get; set; }
        public long DeliverOrderStatus { get; set; }
        public string PostalCode { get; set; } = string.Empty;
        public string HomePhone { get; set; } = string.Empty;
        public long FillStatus { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string DOB { get; set; } = string.Empty;
    }
}
