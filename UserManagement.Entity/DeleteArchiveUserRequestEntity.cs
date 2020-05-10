namespace UserManagement.Entity
{
    public class DeleteArchiveUserRequestEntity
    {
        public string MasterStoreId { get; set; }
        public string UserId { get; set; }
        public string Id { get; set; }
        public long SuperMasterId { get; set; }
    }
}
