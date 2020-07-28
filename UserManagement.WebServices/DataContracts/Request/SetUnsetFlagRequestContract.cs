namespace UserManagement.WebServices.DataContracts.Request
{
    public class SetUnsetFlagRequestContract
    {
        public string MasterStoreId { get; set; }
        public string Id { get; set; }
        public int RecentStatus { get; set; } // 0(no flag) - 1(flag)
    }
}
