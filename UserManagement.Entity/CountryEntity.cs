﻿using Newtonsoft.Json;

namespace UserManagement.Entity
{
    public class CountryEntity
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("sortname")]
        public string Sortname { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phoneCode")]
        public long PhoneCode { get; set; }
    }
}
