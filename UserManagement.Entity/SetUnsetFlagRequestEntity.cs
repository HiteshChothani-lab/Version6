namespace UserManagement.Entity
{
    public class SetUnsetFlagRequestEntity
    {
        public string MasterStoreId { get; set; }
        public string Id { get; set; }
        public int RecentStatus { get; set; } // 0(no flag) - 1(flag)
    }
}
