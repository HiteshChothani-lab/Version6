﻿using System;

namespace UserManagement.Common.Constants
{
    public class MasterStore
    {
        public long SuperMasterId { get; set; }
        public long StoreId { get; set; }
        public long UserId { get; set; }
        public string StoreName { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public long CountryCode { get; set; }
        public string Status { get; set; }
        public string Messagee { get; set; }
        public TimeZoneInfo TimeZone { get; set; }
    }
}
