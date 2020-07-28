using Newtonsoft.Json;
using System.Collections.Generic;
using UserManagement.Common.Converters;

namespace UserManagement.WebServices.DataContracts.Response
{
    public class StoreUsersResponseContract : ContractBase
    {
        [JsonProperty("data")]
        public List<StoreUserContract> Data { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Messagee { get; set; }
    }

    public class ArchieveStoreUsersResponseContract : ContractBase
    {
        [JsonProperty("archive_size")]
        public long ArchieveSize { get; set; }

        [JsonProperty("data")]
        public List<StoreUserContract> Data { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Messagee { get; set; }
    }

    public class StoreUserContract
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("super_master_id")]
        public string SuperMasterId { get; set; }

        [JsonProperty("master_store_id")]
        public string MasterStoreId { get; set; }

        [JsonProperty("store_id")]
        public string StoreId { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("btn1")]
        public string Btn1 { get; set; }

        [JsonProperty("btn2")]
        public string Btn2 { get; set; }

        [JsonProperty("btn3")]
        public string Btn3 { get; set; }

        [JsonProperty("btn4")]
        public string Btn4 { get; set; }

        [JsonProperty("btn5")]
        public string Btn5 { get; set; }

        [JsonProperty("btn_a_b")]
        public string BtnAB { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("note_color")]
        public string NoteColor { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("orphan_status")]
        public string OrphanStatus { get; set; }

        [JsonProperty("recent_status")]
        public string RecentStatus { get; set; }

        [JsonProperty("deliver_order_status")]
        public string DeliverOrderStatus { get; set; }

        [JsonProperty("idr_status")]
        public string IdrStatus { get; set; }

        [JsonProperty("account_block_status")]
        public string AccountBlockStatus { get; set; }

        [JsonProperty("bad_exp_desc")]
        public string BadExpDesc { get; set; }

        [JsonProperty("age")]
        public string Age { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("home_phone")]
        public string HomePhone { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("register_type")]
        public string RegisterType { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("dob")]
        public string DOB { get; set; }

        [JsonProperty("time_difference")]
        public string TimeDifference { get; set; }

        [JsonProperty("service_used_status")]
        public string ServiceUsedStatus { get; set; }

        [JsonProperty("q1")]
        public string Question1 { get; set; }

        [JsonProperty("q2")]
        public string Question2 { get; set; }

        [JsonProperty("q3")]
        public string Question3 { get; set; }

        [JsonProperty("q4")]
        public string Question4 { get; set; }
    }
}
