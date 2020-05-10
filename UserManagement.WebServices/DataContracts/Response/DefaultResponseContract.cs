using Newtonsoft.Json;

namespace UserManagement.WebServices.DataContracts.Response
{
    public class DefaultResponseContract : ContractBase
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public new string Message { get; set; }
    }
}
