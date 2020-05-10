using System;
using System.Net;
using System.Threading.Tasks;
using NLog;
using UserManagement.Common.Constants;
using UserManagement.Common.Enums;
using UserManagement.WebServices.DataContracts.Request;
using UserManagement.WebServices.DataContracts.Response;
using LogLevel = NLog.LogLevel;

namespace UserManagement.WebServices
{
    public class WindowsWebService : WebServiceBase, IWindowsWebService
    {
        private string terminator = ".php?";
        public new Logger logger= LogManager.GetCurrentClassLogger();

        

        public async Task<ValidateUserResponseContract> ValidateUser(ValidateUserRequestContract reqContract)
        {
            logger.Trace(reqContract.Username);

            var endpoint =
                $"validate_user{terminator}"+
                $"username={reqContract.Username}"+
                $"&access_code={reqContract.AccessCode}"+
                $"&app_version_name=Version 4";

            var responseTuple = await GetAsync<ValidateUserResponseContract>(endpoint);
            var resultContract = responseTuple.Item2 ?? new ValidateUserResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            logger.Trace($"EndPoint:{endpoint}");
            logger.Trace($"Result code: {resultContract.StatusCode} Result Status: {resultContract.Status}");
            return resultContract;
        }

        public async Task<RegisterMasterStoreResponseContract> RegisterMasterStore(RegisterMasterStoreRequestContract reqContract)
        {
            logger.Trace(reqContract.StoreName);
            string endpoint = $"register_master_store{terminator}" +
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
                    $"store_preferred_language={reqContract.StorePreferedLanguage}&" +
                    $"app_version_name={reqContract.AppVersionName}&" +
                    $"facility_type={reqContract.FacilityType}&" +
                    $"device_token={reqContract.DeviceToken}&" +
                    $"device_id={reqContract.DeviceId}&" +
                    $"device_type={reqContract.DeviceType}&" +
                    $"timezone={reqContract.TimeZone}";

            var responseTuple = await GetAsync<RegisterMasterStoreResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new RegisterMasterStoreResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            logger.Trace($"EndPoint:{endpoint}");
            logger.Trace($"Result code: {resultContract.StatusCode} Result Status: {resultContract.Status}");
            return resultContract;
        }

        public async Task<DefaultResponseContract> CheckStoreUser(CheckUserRequestContract reqContract)
        {
            logger.Trace(reqContract.LastName);

            string endpoint = $"check_store_user{terminator}" +
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
                $"version=Version4&" +
                $"action={reqContract.Action}";

            var resultContract = await GetDefaultResponseContract(endpoint);
            return resultContract;
        }

        private async Task<DefaultResponseContract> GetDefaultResponseContract(string endpoint)
        {
            var responseTuple = await GetAsync<DefaultResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new DefaultResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            logger.Trace($"EndPoint:{endpoint}");
            logger.Trace($"Result code: {resultContract.StatusCode} Result Status: {resultContract.Status}");
            return resultContract;
        }

        public async Task<DefaultResponseContract> SaveUserData(SaveUserDataRequestContract reqContract, bool Dummy)
        {
            var endpoint = Dummy ? $"registerDummyMobile{terminator}" : $"save_user_data{terminator}";
            logger.Trace(reqContract.LastName);
            endpoint = endpoint +
                       $"action={reqContract.Action}&" +
                       $"firstname={reqContract.FirstName}&" +
                       $"lastname={reqContract.LastName}&" +
                       $"country_code={reqContract.CountryCode}&" +
                       $"mobile={reqContract.Mobile}&" +
                       $"master_store_id={reqContract.StoreId}&" +
                       $"store_id={reqContract.StoreId}&" +
                       $"btn1={reqContract.Button1}&btn2={reqContract.Button2}&" +
                       $"btn3={reqContract.Button3}&btn4={reqContract.Button4}&" +
                       $"orphan_status={reqContract.OrphanStatus}&" +
                       $"super_master_id={reqContract.SuperMasterId}&" +
                       $"deliver_order_status={reqContract.DeliverOrderStatus}&" +
                       $"fill_status={reqContract.FillStatus}&" +
                       $"postal_code={reqContract.PostalCode}&" +
                       $"home_phone={reqContract.HomePhone}&" +
                       $"country={reqContract.Country}&" +
                       $"city={reqContract.City}&" +
                       $"state={reqContract.State}&" +
                       $"gender={reqContract.Gender}&" +
                       $"version=4&" +
                       $"dob={reqContract.DOB}";

            if (!string.IsNullOrWhiteSpace(reqContract.ExpressTime))
            {
                endpoint += $"&reg_type=Express&" +
                            $"express_time={reqContract.ExpressTime}";
            }

            var resultContract = await GetDefaultResponseContract(endpoint);

            return resultContract;
        }

