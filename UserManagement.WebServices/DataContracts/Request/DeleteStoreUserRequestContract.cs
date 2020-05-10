namespace UserManagement.WebServices.DataContracts.Request
{
    public class DeleteStoreUserRequestContract
    {
        public string MasterStoreId { get; set; }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string OrphanStatus { get; set; }
        public long SuperMasterId { get; set; }
    }
}
