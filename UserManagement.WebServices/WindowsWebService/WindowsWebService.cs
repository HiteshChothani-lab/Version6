using System;
using System.Net;
using System.Threading.Tasks;
using UserManagement.Common.Constants;
using UserManagement.Common.Enums;
using UserManagement.WebServices.DataContracts.Request;
using UserManagement.WebServices.DataContracts.Response;

namespace UserManagement.WebServices
{
    public class WindowsWebService : WebServiceBase, IWindowsWebService
    {
        public async Task<ValidateUserResponseContract> ValidateUser(ValidateUserRequestContract reqContract)
        {
            string endpoint = string.Format("validate_user.php?username={0}&access_code={1}&app_version_name=Version 6", reqContract.Username, reqContract.AccessCode);
            var responseTuple = await GetAsync<ValidateUserResponseContract>(endpoint);
            var resultContract = responseTuple.Item2 ?? new ValidateUserResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }

        public async Task<RegisterMasterStoreResponseContract> RegisterMasterStore(RegisterMasterStoreRequestContract reqContract)
        {
            string endpoint = $"register_master_store.php?" +
                $"super_master_id={reqContract.UserId}&" +
                $"store_name={reqContract.StoreName}&" +
                $"phone={reqContract.PhoneNumber}&" +
                $"street={reqContract.Street}&" +
                $"postal_code={reqContract.PostalCode}&" +
                $"address={reqContract.Address}&" +
                $"city={reqContract.City}&" +
                $"state={reqContract.State}&" +
                $"country={reqContract.Country}&" +
                $"country_code={reqContract.CountryCode}&" +
                $"store_preferred_language={reqContract.StorePrefferedLanguage}&" +
                $"app_version_name={reqContract.AppVersionName}&" +
                $"device_token={reqContract.DeviceToken}&" +
                $"device_id={reqContract.DeviceId}&" +
                $"device_type={reqContract.DeviceType}&" +
                $"timezone={reqContract.TimeZone}";

            var responseTuple = await GetAsync<RegisterMasterStoreResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new RegisterMasterStoreResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }

        public async Task<DefaultResponseContract> CheckStoreUser(CheckUserRequestContract reqContract)
        {
            string endpoint = $"check_store_user.php?" +
                $"firstname={reqContract.FirstName}&" +
                $"lastname={reqContract.LastName}&" +
                $"postal_code={reqContract.PostalCode}&" +
                $"home_phone={reqContract.HomePhone}&" +
                $"master_store_id={reqContract.MasterStoreId}&" +
                $"super_master_id={reqContract.SuperMasterId}&" +
                $"country_code={reqContract.CountryCode}&" +
                $"country={reqContract.Country}&" +
                $"city={reqContract.City}&" +
                $"state={reqContract.State}&" +
                $"gender={reqContract.Gender}&" +
                $"dob={reqContract.DOB}&" +
                $"version=6&" +
                $"action={reqContract.Action}";

            var responseTuple = await GetAsync<DefaultResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new DefaultResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }
        public async Task<DefaultResponseContract> SaveUserData(SaveUserDataRequestContract reqContract, bool dummy)
        {
            string endpoint = dummy ? $"registerDummyMobile.php?" : $"save_user_data.php?";
            endpoint = $"{endpoint}" +
                $"action={reqContract.Action}&" +
                $"firstname={reqContract.FirstName}&" +
                $"lastname={reqContract.LastName}&" +
                $"country_code={reqContract.CountryCode}&" +
                $"mobile={reqContract.Mobile}&" +
                $"master_store_id={reqContract.MasterStoreId}&" +
                $"btn1={reqContract.Button1}&" +
                $"btn2={reqContract.Button2}&" +
                $"btn3={reqContract.Button3}&" +
                $"btn4={reqContract.Button4}&" +
                $"orphan_status={reqContract.OrphanStatus}&" +
                $"super_master_id={reqContract.SuperMasterId}&" +
                $"deliver_order_status={reqContract.DeliverOrderStatus}&" +
                $"postal_code={reqContract.PostalCode}&" +
                $"home_phone={reqContract.HomePhone}&" +
                $"fill_status={reqContract.FillStatus}&" +
                $"country={reqContract.Country}&" +
                $"city={reqContract.City}&" +
                $"state={reqContract.State}&" +
                $"gender={reqContract.Gender}&" +
                $"version=6&" +
                $"dob={reqContract.DOB}";

            var responseTuple = await GetAsync<DefaultResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new DefaultResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }

