using System;

namespace UserManagement.WebServices.DataContracts.Request
{
    public class GetStoreUsersRequestContract
    {
        public long SuperMasterId { get; set; }
        public long StoreId { get; set; }
        public TimeZoneInfo TimeZone { get; set; }
    }
}