        public async Task<StoreUsersResponseContract> GetStoreUsers(GetStoreUsersRequestContract reqContract)
        {
            logger.Trace(reqContract.StoreId);
            string endpoint = $"get_store_users{terminator}" +
                              $"master_store_id={reqContract.StoreId}&" +
                              $"super_master_id={reqContract.SuperMasterId}";

            var responseTuple = await GetAsync<StoreUsersResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new StoreUsersResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            logger.Trace($"EndPoint:{endpoint}");
            logger.Trace($"Result code: {resultContract.StatusCode} Result Status: {resultContract.Status}");
            return resultContract;
        }

        public async Task<ArchieveStoreUsersResponseContract> GetArchiveStoreUsers(GetStoreUsersRequestContract reqContract)
        {
            logger.Trace(reqContract.StoreId);
            DateTime dt = TimeZoneInfo.ConvertTime(DateTime.UtcNow, reqContract.TimeZone);

            string endpoint = $"get_archive_store_users{terminator}" +
                $"master_store_id={reqContract.StoreId}&" +
                $"super_master_id={reqContract.SuperMasterId}&" +
                $"archive_length=6&" +
                $"current_time={dt.ToString("yyyy-MM-dd HH:mm:ss")}";
            var responseTuple = await GetAsync<ArchieveStoreUsersResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new ArchieveStoreUsersResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            logger.Trace($"EndPoint:{endpoint}");
            logger.Trace($"Result code: {resultContract.StatusCode} Result Status: {resultContract.Status}");
            return resultContract;
        }

        public async Task<DefaultResponseContract> DeleteStoreUser(DeleteStoreUserRequestContract reqContract)
        {
            logger.Trace(reqContract.Id);
            string endpoint = $"save_user_archive_data{terminator}" +
                $"master_store_id={reqContract.MasterStoreId}&" +
                $"id={reqContract.Id}&" +
                $"user_id={reqContract.UserId}&" +
                $"super_master_id={reqContract.SuperMasterId}&" +
                $"orphan_status={reqContract.OrphanStatus}";
            var resultContract = await GetDefaultResponseContract(endpoint);

            return resultContract;
        }

        public async Task<DefaultResponseContract> ManageUser(ManageUserRequestContract reqContract)
        {
            logger.Trace(reqContract.RoomNumber);
            var endpoint = $"manage_user{terminator}"+
                           $"action=update_idr_archive&id={reqContract.Id}&" +
                           $"master_store_id={reqContract.MasterStoreId}";

            var resultContract = await GetDefaultResponseContract(endpoint);
                
            return resultContract;
        }

        public async Task<DefaultResponseContract> CheckIDRArchiveUser(ManageUserRequestContract reqContract)
        {
            logger.Trace(reqContract.Id);
            var endpoint = $"manage_user{terminator}"+
                           $"action=update_idr_archive&id={reqContract.Id}&" +
                           $"master_store_id={reqContract.MasterStoreId}";

            var resultContract = await SendAndGetResponse(endpoint);
            return resultContract;
        }

        private async Task<DefaultResponseContract> SendAndGetResponse(string endpoint)
        {

            var responseTuple = await GetAsync<DefaultResponseContract>(endpoint, Config.CurrentUser.Token);
            responseTuple = await IsUserAuthorized(endpoint, responseTuple, RequestType.Get);
            var resultContract = responseTuple.Item2 ?? new DefaultResponseContract();
            resultContract.StatusCode = responseTuple.Item4;
            logger.Trace($"EndPoint:{endpoint}");
            logger.Trace($"Result code: {resultContract.StatusCode} Result Status: {resultContract.Status}");
            return resultContract;
        }

        public async Task<DefaultResponseContract> CheckIDRStoreUser(ManageUserRequestContract reqContract)
        {
            logger.Trace(reqContract.Id);
            var endpoint = $"manage_user{terminator}"+
                           $"action=update_idr_archive&id={reqContract.Id}&" +
                           $"master_store_id={reqContract.MasterStoreId}";
            var resultContract = await GetDefaultResponseContract(endpoint);

            return resultContract;
        }