        public async Task<StoreUsersResponseContract> GetStoreUsers(GetStoreUsersRequestContract reqContract)
        {
            string endpoint = $"get_store_users.php?" +
                $"master_store_id={reqContract.StoreId}&" +
                $"super_master_id={reqContract.SuperMasterId}";

            var responseTuple = await GetAsync<StoreUsersResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new StoreUsersResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }

        public async Task<ArchieveStoreUsersResponseContract> GetArchieveStoreUsers(GetStoreUsersRequestContract reqContract)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string endpoint = $"get_archive_store_users.php?" +
                $"master_store_id={reqContract.StoreId}&" +
                $"super_master_id={reqContract.SuperMasterId}&" +
                $"archive_length=6&" +
                $"current_time={time}";

            var responseTuple = await GetAsync<ArchieveStoreUsersResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new ArchieveStoreUsersResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }

        public async Task<DefaultResponseContract> DeleteStoreUser(DeleteStoreUserRequestContract reqContract)
        {
            string endpoint = $"save_user_archive_data.php?" +
                $"master_store_id={reqContract.MasterStoreId}&" +
                $"id={reqContract.Id}&" +
                $"user_id={reqContract.UserId}&" +
                $"super_master_id={reqContract.SuperMasterId}&" +
                $"orphan_status={reqContract.OrphanStatus}";

            var responseTuple = await GetAsync<DefaultResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new DefaultResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }

        public async Task<DefaultResponseContract> ManageUser(ManageUserRequestContract reqContract)
        {
            string endpoint = $"manage_user.php?action=update_idr_archive&id={reqContract.Id}";

            var responseTuple = await GetAsync<DefaultResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new DefaultResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }

        public async Task<DefaultResponseContract> CheckIDRArchiveUser(ManageUserRequestContract reqContract)
        {
            string endpoint = $"manage_user.php?" +
                $"action=update_idr_archive&" +
                $"master_store_id={reqContract.MasterStoreId}&" +
                $"super_master_id={reqContract.SuperMasterId}&" +
                $"id={reqContract.Id}";

            var responseTuple = await GetAsync<DefaultResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new DefaultResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }

        public async Task<DefaultResponseContract> CheckIDRStoreUser(ManageUserRequestContract reqContract)
        {
            string endpoint = $"manage_user.php?" +
                $"action=update_idr&" +
                $"master_store_id={reqContract.MasterStoreId}&" +
                $"super_master_id={reqContract.SuperMasterId}&" +
                $"id={reqContract.Id}";

            var responseTuple = await GetAsync<DefaultResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new DefaultResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }

        public async Task<DefaultResponseContract> DeleteArchiveUser(DeleteArchiveUserRequestContract reqContract)
        {
            string endpoint = $"delete_archive.php?" +
                $"master_bookstore_id={reqContract.MasterStoreId}&" +
                $"user_id={reqContract.UserId}&" +
                $"super_master_id={reqContract.SuperMasterId}&" +
                $"id={reqContract.Id}";

            var responseTuple = await GetAsync<DefaultResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new DefaultResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }

        public async Task<DefaultResponseContract> EditStoreUser(EditStoreUserRequestContract reqContract)
        {
            string endpoint = $"edit_store_nonmobile_user.php?" +
                $"firstname={reqContract.FirstName}&" +
                $"lastname={reqContract.LastName}&" +
                $"postal_code={reqContract.PostalCode}&" +
                $"home_phone={reqContract.HomePhone}&" +
                $"master_store_id={reqContract.MasterStoreId}&" +
                $"country_code={reqContract.CountryCode}&" +
                $"user_id={reqContract.UserId}&" +
                $"super_master_id={reqContract.SuperMasterId}&" +
                $"mobile={reqContract.Mobile}&" +
                $"action={reqContract.Action}&" +
                $"country={reqContract.Country}&" +
                $"city={reqContract.City}&" +
                $"state={reqContract.State}&" +
                $"gender={reqContract.Gender}&" +
                $"dob={reqContract.DOB}";

            var responseTuple = await GetAsync<DefaultResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new DefaultResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }

