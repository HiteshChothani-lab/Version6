namespace UserManagement.WebServices.DataContracts.Request
{
    public class UpdateButtonsRequestContract
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public long UserId { get; set; }
        public long SuperMasterId { get; set; }
        public string Button1 { get; set; }
        public string Button2 { get; set; }
        public string Button3 { get; set; }
        public string Button4 { get; set; }
        public string ServiceUsedStatus { get; set; }
        public string Question1 { get; set; }
        public string Question2 { get; set; }
        public string Question3 { get; set; }
        public string Question4 { get; set; }
    }
}
