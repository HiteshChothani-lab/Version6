using System;

namespace UserManagement.Entity
{
    public class GetStoreUsersRequestEntity
    {
        public long SuperMasterId { get; set; }
        public long StoreId { get; set; }
        public TimeZoneInfo TimeZone { get; set; }
    }
}
