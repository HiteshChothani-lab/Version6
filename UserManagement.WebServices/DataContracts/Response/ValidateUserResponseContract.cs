using Newtonsoft.Json;

namespace UserManagement.WebServices.DataContracts.Response
{
    public class ValidateUserResponseContract : ContractBase
    {
        [JsonProperty("app_version_name")]
        public string AppVersionName { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Messagee { get; set; }
    }
}
