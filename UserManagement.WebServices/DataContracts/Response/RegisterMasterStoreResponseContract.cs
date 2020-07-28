using Newtonsoft.Json;
using UserManagement.Common.Converters;

namespace UserManagement.WebServices.DataContracts.Response
{
	public class RegisterMasterStoreResponseContract : ContractBase
	{
		[JsonProperty("super_master_id")]
		[JsonConverter(typeof(ParseStringConverter))]
		public long SuperMasterId { get; set; }

		[JsonProperty("store_id")]
		public long StoreId { get; set; }

		[JsonProperty("user_id")]
		[JsonConverter(typeof(ParseStringConverter))]
		public long UserId { get; set; }

		[JsonProperty("store_name")]
		public string StoreName { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }

		[JsonProperty("postal_code")]
		public string PostalCode { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }

		[JsonProperty("street")]
		public string Street { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("country_code")]
		public long CountryCode { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("message")]
		public string Messagee { get; set; }
	}
}
