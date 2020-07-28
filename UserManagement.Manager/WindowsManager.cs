using Newtonsoft.Json;
using System.Threading.Tasks;
using UserManagement.Common.Constants;
using UserManagement.Common.Enums;
using UserManagement.Common.Utilities;
using UserManagement.Entity;
using UserManagement.Manager.Mappers;
using UserManagement.WebServices;
using UserManagement.WebServices.DataContracts.Request;

namespace UserManagement.Manager
{
    public class WindowsManager : ManagerBase, IWindowsManager
    {
        private readonly IWindowsWebService _windowsWebService;

        public WindowsManager(IConnectivity connectivity, IServiceEntityMapper mapper, IWindowsWebService windowsWebService) : base(connectivity, mapper)
        {
            _windowsWebService = windowsWebService;
        }

        public async Task<ValidateUserResponseEntity> ValidateUser(ValidateUserRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new ValidateUserResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<ValidateUserRequestContract>(reqEntity);

            var respContract = await _windowsWebService.ValidateUser(reqContract);
            var respEntity = Mapper.Map<ValidateUserResponseEntity>(respContract);

            if (respEntity.StatusCode == (int)GenericStatusValue.Success)
            {
                respEntity.Username = reqEntity.Username;
                respEntity.AccessCode = reqEntity.AccessCode;

                string json = JsonConvert.SerializeObject(respEntity);

                Config.SaveUserData(json);
            }

            return respEntity;
        }

        public async Task<RegisterMasterStoreResponseEntity> RegisterMasterStore(RegisterMasterStoreRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new RegisterMasterStoreResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<RegisterMasterStoreRequestContract>(reqEntity);

            var respContract = await _windowsWebService.RegisterMasterStore(reqContract);
            var respEntity = Mapper.Map<RegisterMasterStoreResponseEntity>(respContract);

            if (respEntity.StatusCode == (int)GenericStatusValue.Success)
            {
                string json = JsonConvert.SerializeObject(respEntity);

                Config.SaveMasterDataLocal(json);
            }

            return respEntity;
        }

        public void Logout()
        {
            Config.ClearMasterData();
            Config.ClearUserData();
        }

        public async Task<DefaultResponseEntity> CheckStoreUser(CheckUserRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new DefaultResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<CheckUserRequestContract>(reqEntity);

            var respContract = await _windowsWebService.CheckStoreUser(reqContract);
            var respEntity = Mapper.Map<DefaultResponseEntity>(respContract);

            return respEntity;
        }
        public async Task<DefaultResponseEntity> SaveUserData(SaveUserDataRequestEntity reqEntity, bool dummy)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new DefaultResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<SaveUserDataRequestContract>(reqEntity);
            var respContract = await _windowsWebService.SaveUserData(reqContract, dummy);
            var respEntity = Mapper.Map<DefaultResponseEntity>(respContract);

            return respEntity;
        }

        public async Task<StoreUsersResponseEntity> GetStoreUsers(GetStoreUsersRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new StoreUsersResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<GetStoreUsersRequestContract>(reqEntity);

            var respContract = await _windowsWebService.GetStoreUsers(reqContract);
            var respEntity = Mapper.Map<StoreUsersResponseEntity>(respContract);

            return respEntity;
        }

        public async Task<ArchieveStoreUsersResponseEntity> GetArchieveStoreUsers(GetStoreUsersRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new ArchieveStoreUsersResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<GetStoreUsersRequestContract>(reqEntity);

            var respContract = await _windowsWebService.GetArchieveStoreUsers(reqContract);
            var respEntity = Mapper.Map<ArchieveStoreUsersResponseEntity>(respContract);

            return respEntity;
        }

        public async Task<DefaultResponseEntity> DeleteStoreUser(DeleteStoreUserRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new DefaultResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<DeleteStoreUserRequestContract>(reqEntity);

            var respContract = await _windowsWebService.DeleteStoreUser(reqContract);
            var respEntity = Mapper.Map<DefaultResponseEntity>(respContract);

            return respEntity;
        }

        public async Task<DefaultResponseEntity> ManageUser(ManageUserRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new DefaultResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<ManageUserRequestContract>(reqEntity);

            var respContract = await _windowsWebService.ManageUser(reqContract);
            var respEntity = Mapper.Map<DefaultResponseEntity>(respContract);

            return respEntity;
        }

        public async Task<DefaultResponseEntity> CheckIDRArchiveUser(ManageUserRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new DefaultResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<ManageUserRequestContract>(reqEntity);

            var respContract = await _windowsWebService.CheckIDRArchiveUser(reqContract);
            var respEntity = Mapper.Map<DefaultResponseEntity>(respContract);

            return respEntity;
        }

        public async Task<DefaultResponseEntity> CheckIDRStoreUser(ManageUserRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new DefaultResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<ManageUserRequestContract>(reqEntity);

            var respContract = await _windowsWebService.CheckIDRStoreUser(reqContract);
            var respEntity = Mapper.Map<DefaultResponseEntity>(respContract);

            return respEntity;
        }

        public async Task<DefaultResponseEntity> DeleteArchiveUser(DeleteArchiveUserRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new DefaultResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<DeleteArchiveUserRequestContract>(reqEntity);

            var respContract = await _windowsWebService.DeleteArchiveUser(reqContract);
            var respEntity = Mapper.Map<DefaultResponseEntity>(respContract);

            return respEntity;
        }

        public async Task<DefaultResponseEntity> EditStoreUser(EditStoreUserRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new DefaultResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<EditStoreUserRequestContract>(reqEntity);

            var respContract = await _windowsWebService.EditStoreUser(reqContract);
            var respEntity = Mapper.Map<DefaultResponseEntity>(respContract);

            return respEntity;
        }

        public async Task<DefaultResponseEntity> UpdateNonMobileStoreUser(UpdateNonMobileStoreUserRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new DefaultResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<UpdateNonMobileStoreUserRequestContract>(reqEntity);

            var respContract = await _windowsWebService.UpdateNonMobileUser(reqContract);
            var respEntity = Mapper.Map<DefaultResponseEntity>(respContract);

            return respEntity;
        }

        public async Task<DefaultResponseEntity> UpdateButtons(UpdateButtonsRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new DefaultResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<UpdateButtonsRequestContract>(reqEntity);

            var respContract = await _windowsWebService.UpdateButtons(reqContract);
            var respEntity = Mapper.Map<DefaultResponseEntity>(respContract);

            return respEntity;
        }

        public async Task<DefaultResponseEntity> MoveStoreUser(MoveStoreUserRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new DefaultResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<MoveStoreUserRequestContract>(reqEntity);

            var respContract = await _windowsWebService.MoveStoreUser(reqContract);
            var respEntity = Mapper.Map<DefaultResponseEntity>(respContract);

            return respEntity;
        }

        public async Task<DefaultResponseEntity> SetUnsetFlag(SetUnsetFlagRequestEntity reqEntity)
        {
            if (!Connectivity.IsInternetAvailable)
            {
                return new DefaultResponseEntity() { StatusCode = (int)GenericStatusValue.NoInternetConnection };
            }

            var reqContract = Mapper.Map<SetUnsetFlagRequestContract>(reqEntity);

            var respContract = await _windowsWebService.SetUnsetFlag(reqContract);
            var respEntity = Mapper.Map<DefaultResponseEntity>(respContract);

            return respEntity;
        }
    }
}