        public async Task<DefaultResponseContract> DeleteArchiveUser(DeleteArchiveUserRequestContract reqContract)
        {
            logger.Trace(reqContract.Id);
            string endpoint = $"delete_archive{terminator}" +
                $"master_store_id={reqContract.MasterStoreId}&" +
                $"user_id={reqContract.UserId}&" +
                $"super_master_id={reqContract.SuperMasterId}&" +
                $"id={reqContract.Id}";

            var resultContract = await GetDefaultResponseContract(endpoint);

            return resultContract;
        }

        public async Task<DefaultResponseContract> EditStoreUser(EditStoreUserRequestContract reqContract)
        {
            logger.Trace(reqContract.LastName);
            string endpoint = $"edit_store_nonmobile_user{terminator}" +
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

            var resultContract = await GetDefaultResponseContract(endpoint);

            return resultContract;
        }

        public async Task<DefaultResponseContract> UpdateNonMobileUser(UpdateNonMobileStoreUserRequestContract reqContract)
        {
            logger.Trace(reqContract.Id);
            string endpoint = $"manage_user{terminator}" +
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
            var resultContract = await GetDefaultResponseContract(endpoint);

            return resultContract;
        }

        public async Task<DefaultResponseContract> UpdateButtons(UpdateButtonsRequestContract reqContract)
        {
            logger.Trace(reqContract.Id);
            string endpoint = $"manage_user{terminator}" +
                              $"id={reqContract.Id}&" +
                              $"user_id={reqContract.UserId}&" +
                              $"super_master_id={reqContract.SuperMasterId}&" +
                              $"action={reqContract.Action}&" +
                              $"btn1={reqContract.Button1}&" +
                              $"btn2={reqContract.Button2}&" +
                              $"btn3={reqContract.Button3}&" +
                              $"btn4={string.Empty}&" +
                              $"bad_exp_desc={string.Empty}";
            var resultContract = await GetDefaultResponseContract(endpoint);

            return resultContract;
        }

        public async Task<DefaultResponseContract> SetRoomNumber(ManageUserRequestContract reqContract)
        {
            logger.Trace(reqContract.Id);
            var endpoint = $"manage_user{terminator}" +
                           $"action=update_room_archive&id={reqContract.Id}&" +
                           $"super_master_id={Config.MasterStore.UserId}&" +
                           $"master_store_id={reqContract.MasterStoreId}&" +
                           $"room_num={reqContract.RoomNumber}";

            var resultContract = await GetDefaultResponseContract(endpoint);

            return resultContract;
        }

        public async Task<DefaultResponseContract> MoveStoreUser(MoveStoreUserRequestContract reqContract)
        {
            logger.Trace(reqContract.MovedId);
            string endpoint = $"manage_user{terminator}action=move&" +
                $"moved_pos_oid={reqContract.MovedPosOid}&" +
                $"mid={reqContract.Mid}&" +
                $"order_id={reqContract.OrderId}&" +
                $"moved_id={reqContract.MovedId}&" +
                $"new_cell_no={reqContract.NewCellNo}&" +
                $"super_master_id={Config.MasterStore.UserId}&" +
                $"old_cell_no={reqContract.OldCellNo}";

            var resultContract = await GetDefaultResponseContract(endpoint);

            return resultContract;
        }

        public async Task<DefaultResponseContract> SetUnsetFlag(SetUnsetFlagRequestContract reqContract)
        {
            logger.Trace(reqContract.Id);
            string endpoint = $"manage_user{terminator}" +
                $"action=recent_status&" +
                $"id={reqContract.Id}&" +
                $"master_store_id={reqContract.MasterStoreId}&" +
                $"super_master_id={Config.MasterStore.UserId}&" +
                $"recent_status={reqContract.RecentStatus}";

            var resultContract = await GetDefaultResponseContract(endpoint);

            return resultContract;
        }

        private async Task<Tuple<string, T, HttpStatusCode, int>> IsUserAuthorized<T>(string endpoint, Tuple<string, T, HttpStatusCode, int> responseTuple, RequestType requestType)
        {
            try
            {
                if (responseTuple.Item3 == HttpStatusCode.OK && (string.IsNullOrWhiteSpace(Config.CurrentUser.Token) || responseTuple.Item1.Contains("Not Authorized.")))
                {
                    var response = await ValidateUser(new ValidateUserRequestContract()
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
                logger.Error(ex);
                return new Tuple<string, T, HttpStatusCode, int>(ex.Message, default(T), HttpStatusCode.Unauthorized, 401);
            }
        }
    }
}
