using System.Threading.Tasks;
using UserManagement.WebServices.DataContracts.Request;
using UserManagement.WebServices.DataContracts.Response;

namespace UserManagement.WebServices
{
    public interface IWindowsWebService
    {
        Task<ValidateUserResponseContract> ValidateUser(ValidateUserRequestContract reqContract);
		Task<RegisterMasterStoreResponseContract> RegisterMasterStore(RegisterMasterStoreRequestContract reqContract);
        Task<DefaultResponseContract> CheckStoreUser(CheckUserRequestContract reqContract);
        Task<DefaultResponseContract> SaveUserData(SaveUserDataRequestContract reqContract, bool Dummy);
        Task<StoreUsersResponseContract> GetStoreUsers(GetStoreUsersRequestContract reqContract);
        Task<ArchieveStoreUsersResponseContract> GetArchieveStoreUsers(GetStoreUsersRequestContract reqContract);
        Task<DefaultResponseContract> DeleteStoreUser(DeleteStoreUserRequestContract reqContract);
        Task<DefaultResponseContract> ManageUser(ManageUserRequestContract reqContract);
        Task<DefaultResponseContract> CheckIDRArchiveUser(ManageUserRequestContract reqContract);
        Task<DefaultResponseContract> CheckIDRStoreUser(ManageUserRequestContract reqContract);
        Task<DefaultResponseContract> DeleteArchiveUser(DeleteArchiveUserRequestContract reqContract);
        Task<DefaultResponseContract> EditStoreUser(EditStoreUserRequestContract reqContract);
        Task<DefaultResponseContract> UpdateNonMobileUser(UpdateNonMobileStoreUserRequestContract reqContract);
        Task<DefaultResponseContract> MoveStoreUser(MoveStoreUserRequestContract reqContract);
        Task<DefaultResponseContract> UpdateButtons(UpdateButtonsRequestContract reqEntity);
        Task<DefaultResponseContract> SetUnsetFlag(SetUnsetFlagRequestContract reqContract);
    }
}