namespace UserManagement.WebServices.DataContracts.Request
{
    public class UpdateButtonsRequestContract
    {
        public string Id { get; set; }
        public long UserId { get; set; }
        public string Action { get; set; }
        public string Button1 { get; set; }
        public string Button2 { get; set; }
        public string Button3 { get; set; }
        public long SuperMasterId { get; set; }
    }
}