        public async Task<DefaultResponseContract> UpdateNonMobileUser(UpdateNonMobileStoreUserRequestContract reqContract)
        {
            string endpoint = $"manage_user.php?" +
                $"action={reqContract.Action}&" +
                $"id={reqContract.Id}&" +
                $"user_id={reqContract.UserId}&" +
                $"super_master_id={reqContract.SuperMasterId}&" +
                $"home_phone={reqContract.HomePhone}&" +
                $"postal_code={reqContract.PostalCode}&" +
                $"master_store_id={reqContract.MasterStoreId}&" +
                $"firstname={reqContract.FirstName}&" +
                $"lastname={reqContract.LastName}&" +
                $"country={reqContract.Country}&" +
                $"city={reqContract.City}&" +
                $"state={reqContract.State}&" +
                $"gender={reqContract.Gender}&" +
                $"dob={reqContract.DOB}";

            var responseTuple = await GetAsync<DefaultResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new DefaultResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }

        public async Task<DefaultResponseContract> UpdateButtons(UpdateButtonsRequestContract reqContract)
        {
            string endpoint = $"manage_user.php?" +
                $"id={reqContract.Id}&" +
                $"user_id={reqContract.UserId}&" +
               $"super_master_id={reqContract.SuperMasterId}&" +
                $"action={reqContract.Action}&" +
                $"btn1={reqContract.Button1}&" +
                $"btn2={reqContract.Button2}&" +
                $"btn3={reqContract.Button3}&" +
                $"btn4={reqContract.Button4}&" +
                $"service_used_status={reqContract.ServiceUsedStatus}&" +
                $"q1={reqContract.Question1}&" +
                $"q2={reqContract.Question2}&" +
                $"q3={reqContract.Question3}&" +
                $"q4={reqContract.Question4}";

            var responseTuple = await GetAsync<DefaultResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new DefaultResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }

        public async Task<DefaultResponseContract> MoveStoreUser(MoveStoreUserRequestContract reqContract)
        {
            string endpoint = $"manage_user.php?action=move&" +
                $"moved_pos_oid={reqContract.MovedPosOid}&" +
                $"mid={reqContract.Mid}&" +
                $"order_id={reqContract.OrderId}&" +
                $"moved_id={reqContract.MovedId}&" +
                $"new_cell_no={reqContract.NewCellNo}&" +
                $"super_master_id={Config.MasterStore.UserId}&" +
                $"old_cell_no={reqContract.OldCellNo}";

            var responseTuple = await GetAsync<DefaultResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new DefaultResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }

        public async Task<DefaultResponseContract> SetUnsetFlag(SetUnsetFlagRequestContract reqContract)
        {
            string endpoint = $"manage_user.php?" +
                $"action=recent_status&" +
                $"id={reqContract.Id}&" +
                $"master_store_id={reqContract.MasterStoreId}&" +
                $"super_master_id={Config.MasterStore.UserId}&" +
                $"recent_status={reqContract.RecentStatus}";

            var responseTuple = await GetAsync<DefaultResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new DefaultResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            return resultContract;
        }

        private async Task<Tuple<string, T, HttpStatusCode, int>> IsUserAuthorized<T>(string endpoint, Tuple<string, T, HttpStatusCode, int> responseTuple, RequestType requestType)
        {
            try
            {
                if (responseTuple.Item3 == HttpStatusCode.OK && (string.IsNullOrWhiteSpace(Config.CurrentUser.Token) || responseTuple.Item1.Contains("Not Authorized.")))
                {
                    var response = await this.ValidateUser(new ValidateUserRequestContract()
                    {
                        AccessCode = Config.CurrentUser.AccessCode,
                        Username = Config.CurrentUser.Username
                    });

                    if (response.StatusCode != (int)GenericStatusValue.Success)
                    {
                        return new Tuple<string, T, HttpStatusCode, int>("Not Authorized.", default(T), HttpStatusCode.Unauthorized, 401);
                    }
                    else
                    {
                        Config.CurrentUser.Token = response.Token;
                        if (requestType == RequestType.Get)
                        {
                            return await GetAsync<T>(endpoint, Config.CurrentUser.Token);
                        }
                    }
                }

                return responseTuple;
            }
            catch (Exception ex)
            {
                return new Tuple<string, T, HttpStatusCode, int>(ex.Message, default(T), HttpStatusCode.Unauthorized, 401);
            }
        }
    }
}
