using Newtonsoft.Json;
using UserManagement.Common.Converters;

namespace UserManagement.Entity
{
	public class CityEntity
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("state_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long StateId { get; set; }
    }
}
