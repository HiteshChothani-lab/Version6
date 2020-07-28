using Newtonsoft.Json;
using UserManagement.Common.Converters;

namespace UserManagement.Entity
{
	public class StateEntity
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CountryId { get; set; }
    }
}
